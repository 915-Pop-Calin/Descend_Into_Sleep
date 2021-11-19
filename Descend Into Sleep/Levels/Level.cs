using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game;
using ConsoleApp12.Game.keysWork;
using ConsoleApp12.SaveFile;

namespace ConsoleApp12.Levels
{
    public class Level
    {
        protected int Number;
        protected Type MainEnemy;
        protected List<Type> SideEnemies;
        protected Shop.Shop Shop;
        protected HumanPlayer Player;
        protected Cheats Cheats;
        protected bool Passed;
        protected bool InCombat;
        protected List<SaveFile.SaveFile> ListOfSaveFiles;

        protected Level(int levelNumber, HumanPlayer humanPlayer)
        {
            Number = levelNumber;
            MainEnemy = null;
            SideEnemies = new List<Type>();
            Shop = null;
            Player = humanPlayer;
            Cheats = new Cheats(Player);
            Passed = false;
            InCombat = false;

            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            ListOfSaveFiles = SaveFile.SaveFile.saveFiles;

        }
        
        private void Explore()
        {
            var choice = 0;
            var randomObject = new Random();
            while (choice != 1)
            {
                Console.WriteLine("Exploring...\n");
                choice = randomObject.Next(1, 4);
                Thread.Sleep(1000);
            }
            var randomSideBoss = randomObject.Next(0, SideEnemies.Count);
            var foundEnemy = (SideEnemy)Activator.CreateInstance(SideEnemies[randomSideBoss]);
            SideBossFight(foundEnemy);
        }

        private void Save()
        {
            if (Player.IsCheater())
                Console.WriteLine("Game cannot be saved because you cheated!\n");
            else
            {
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
                ListOfSaveFiles[saveNumber].SaveInfo(Player, Number);
            }

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
            int choice = ConsoleHelper.MultipleChoice(50,"drop current weapon", "drop current armour");
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

        // returns 2 if we want to proceed, 0 if we want to exit
        private int GameOptions()
        {
            int choice = ConsoleHelper.MultipleChoice(20,"proceed", "explore", "save", "exit", "back");
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
                    Environment.Exit(0);
                    break;
                case 4:
                    break;
            }
            return 1;
        }

        private void PlayerOptions()
        {
            int choice = ConsoleHelper.MultipleChoice(20,"equip item", "drop item", "check stats", "drop current item",
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
            int choice = ConsoleHelper.MultipleChoice(20, "buy", "sell", "back");
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

                var choice = ConsoleHelper.MultipleChoice(20,"game options", "player options", "shop options");
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
                catch (UnopenableSaveFileException unopenableSaveFileException)
                {
                    Console.WriteLine(unopenableSaveFileException.Message);
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

        private void BossFight()
        {
            var mainEnemy = (Character) Activator.CreateInstance(MainEnemy);
            var toStr = "WILD " + mainEnemy.GetName() + " APPEARED!\n";
            Console.WriteLine(toStr);
            Combat(mainEnemy);
        }

        private void SideBossFight(SideEnemy sideEnemy)
        {
            var toStr = sideEnemy.GetName() + " APPEARS INTO THE FRAY!\n";
            Console.WriteLine(toStr);
            Combat(sideEnemy);
        }

        private void Combat(Character enemy)
        {
            var combat = new Fight(Player, enemy);
            combat.Brawl();
            var mainEnemyName = ((Character) Activator.CreateInstance(MainEnemy)).GetName();
            if (enemy.GetName() == mainEnemyName)
                Passed = true;
        }

        public virtual void PlayOut()
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