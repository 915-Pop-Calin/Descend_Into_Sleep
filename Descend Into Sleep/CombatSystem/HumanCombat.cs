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
            var abilitiesString = new String[length];
            var currentPosition = 0;
            
            foreach (var ability in Player.GetRespectiveAbilities())
            {
                abilitiesString[currentPosition] = ability.Key;
                currentPosition += 1;
            }

            return abilitiesString;
        }

        private void Action(Character secondCharacter)
        {
            var actions = GetActions();

            try
            {
                if (actions.Length == 0)
                    throw new NoAbilitiesException();
                var choice = ConsoleHelper.MultipleChoice(15, actions);
                var chosenAbilityKey = actions[choice];
                var toStrCast = Player.Cast(chosenAbilityKey, secondCharacter, ListOfTurns, TurnCounter);
                Console.WriteLine(toStrCast);
                InvalidInput = false;
            }
            catch (NoAbilitiesException noAbilitiesException)
            {
                Console.WriteLine(noAbilitiesException.Message);
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
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var humanPlayer = (HumanPlayer) Player;
            var choice = ConsoleHelper.MultipleChoice(20, "actions", "attack", "check stats", "equip item");
            InvalidInput = true;

            while (InvalidInput)
            {
                switch (choice)
                {
                    case 0:
                        Action(secondCharacter);
                        break;
                    case 1:
                        var toStr = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
                        Console.WriteLine(toStr);
                        InvalidInput = false;
                        break;
                    case 2:
                        Console.WriteLine(secondCharacter);
                        InvalidInput = false;
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