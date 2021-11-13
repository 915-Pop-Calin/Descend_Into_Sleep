using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.CombatSystem
{
    public class HumanCombat: Combat
    {
        public HumanCombat(HumanPlayer humanPlayer): base(humanPlayer)
        {
            ;
        }

        private void PrintMenu()
        {
            Console.WriteLine("Attack\nCheck Stats\nEquip Item\nActions\n");
        }

        private void PrintActions()
        {
            var toStr = "";
            foreach (var ability in Player.GetRespectiveAbilities())
            {
                toStr += ability.Key + "\n";
            }
            Console.WriteLine(toStr);
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var invalidInput = true;
            var humanPlayer = (HumanPlayer) Player;
            while (invalidInput)
            {
                PrintMenu();
                var currentCommand = Console.ReadLine();
                currentCommand = currentCommand.ToLower();
                switch (currentCommand)
                {
                    case "actions":
                        PrintActions();
                        var chosenAction = Console.ReadLine();
                        if (Player.GetRespectiveAbilities().ContainsKey(chosenAction))
                        {
                            try
                            {
                                var toStrCast = Player.Cast(chosenAction, secondCharacter, ListOfTurns, TurnCounter);
                                Console.WriteLine(toStrCast);
                                invalidInput = false;
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
                        else
                        {
                            Console.WriteLine("Invalid Action!\n");
                        }
                        break;
                    case "attack":
                        var toStr = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
                        Console.WriteLine(toStr);
                        invalidInput = false;
                        break;
                    case "check Stats":
                        Console.WriteLine(secondCharacter);
                        break;
                    case "equip Item":
                        try
                        {
                            Console.WriteLine(humanPlayer.ShowInventory());
                            Console.WriteLine("The item you want to equip:\n");
                            var toStrEquip = Console.ReadLine();
                            var toStrEquipped = humanPlayer.UseItem(toStrEquip);
                            Console.WriteLine(toStrEquipped);
                            invalidInput = false;
                        }
                        catch (InvalidItemException invalidItemException)
                        {
                            Console.WriteLine(invalidItemException.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Command!\n");
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