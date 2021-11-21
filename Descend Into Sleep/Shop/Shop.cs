using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Armours.LevelSix;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Potions;
using ConsoleApp12.Items.Weapons.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFour;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Items.Weapons.LevelSix;
using ConsoleApp12.Items.Weapons.LevelThree;
using ConsoleApp12.Items.Weapons.LevelTwo;

namespace ConsoleApp12.Shop
{
    public class Shop
    {
        private HumanPlayer HumanPlayer;
        private int Level;
        private List<KeyValuePair<Item, int>> UniversalOptions;
        private Dictionary<int, List<KeyValuePair<Item, int>>> ShopOptions;
        private List<KeyValuePair<Item, int>> TotalOptions;
        private List<KeyValuePair<Item, int>> AllItems;
        private readonly double ReturnRate;

        public Shop(HumanPlayer humanPlayer, int level)
        {
            HumanPlayer = humanPlayer;
            Level = level;

            UniversalOptions = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.HealthPotion, 10),
                KeyValuePair.Create<Item, int>(Items.AllItems.GrainOfSalt, 50),
                KeyValuePair.Create<Item, int>(Items.AllItems.DefensePotion, 100),
                KeyValuePair.Create<Item, int>(Items.AllItems.OffensePotion, 100),
                KeyValuePair.Create<Item, int>(Items.AllItems.SanityPotion, 100),
                KeyValuePair.Create<Item, int>(Items.AllItems.ManaPotion, 20),
                KeyValuePair.Create<Item, int>(Items.AllItems.ManaElixir, 100)
            };
            
            var levelTwoItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.TemArmour, 450),
                KeyValuePair.Create<Item, int>(Items.AllItems.Cloth, 100),
                KeyValuePair.Create<Item, int>(Items.AllItems.Eclipse, 400),
                KeyValuePair.Create<Item, int>(Items.AllItems.Words, 50),
                KeyValuePair.Create<Item, int>(Items.AllItems.ToyKnife, 50),
                KeyValuePair.Create<Item, int>(Items.AllItems.Bandage, 0)
            };
            
            var levelThreeItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.SteelPlateau, 800),
                KeyValuePair.Create<Item, int>(Items.AllItems.TacosWhisper, 1000),
                KeyValuePair.Create<Item, int>(Items.AllItems.TitansFindings, 1000),
                KeyValuePair.Create<Item, int>(Items.AllItems.DoubleEdgedSword, 800),
                KeyValuePair.Create<Item, int>(Items.AllItems.TwoHandedMace, 750)
            };
            
            var levelFourItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.BootsOfDodge, 1500),
                KeyValuePair.Create<Item, int>(Items.AllItems.BoilingBlood, 1500),
                KeyValuePair.Create<Item, int>(Items.AllItems.TankBuster, 1500),
                KeyValuePair.Create<Item, int>(Items.AllItems.LanguageHacker, 1800),
                KeyValuePair.Create<Item, int>(Items.AllItems.Xalatath, 1800),
                KeyValuePair.Create<Item, int>(Items.AllItems.LastStand, 1800)
            };
            
            var levelFiveItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.Scales, 1000),
                KeyValuePair.Create<Item, int>(Items.AllItems.IcarusesTouch, 3600),
                KeyValuePair.Create<Item, int>(Items.AllItems.TidalArmour, 2800),
                KeyValuePair.Create<Item, int>(Items.AllItems.FireDeflector, 3200),
                KeyValuePair.Create<Item, int>(Items.AllItems.GiantSlayer, 3600)
            };
            
            var levelSixItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.EyeOfSauron, 5000),
                KeyValuePair.Create<Item, int>(Items.AllItems.InfinityEdge, 2500),
                KeyValuePair.Create<Item, int>(Items.AllItems.RadusBiceps, 3700),
                KeyValuePair.Create<Item, int>(Items.AllItems.NinjaYoroi, 5000)
            };
            
            var levelSevenItems = new List<KeyValuePair<Item, int>>()
            {
                KeyValuePair.Create<Item, int>(Items.AllItems.TheRing, 9000),
                KeyValuePair.Create<Item, int>(Items.AllItems.Dreams, 2400)
            };

            ShopOptions = new Dictionary<int, List<KeyValuePair<Item, int>>>()
            {
                {2, levelTwoItems}, {3, levelThreeItems}, {4, levelFourItems}, {5, levelFiveItems},
                {6, levelSixItems}, {7, levelSevenItems}
            };

            
            TotalOptions = new List<KeyValuePair<Item, int>>();

            foreach (var option in UniversalOptions)
            {
                TotalOptions.Add(option);
            }
            
            for (int i = 2; i <= Level; i++)
            {
                foreach (var item in ShopOptions[i])
                {
                    TotalOptions.Add(item);
                }
            }

            AllItems = new List<KeyValuePair<Item, int>>();
            for (int i = 2; i < 8; i++)
            {
                foreach (var item in ShopOptions[i])
                {
                    AllItems.Add(item);
                }
            }
            foreach (var option in UniversalOptions)
            {
                AllItems.Add(option);
            }

            ReturnRate = 0.75;
        }

        private void PrintOptions()
        {
            var toStr = "";
            foreach (var option in TotalOptions)
            {
                var itemType = option.Key;
                toStr += itemType + " cost: " + option.Value + " gold\n";
            }
            Console.WriteLine(toStr);
            
            Console.WriteLine(HumanPlayer.GetGold() + " gold available\n");
        }

        private void PrintCurrentItems()
        {
            var toStr = "";
            foreach (var currentItem in HumanPlayer.GetInventory())
            {
                if (currentItem != null)
                {
                    var newCost = ReturnRate * FindCostByName(currentItem.GetName());
                    toStr += currentItem + ", gold: " + newCost + "\n";
                }
            }
            Console.WriteLine(toStr);
        }

        private KeyValuePair<Item, int>? SearchItemByName(string itemName)
        {
            foreach (var itemPair in TotalOptions)
            {
                var item = itemPair.Key;
                if (item.GetName().ToLower() == itemName.Trim().ToLower())
                    return itemPair;
            }
            return null;
        }

        public void BuyItem()
        {
            PrintOptions();
            Console.WriteLine("The item you want to buy:\n");
            
            var choice = Console.ReadLine();
            choice = choice.ToLower();
            var splitChoice = choice.Split('x');
            
            if (splitChoice.Length != 1 && splitChoice.Length != 2)
                throw new InvalidBuyingStatementException(choice);

            int numberOfBuys;
            string itemChoice;
            if (splitChoice.Length == 1)
            {
                itemChoice = splitChoice[0].Trim();
                numberOfBuys = 1;
            }
            else
            {
                itemChoice = splitChoice[0].Trim();
                var isParseable = int.TryParse(splitChoice[1].Trim(), out numberOfBuys);
                if (!isParseable)
                    throw new InvalidInputTypeException(typeof(int), typeof(string));
            }

            var itemPair = SearchItemByName(itemChoice);
            if (itemPair is null)
                throw new NotFoundItemException();
            
            var itemPairNotNull = (KeyValuePair<Item, int>) itemPair;
            
            var item = itemPairNotNull.Key;
            var itemCost = itemPairNotNull.Value;
            
            for (int i = 0; i < numberOfBuys; i++)
            {
                HumanPlayer.BuyItem(itemCost, item);
                var writtenLine = "You have bought " + item.GetName() + "!\n";
                Console.WriteLine(writtenLine);
            }

            
        }

        public void SellItem()
        {
            PrintCurrentItems();
            Console.WriteLine("The item you want to sell:\n");
            var choice = Console.ReadLine();
            var itemPair = SearchItemByName(choice);
            if (itemPair is null)
                throw new NotFoundItemException();
            
            var itemPairNotNull = (KeyValuePair<Item, int>) itemPair;
            var item = itemPairNotNull.Key;

            var itemCost = itemPairNotNull.Value;
            var soldCost = ReturnRate * itemCost;
            HumanPlayer.SellItem(soldCost, item);
            var writtenLine = "You have sold " + item.GetName() + "!\n";
            Console.WriteLine(writtenLine);
        }

        private int FindCostByName(string itemName)
        {
            foreach (var itemTypePair in TotalOptions)
            {
                var item = itemTypePair.Key;
                if (item.GetName() == itemName)
                    return itemTypePair.Value;
            }

            return -1;
        }
        
    }
}