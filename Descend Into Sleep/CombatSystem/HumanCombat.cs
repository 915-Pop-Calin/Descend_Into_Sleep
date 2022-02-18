using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils.keysWork;

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

        private void CheckAbility()
        {
            var actions = GetActions();
            const string question = "";
            var choice = ConsoleHelper.MultipleChoice(14, question, actions);
            if (choice == actions.Length - 1)
                return;
            var chosenAbilityDescription = Player.GetAbilityDescriptionByName(actions[choice]);
            Console.WriteLine(chosenAbilityDescription);
        }
        
        private bool Ability(Character secondCharacter)
        {
            var actions = GetActions();

            try
            {
                if (actions.Length == 0)
                    throw new NoAbilitiesException();
                const string question = "";
                var choice = ConsoleHelper.MultipleChoice(14, question, actions);
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

        private bool Act(Character secondCharacter)
        {
            List<String> actionsList = secondCharacter.GetActions();
            var actionsLength = actionsList.Count;
            String[] allActions = new String[actionsLength + 1];
            actionsList.CopyTo(allActions);
            allActions[actionsLength] = "back";
            var choice = ConsoleHelper.MultipleChoice(20, "",allActions);
            if (choice == actionsLength)
                return false;
            var actionChoice = allActions[choice];
            var toStr = secondCharacter.Act(actionChoice);
            toStr += Player.ItemEffects(secondCharacter, ListOfTurns, TurnCounter);
            Console.WriteLine(toStr);
            return true;
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var humanPlayer = (HumanPlayer) Player;

            InvalidInput = true;

            while (InvalidInput)
            {
                const string question = "";
                var choice = ConsoleHelper.MultipleChoice(20, question, "attack", "check abilities", "abilities", "check stats", "equip item", "act", "spare");
                // var choice = Utils.keysWork.Utils.MultipleChoice(20, question, "attack", "actions", "check stats", "equip item");
                switch (choice)
                {
                    case 0:
                        var toStr = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
                        Console.WriteLine(toStr);
                        InvalidInput = false;
                        break;
                    case 1:
                        CheckAbility();
                        break;
                    case 2:
                        try
                        {
                            if (Ability(secondCharacter))
                                InvalidInput = false;
                        }
                        catch (NoAbilitiesException noAbilitiesException)
                        {
                            Console.WriteLine(noAbilitiesException.Message);
                        }
                        break;
                    case 3:
                        if (secondCharacter.IsSpareable())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.WriteLine(secondCharacter);
                        Console.ResetColor();
                        break;
                    case 4:
                        try
                        {
                            var itemsString = humanPlayer.GetInventoryItems();
                            itemsString[8] = "back";
                            int option =
                                ConsoleHelper.MultipleChoice(15, "The item you want to equip is:", itemsString);
                            if (option == 8)
                                break;
                            var equippedString = humanPlayer.EquipItem(option);
                            Console.WriteLine(equippedString);
                            InvalidInput = false;
                        }
                        catch (InvalidItemException invalidItemException)
                        {
                            Console.WriteLine(invalidItemException.Message);
                        }
                        catch (InvalidItemTypeException invalidItemTypeException)
                        {
                            Console.WriteLine(invalidItemTypeException.Message);
                        }
                        catch (NullItemException nullEquipException)
                        {
                            Console.WriteLine(nullEquipException.Message);
                        }
                        catch (InventoryOutOfBoundsException inventoryOutOfBoundsException)
                        {
                            Console.WriteLine(inventoryOutOfBoundsException.Message);
                        }
                        break;
                    case 5:
                        InvalidInput = !Act(secondCharacter);
                        break;
                    case 6:
                        try
                        {
                            secondCharacter.Spare();
                            Console.WriteLine($"{secondCharacter.GetName()} has been successfully spared!");
                            InvalidInput = false;
                        }
                        catch (ImpossibleSpareException impossibleSpareException)
                        {
                            Console.WriteLine(impossibleSpareException.Message);
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
            // Console.WriteLine($"You have gained {experienceToGain} experience!\n");
            if (hasLeveled)
                Console.WriteLine($"Level up! Level {humanPlayer.GetLevel()}!\n");
            Console.WriteLine($"You have gained {goldToGain} gold!\n");
            humanPlayer.GainGold(goldToGain);
            ListOfTurns.Clear();
            TurnCounter = 0;
        }
    }
}