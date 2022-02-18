using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game;
using ConsoleApp12.SaveFile;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Levels
{
    public class Level
    {
        protected readonly int Number;
        protected readonly Queue<Character> MainEnemies;
        private readonly Dictionary<Type, int> SideEnemies;
        protected Shop.Shop Shop;
        protected readonly HumanPlayer Player;
        private readonly Cheats Cheats;
        private bool Passed;
        private bool InCombat;
        private readonly List<SaveFile.SaveFile> ListOfSaveFiles;

        public Level(int levelNumber, HumanPlayer humanPlayer, Dictionary<Type, int> sideEnemies, Queue<Character> mainEnemies,
            Shop.Shop shop)
        {
            Number = levelNumber;
            MainEnemies = mainEnemies;
            SideEnemies = sideEnemies;
            Shop = shop;
            Player = humanPlayer;
            Cheats = new Cheats(Player);
            Passed = false;
            InCombat = false;

            // why do we check this? remind me again
            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            ListOfSaveFiles = SaveFile.SaveFile.saveFiles;

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
            
            int saveNumber;
            var isParseable = int.TryParse(readLine.Trim(), out saveNumber);
            if (!isParseable)
                throw new InvalidInputTypeException(typeof(int), readLine.GetType());
            if (saveNumber < 0 || saveNumber > 9)
                throw new InvalidSaveFileException();

            if (!ListOfSaveFiles[saveNumber].IsEmpty())
            {
                var question = $"Do you want to overwrite Save File {saveNumber}?";
                var choice = Utils.keysWork.Utils.MultipleChoice(20, question, "yes", "no");
                if (choice == 1)
                    return;
            }

            var enemies = GetNumberOfEnemies();
            ListOfSaveFiles[saveNumber].SaveInfo(Player, Number, enemies);

            var conclusion = $"You have saved to Save File {saveNumber}";
            Console.WriteLine(conclusion);
            

        }

        private void EquipItem()
        {
            Console.WriteLine(Player.ShowInventory());
            Console.WriteLine("The item you want to equip is:\n");
            var itemChoice = Console.ReadLine();
            if (itemChoice == "Back")
                return;
            var toStr = Player.UseItem(itemChoice);
            Console.WriteLine(toStr);
        }

        private void DropItem()
        {
            Console.WriteLine(Player.ShowInventory());
            Console.WriteLine("The item you want to drop is:\n");
            var itemChoice = Console.ReadLine();
            if (itemChoice == "Back")
                return;
            var toStr = Player.DropItem(itemChoice);
            Console.WriteLine(toStr);
        }

        private void CheckStats()
        {
            Console.WriteLine(Player);
        }

        private void DropCurrentItem()
        {
            Console.WriteLine("What do you want to drop?");
            const string question = "What do you want to drop?";
            int choice = Utils.keysWork.Utils.MultipleChoice(50, question, "drop current weapon", "drop current armour");
            switch (choice)
            {
                case 0:
                    Player.MoveWeaponToInventory();
                    break;
                case 1:
                    Player.MoveArmourToInventory();
                    break;
            }
        }
        
        private void SeeAbilities()
        {
            Console.WriteLine(Player.GetAbilitiesDescription());
        }

        private void BuyItem()
        {
            Shop.BuyItem();
        }

        private void SellItem()
        {
            Shop.SellItem();
        }

        private void Exit()
        {
            int choice = Utils.keysWork.Utils.MultipleChoice(20, "Are you really sure you want to exit?", "yes", "no");
            if (choice == 0)
                throw new GameOverException();
        }
        
        // returns 2 if we want to proceed, 0 if we want to exit
        private int GameOptions()
        {
            const string question = "";
            int choice = Utils.keysWork.Utils.MultipleChoice(20,question, "proceed", "explore", "save", "exit", "back");
            switch (choice)
            {
                case 0:
                    return 2;
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
            return 1;
        }

        private void PlayerOptions()
        {
            const string question = "";
            int choice = Utils.keysWork.Utils.MultipleChoice(20,question, "equip item", "drop item", "check stats", "drop current item",
                "see abilities", "back");
            switch (choice)
            {
                case 0:
                    EquipItem();
                    break;
                case 1:
                    DropItem();
                    break;
                case 2:
                    CheckStats();
                    break;
                case 3:
                    DropCurrentItem();
                    break;
                case 4:
                    SeeAbilities();
                    break;
                case 5:
                    break;
            }
        }

        private void ShopOptions()
        {
            const string question = "";
            int choice = Utils.keysWork.Utils.MultipleChoice(20, question, "buy", "sell", "back");
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

        private void Cheat(string chosenCheat)
        {
            var toStr = Cheats.ListOfCheats[chosenCheat]();
            Player.SetCheater();
            Console.WriteLine(toStr);
        }

        private void OutOfCombat()
        {
            string decision = null;
            while (decision != "Proceed")
            {

                const string question = "";
                var choice = Utils.keysWork.Utils.MultipleChoice(20, question, "game options", "player options", "shop options");
                try
                {
                    switch (choice)
                    {
                        case 0:
                            var finalResult = GameOptions();
                            if (finalResult == 2)
                            {
                                if (Player.GetLevel() < 5 * Number - 1)
                                    Console.WriteLine("Too low level to proceed!\n");
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
                throw new Exception();

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