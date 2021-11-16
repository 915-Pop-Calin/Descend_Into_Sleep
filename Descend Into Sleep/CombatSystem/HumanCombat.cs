using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game.keysWork;

namespace ConsoleApp12.CombatSystem
{
    public class HumanCombat: Combat
    {
        private bool InvalidInput;
        public HumanCombat(HumanPlayer humanPlayer): base(humanPlayer)
        {
            ;
        }
        
        private String[] GetActions()
        {
            var abilities = Player.GetRespectiveAbilities();
            var length = abilities.Count;
            var abilitiesString = new String[length + 1];
            var currentPosition = 0;
            
            foreach (var ability in Player.GetRespectiveAbilities())
            {
                abilitiesString[currentPosition] = ability.Key;
                currentPosition += 1;
            }

            abilitiesString[length] = "back"; 
            return abilitiesString;
        }

        private bool Action(Character secondCharacter)
        {
            var actions = GetActions();

            try
            {
                if (actions.Length == 0)
                    throw new NoAbilitiesException();
                var choice = ConsoleHelper.MultipleChoice(15, actions);
                if (choice == actions.Length - 1)
                    return false;
                var chosenAbilityKey = actions[choice];
                var toStrCast = Player.Cast(chosenAbilityKey, secondCharacter, ListOfTurns, TurnCounter);
                Console.WriteLine(toStrCast);
                InvalidInput = false;
                return true;
            }

            catch (StunException stunException)
            {
                Console.WriteLine(stunException.Message);
            }
            catch (CooldownException cooldownException)
            {
                Console.WriteLine(cooldownException.Message);
            }
            catch (NegativeAttackException negativeAttackException)
            {
                Console.WriteLine(negativeAttackException.Message);
            }
            catch (InsufficientManaException insufficientManaException)
            {
                Console.WriteLine(insufficientManaException.Message);
            }
            catch (InexistentDecastException inexistentDecastException)
            {
                Console.WriteLine(inexistentDecastException.Message);
            }
            catch (SchoolException schoolException)
            {
                Console.WriteLine(schoolException.Message);
            }
            catch (EmptyQueueException emptyQueueException)
            {
                Console.WriteLine(emptyQueueException.Message);
            }
            return false;
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var humanPlayer = (HumanPlayer) Player;

            InvalidInput = true;

            while (InvalidInput)
            {
                var choice = ConsoleHelper.MultipleChoice(20, "attack", "actions", "check stats", "equip item");
                switch (choice)
                {
                    case 0:
                        var toStr = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
                        Console.WriteLine(toStr);
                        InvalidInput = false;
                        break;
                    case 1:
                        try
                        {
                            if (Action(secondCharacter))
                                InvalidInput = false;
                        }
                        catch (NoAbilitiesException noAbilitiesException)
                        {
                            Console.WriteLine(noAbilitiesException.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine(secondCharacter);
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine(humanPlayer.ShowInventory());
                            Console.WriteLine("The item you want to equip:\n");
                            var toStrEquip = Console.ReadLine();
                            var toStrEquipped = humanPlayer.UseItem(toStrEquip);
                            Console.WriteLine(toStrEquipped);
                            InvalidInput = false;
                        }
                        catch (InvalidItemException invalidItemException)
                        {
                            Console.WriteLine(invalidItemException.Message);
                        }

                        break;
                }
            }

            TurnCounter++;
        }

        public void PostCombat(int experienceToGain, int goldToGain)
        {
            var humanPlayer = (HumanPlayer) Player;
            var hasLeveled = humanPlayer.GainExperience(experienceToGain);
            Console.WriteLine("You have gained " + experienceToGain.ToString() + " experience!\n");
            if (hasLeveled)
                Console.WriteLine("Level up! Level " + humanPlayer.GetLevel().ToString() + "!\n");
            Console.WriteLine("You have gained " + goldToGain.ToString() + " gold!\n");
            humanPlayer.GainGold(goldToGain);
            ListOfTurns.Clear();
            TurnCounter = 0;
        }
    }
}