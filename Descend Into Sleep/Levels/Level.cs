using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game;

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
        protected bool Dead;
        protected bool Passed;
        protected bool InCombat;
        protected List<SaveFile.SaveFile> ListOfSaveFiles;
        protected bool Exit;
        
        public Level(int levelNumber, HumanPlayer humanPlayer)
        {
            Number = levelNumber;
            MainEnemy = null;
            SideEnemies = new List<Type>();
            Shop = null;
            Player = humanPlayer;
            Cheats = new Cheats(Player);
            Dead = false;
            Passed = false;
            InCombat = false;
            
            ListOfSaveFiles = new List<SaveFile.SaveFile>();
            for (int i = 0; i <= 9; i++)
            {
                ListOfSaveFiles.Add(new SaveFile.SaveFile(i));
            }

            Exit = false;
        }

        private void PrintMenu()
        {
            Console.WriteLine("Game Options\nPlayer Options\nShop Options\n");
        }

        private void PrintGameOptions()
        {
            Console.WriteLine("Proceed\nExplore\nSave\nExit\nBack\n");
        }

        private void PrintPlayerOptions()
        {
            Console.WriteLine("Equip Item\nDrop Item\nCheck Stats\nDrop Current Item\nSee Abilities\nBack\n");
        }

        private void PrintShopOptions()
        {
            Console.WriteLine("Buy\nSell\nBack\n");
        }

        private int Explore()
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
            return SideBossFight(foundEnemy);
        }

        private void Save()
        {
            if (Player.IsCheater())
                Console.WriteLine("Game cannot be saved because you cheated!\n");
            else
            {
                PrintAllSaveFiles();
                Console.WriteLine("Choose the number of the Save File to Save On:\n");
                var readLine = Console.ReadLine();
                int saveNumber;
                try
                {
                    saveNumber = Convert.ToInt32(readLine);
                }
                catch (FormatException)
                {
                    throw new InvalidInputTypeException(typeof(int), readLine.GetType());
                }
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
            Console.WriteLine("Do you want to drop your weapon or your armour?W/A\n");
            var droppingChoice = Console.ReadLine();
            switch (droppingChoice)
            {
                case "w":
                    Player.MoveWeaponToInventory();
                    break;
                case "a":
                    Player.MoveArmourToInventory();
                    break;
                default:
                    Console.WriteLine("Invalid Input!\n");
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
            PrintGameOptions();
            var gameChoice = Console.ReadLine();
            switch (gameChoice)
            {
                case "Proceed":
                    return 2;
                case "Back":
                    break;
                case "Explore":
                    return Explore();
                case "Save":
                    Save();
                    break;
                case "Exit":
                    Exit = true;
                    return 0;
                default:
                    Console.WriteLine("Invalid Command!\n");
                    break;
            }
            return 1;
        }

        private void PlayerOptions()
        {
            PrintPlayerOptions();
            var playerChoice = Console.ReadLine();
            switch (playerChoice)
            {
                case "Equip Item":
                    EquipItem();
                    break;
                case "Drop Item":
                    DropItem();
                    break;
                case "Check Stats":
                    CheckStats();
                    break;
                case "Drop Current Item":
                    DropCurrentItem();
                    break;
                case "See Abilities":
                    SeeAbilities();
                    break;
                case "Back":
                    ;
                    break;
                default:
                    Console.WriteLine("Invalid Command!\n");
                    break;
            }
        }

        private void ShopOptions()
        {
            PrintShopOptions();
            var shopChoice = Console.ReadLine();
            switch (shopChoice)
            {
                case "Buy":
                    BuyItem();
                    break;
                case "Sell":
                    SellItem();
                    break;
                case "Back":
                    ;
                    break;
                default:
                    Console.WriteLine("Invalid Command!\n");
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
            while (decision != "Proceed" && !Exit)
            {
                PrintMenu();
                var gameChoice = Console.ReadLine();
                try
                {
                    switch (gameChoice)
                    {
                        case "Game Options":
                            var finalResult = GameOptions();
                            if (finalResult == 0)
                                Exit = true;
                            if (finalResult == 2)
                            {
                                if (Player.GetLevel() < 5 * Number - 1)
                                    Console.WriteLine("Too low level to proceed!\n");
                                else
                                    decision = "Proceed";
                            }

                            if (finalResult == -1)
                            {
                                Exit = true;
                                Dead = true;
                            }

                            break;
                        case "Player Options":
                            PlayerOptions();
                            break;
                        case "Shop Options":
                            ShopOptions();
                            break;
                        default:
                            if (Cheats.ListOfCheats.ContainsKey(gameChoice))
                                Cheat(gameChoice);
                            else
                                Console.WriteLine("Invalid Command!\n");
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

        private int SideBossFight(SideEnemy sideEnemy)
        {
            var toStr = sideEnemy.GetName() + " APPEARS INTO THE FRAY!\n";
            Console.WriteLine(toStr);
            return Combat(sideEnemy);
        }

        private int Combat(Character enemy)
        {
            var combat = new Fight(Player, enemy);
            combat.Brawl();
            if (combat.IsDead())
            {
                Dead = true;
                return -1;
            }

            var mainEnemyName = ((Character) Activator.CreateInstance(MainEnemy)).GetName();
            if (enemy.GetName() == mainEnemyName)
                Passed = true;
            return 1;
        }

        public virtual int PlayOut()
        {
            while (!Dead && !Passed)
            {
                if (InCombat)
                {
                    if (Exit)
                        return -1;
                    else
                        BossFight();
                }
                else
                {
                    OutOfCombat();
                    if (Exit)
                        return -1;
                    InCombat = true;
                }
            }

            if (Dead)
                return -1;
            return 0;
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