using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;
using ConsoleApp12.Utils.keysWork;

namespace ConsoleApp12.Levels
{
    public class Level
    {
        private readonly int Number;
        protected readonly Queue<Character> MainEnemies;
        private readonly Dictionary<Type, int> SideEnemies;
        protected readonly HumanPlayer Player;
        private bool Passed;
        private bool InCombat;
        private readonly List<SaveFile.SaveFile> ListOfSaveFiles;

        protected Level(int levelNumber, HumanPlayer humanPlayer, Dictionary<Type, int> sideEnemies,
            Queue<Character> mainEnemies)
        {
            Number = levelNumber;
            MainEnemies = mainEnemies;
            SideEnemies = sideEnemies;
            Player = humanPlayer;
            Passed = false;
            InCombat = false;

            // why do we check this? remind me again
            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            ListOfSaveFiles = SaveFile.SaveFile.SAVE_FILES;
        }

        private void Explore()
        {
            var found = false;
            while (!found)
            {
                Console.WriteLine("Exploring...\n");
                found = RandomHelper.IsSuccessfulTry(0.25);
                Thread.Sleep(1000);
            }

            var foundEnemy = FindEnemy();
            if (foundEnemy == null)
            {
                Console.WriteLine("But nobody came.");
                return;
            }

            SideBossFight(foundEnemy);
        }

        private List<int> GetNumberOfEnemies()
        {
            var enemies = new List<int>();
            foreach (var tuple in SideEnemies)
            {
                enemies.Add(tuple.Value);
            }

            return enemies;
        }

        private SideEnemy FindEnemy()
        {
            List<int> intervals = new List<int>();
            int current = 0;
            var keys = SideEnemies.Keys;
            foreach (var key in keys)
            {
                intervals.Add(current);
                current += SideEnemies[key];
            }

            if (current == 0)
                return null;

            int choice = RandomHelper.GenerateRandomInInterval(0, current);

            int currentPosition = 0;
            while (currentPosition < intervals.Count && intervals[currentPosition] <= choice)
            {
                currentPosition++;
            }

            currentPosition--;
            int currentIndex = 0;
            foreach (var key in keys)
            {
                if (currentPosition == currentIndex)
                {
                    SideEnemies[key]--;
                    return (SideEnemy) Activator.CreateInstance(key);
                }

                currentIndex++;
            }

            return null;
        }

        private void Save()
        {
            if (Player.IsCheater())
            {
                Console.WriteLine("Game cannot be saved because you cheated!\n");
                return;
            }

            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);

            PrintAllSaveFiles();
            Console.WriteLine("Choose the number of the Save File to Save On:\n");
            var readLine = Console.ReadLine();

            var isParseable = int.TryParse(readLine!.Trim(), out var saveNumber);
            if (!isParseable)
                throw new InvalidInputTypeException(typeof(int), readLine.GetType());
            if (saveNumber < 0 || saveNumber > 9)
                throw new InvalidSaveFileException();

            if (!ListOfSaveFiles[saveNumber].IsEmpty())
            {
                var question = $"Do you want to overwrite Save File {saveNumber}?";
                var choice = ConsoleHelper.MultipleChoice(20, question, "yes", "no");
                if (choice == 1)
                    return;
            }

            var enemies = GetNumberOfEnemies();
            ListOfSaveFiles[saveNumber].SaveInfo(Player, Number, enemies);

            var conclusion = $"You have saved to Save File {saveNumber}";
            Console.WriteLine(conclusion);
        }

        private void SeeInventory()
        {
            var inventory = Player.ShowInventory();
            Console.WriteLine(inventory);
        }

        private void EquipItem()
        {
            var itemsString = Player.GetInventoryItems();
            itemsString[8] = "back";
            int option =
                ConsoleHelper.MultipleChoice(15, "The item you want to equip is:", itemsString);
            if (option == 8)
                return;
            var equippedString = Player.EquipItem(option);
            Console.WriteLine(equippedString);
        }

        private void DropItem()
        {
            var itemsString = Player.GetInventoryItems();
            int option =
                ConsoleHelper.MultipleChoice(15, "The item you want to drop is:", itemsString);
            if (option == 8)
                return;
            var equippedString = Player.DropItem(option);
            Console.WriteLine(equippedString);
        }

        private void CheckStats()
        {
            Console.WriteLine(Player);
        }

        private void DropCurrentItem()
        {
            const string QUESTION = "What do you want to drop?";
            int choice =
                ConsoleHelper.MultipleChoice(50, QUESTION, "drop current weapon", "drop current armour", "back");
            switch (choice)
            {
                case 0:
                    Player.MoveWeaponToInventory();
                    break;
                case 1:
                    Player.MoveArmourToInventory();
                    break;
                case 2:
                    break;
            }
        }

        private void SeeAbilities()
        {
            Console.WriteLine(Player.GetAbilitiesDescription());
        }

        private void BuyItem()
        {
            Shop.Shop.BuyItem(Player, Number);
        }

        private void SellItem()
        {
            Shop.Shop.SellItem(Player);
        }

        private void Exit()
        {
            int choice = ConsoleHelper.MultipleChoice(20, "Are you really sure you want to exit?", "yes", "no");
            if (choice == 0)
                throw new GameOverException();
        }

        private bool GameOptions()
        {
            int choice = ConsoleHelper.MultipleChoice(20, "", "proceed", "explore", "save", "exit", "back");
            switch (choice)
            {
                case 0:
                    return true;
                case 1:
                    Explore();
                    break;
                case 2:
                    Save();
                    break;
                case 3:
                    Exit();
                    break;
                case 4:
                    break;
            }

            return false;
        }

        private void PlayerOptions()
        {
            int choice = ConsoleHelper.MultipleChoice(20, "", "see inventory", "equip item", "drop item",
                "check stats", "drop current item",
                "see abilities", "back");
            switch (choice)
            {
                case 0:
                    SeeInventory();
                    break;
                case 1:
                    EquipItem();
                    break;
                case 2:
                    DropItem();
                    break;
                case 3:
                    CheckStats();
                    break;
                case 4:
                    DropCurrentItem();
                    break;
                case 5:
                    SeeAbilities();
                    break;
                case 6:
                    break;
            }
        }

        private void ShopOptions()
        {
            int choice = ConsoleHelper.MultipleChoice(20, "", "buy", "sell", "back");
            switch (choice)
            {
                case 0:
                    BuyItem();
                    break;
                case 1:
                    SellItem();
                    break;
                case 2:
                    break;
            }
        }

        private void OutOfCombat()
        {
            string decision = null;
            while (decision != "Proceed")
            {
                var choice =
                    ConsoleHelper.MultipleChoice(20, "", "game options", "player options", "shop options");
                try
                {
                    switch (choice)
                    {
                        case 0:
                            var finalResult = GameOptions();
                            if (finalResult)
                            {
                                int requiredLevel = 5 * Number - 1;
                                if (Player.GetLevel() < requiredLevel)
                                    Console.WriteLine(
                                        $"Too low level to proceed, you must be level {requiredLevel}!\n");
                                else
                                    decision = "Proceed";
                            }

                            break;
                        case 1:
                            PlayerOptions();
                            break;
                        case 2:
                            ShopOptions();
                            break;
                    }
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
                catch (InvalidSaveFileException invalidSaveFileException)
                {
                    Console.WriteLine(invalidSaveFileException.Message);
                }
                catch (InvalidItemException invalidItemException)
                {
                    Console.WriteLine(invalidItemException.Message);
                }
                catch (InsufficientGoldException insufficientGoldException)
                {
                    Console.WriteLine(insufficientGoldException.Message);
                }
                catch (NotFoundItemException notFoundItemException)
                {
                    Console.WriteLine(notFoundItemException.Message);
                }
                catch (FullInventoryException fullInventoryException)
                {
                    Console.WriteLine(fullInventoryException.Message);
                }
                catch (InvalidItemDropException invalidItemDropException)
                {
                    Console.WriteLine(invalidItemDropException.Message);
                }
                catch (MaximumLevelException maximumLevelException)
                {
                    Console.WriteLine(maximumLevelException.Message);
                }
                catch (CorruptedSaveFileException corruptedSaveFileException)
                {
                    Console.WriteLine(corruptedSaveFileException.Message);
                }
                catch (InvalidInputTypeException invalidInputTypeException)
                {
                    Console.WriteLine(invalidInputTypeException.Message);
                }
                catch (InvalidBuyException invalidBuyException)
                {
                    Console.WriteLine(invalidBuyException.Message);
                }
                catch (EmptyInventorySellException emptyInventorySellException)
                {
                    Console.WriteLine(emptyInventorySellException.Message);
                }
            }
        }

        protected virtual void BossFight()
        {
            while (MainEnemies.Count != 0)
            {
                var mainEnemy = MainEnemies.Dequeue();
                var toStr = $"WILD {mainEnemy.GetName()} APPEARED!\n";
                Console.WriteLine(toStr);
                Combat(mainEnemy);
            }

            Passed = true;
        }

        private void SideBossFight(SideEnemy sideEnemy)
        {
            var toStr = $"{sideEnemy.GetName()} APPEARS INTO THE FRAY!\n";
            Console.WriteLine(toStr);
            Combat(sideEnemy);
        }

        private void Combat(Character enemy)
        {
            var combat = new Fight(Player, enemy);
            combat.Brawl();
        }

        public void SetEnemies(List<int> enemies)
        {
            if (enemies.Count != SideEnemies.Count)
                throw new InvalidEnemiesNumberException(SideEnemies.Count, enemies.Count);

            int position = 0;
            foreach (var key in SideEnemies.Keys)
            {
                SideEnemies[key] = enemies[position];
                position++;
            }
        }

        public void PlayOut()
        {
            while (!Passed)
            {
                if (InCombat)
                {
                    BossFight();
                }
                else
                {
                    OutOfCombat();
                    InCombat = true;
                }
            }
        }

        private void PrintAllSaveFiles()
        {
            foreach (var saveFile in ListOfSaveFiles)
            {
                Console.WriteLine(saveFile);
            }
        }
    }
}