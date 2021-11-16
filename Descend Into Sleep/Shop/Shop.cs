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
        private List<KeyValuePair<Type, int>> UniversalOptions;
        private Dictionary<int, List<KeyValuePair<Type, int>>> ShopOptions;
        private List<KeyValuePair<Type, int>> TotalOptions;
        private List<KeyValuePair<Type, int>> AllItems;
        private readonly double ReturnRate;

        public Shop(HumanPlayer humanPlayer, int level)
        {
            HumanPlayer = humanPlayer;
            Level = level;

            UniversalOptions = new List<KeyValuePair<Type, int>>();
            UniversalOptions.Add(new KeyValuePair<Type, int>(new HealthPotion().GetType(), 10));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new GrainOfSalt().GetType(), 50));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new DefensePotion().GetType(), 100));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new OffensePotion().GetType(), 100));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new SanityPotion().GetType(), 200));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new ManaPotion().GetType(), 20));
            UniversalOptions.Add(new KeyValuePair<Type, int>(new ManaElixir().GetType(), 100));
            ShopOptions = new Dictionary<int, List<KeyValuePair<Type, int>>>();

            var levelTwoItems = new List<KeyValuePair<Type, int>>();
            levelTwoItems.Add(new KeyValuePair<Type, int>(new TemArmour().GetType(), 150));
            levelTwoItems.Add(new KeyValuePair<Type, int>(new Cloth().GetType(), 100));
            levelTwoItems.Add(new KeyValuePair<Type, int>(new Eclipse().GetType(), 150));
            levelTwoItems.Add(new KeyValuePair<Type, int>(new Words().GetType(), 50));
            levelTwoItems.Add(new KeyValuePair<Type, int>(new ToyKnife().GetType(), 50));
            levelTwoItems.Add(new KeyValuePair<Type, int>(new Bandage().GetType(), 0));
            ShopOptions.Add(2, levelTwoItems);

            var levelThreeItems = new List<KeyValuePair<Type, int>>();
            levelThreeItems.Add(new KeyValuePair<Type, int>(new SteelPlateau().GetType(), 800));
            levelThreeItems.Add(new KeyValuePair<Type, int>(new TacosWhisper().GetType(), 1000));
            levelThreeItems.Add(new KeyValuePair<Type, int>(new TitansFindings().GetType(), 1000));
            levelThreeItems.Add(new KeyValuePair<Type, int>(new DoubleEdgedSword().GetType(), 800));
            levelThreeItems.Add(new KeyValuePair<Type, int>(new TwoHandedMace().GetType(), 750));
            ShopOptions.Add(3, levelThreeItems);

            var levelFourItems = new List<KeyValuePair<Type, int>>();
            levelFourItems.Add(new KeyValuePair<Type, int>(new BootsOfDodge().GetType(), 1500));
            levelFourItems.Add(new KeyValuePair<Type, int>(new BoilingBlood().GetType(), 1500));
            levelFourItems.Add(new KeyValuePair<Type, int>(new TankBuster().GetType(), 1500));
            levelFourItems.Add(new KeyValuePair<Type, int>(new LanguageHacker().GetType(), 1800));
            levelFourItems.Add(new KeyValuePair<Type, int>(new Xalatath().GetType(), 1800));
            levelFourItems.Add(new KeyValuePair<Type, int>(new LastStand().GetType(), 1800));
            ShopOptions.Add(4, levelFourItems);

            var levelFiveItems = new List<KeyValuePair<Type, int>>();
            levelFiveItems.Add(new KeyValuePair<Type, int>(new Scales().GetType(), 1000));
            levelFiveItems.Add(new KeyValuePair<Type, int>(new IcarusesTouch().GetType(), 3600));
            levelFiveItems.Add(new KeyValuePair<Type, int>(new TidalArmour().GetType(), 2800));
            levelFiveItems.Add(new KeyValuePair<Type, int>(new FireDeflector().GetType(), 3200));
            levelFiveItems.Add(new KeyValuePair<Type, int>(new GiantSlayer().GetType(), 3600));
            ShopOptions.Add(5, levelFiveItems);
            
            var levelSixItems = new List<KeyValuePair<Type, int>>();
            levelSixItems.Add(new KeyValuePair<Type, int>(new EyeOfSauron().GetType(), 5000));
            levelSixItems.Add(new KeyValuePair<Type, int>(new InfinityEdge().GetType(), 2500));
            levelSixItems.Add(new KeyValuePair<Type, int>(new RadusBiceps().GetType(), 3700));
            levelSixItems.Add(new KeyValuePair<Type, int>(new NinjaYoroi().GetType(), 5000));
            ShopOptions.Add(6, levelSixItems);
            
            var levelSevenItems = new List<KeyValuePair<Type, int>>();
            levelSevenItems.Add(new KeyValuePair<Type, int>(new TheRing().GetType(), 9000));
            levelSevenItems.Add(new KeyValuePair<Type, int>(new Dreams().GetType(), 2400));
            ShopOptions.Add(7, levelSevenItems);
            
            TotalOptions = new List<KeyValuePair<Type, int>>();

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

            AllItems = new List<KeyValuePair<Type, int>>();
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
                var item = (Item)Activator.CreateInstance(itemType);
                toStr += item + " cost: " + option.Value + " gold\n";
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

        private KeyValuePair<Type, int>? SearchItemByName(string itemName)
        {
            foreach (var itemTypePair in TotalOptions)
            {
                var itemType = itemTypePair.Key;
                var item = (Item) Activator.CreateInstance(itemType);
                if (item.GetName().ToLower() == itemName)
                    return itemTypePair;
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
            
            var itemPairNotNull = (KeyValuePair<Type, int>) itemPair;
            
            var itemType = itemPairNotNull.Key;
            var item = (Item) Activator.CreateInstance(itemType);
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
            var itemPairNotNull = (KeyValuePair<Type, int>) itemPair;
            var itemType = itemPairNotNull.Key;
            var item = (Item) Activator.CreateInstance(itemType);
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
                var itemType = itemTypePair.Key;
                var item = (Item) Activator.CreateInstance(itemType);
                if (item.GetName() == itemName)
                    return itemTypePair.Value;
            }

            return -1;
        }
        
    }
}