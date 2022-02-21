using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Armours.LevelSix;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Potions;
using ConsoleApp12.Items.Weapons.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFour;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Items.Weapons.LevelSix;
using ConsoleApp12.Items.Weapons.LevelThree;
using ConsoleApp12.Items.Weapons.LevelTwo;
using ConsoleApp12.Items.Weapons.Unobtainable;
using ConsoleApp12.Levels;
using ConsoleApp12.Utils.keysWork;

namespace ConsoleApp12.Items
{
    public class AllItems
    {
        public static readonly NoArmour NoArmour = new NoArmour();
        public static readonly NoWeapon NoWeapon = new NoWeapon();
        public static readonly HealthPotion HealthPotion = new HealthPotion();
        public static readonly ManaPotion ManaPotion = new ManaPotion();
        public static readonly GrainOfSalt GrainOfSalt = new GrainOfSalt();
        public static readonly ManaElixir ManaElixir = new ManaElixir();
        public static readonly SanityPotion SanityPotion = new SanityPotion();
        public static readonly DefensePotion DefensePotion = new DefensePotion();
        public static readonly OffensePotion OffensePotion = new OffensePotion();
        public static readonly Bandage Bandage = new Bandage();
        public static readonly Cloth Cloth = new Cloth();
        public static readonly TemArmour TemArmour = new TemArmour();
        public static readonly Eclipse Eclipse = new Eclipse();
        public static readonly ToyKnife ToyKnife = new ToyKnife();
        public static readonly Words Words = new Words();
        public static readonly SteelPlateau SteelPlateau = new SteelPlateau();
        public static readonly DoubleEdgedSword DoubleEdgedSword = new DoubleEdgedSword();
        public static readonly TacosWhisper TacosWhisper = new TacosWhisper();
        public static readonly TitansFindings TitansFindings = new TitansFindings();
        public static readonly TwoHandedMace TwoHandedMace = new TwoHandedMace();
        public static readonly BootsOfDodge BootsOfDodge = new BootsOfDodge();
        public static readonly LastStand LastStand = new LastStand();
        public static readonly BoilingBlood BoilingBlood = new BoilingBlood();
        public static readonly LanguageHacker LanguageHacker = new LanguageHacker();
        public static readonly TankBuster TankBuster = new TankBuster();
        public static readonly Xalatath Xalatath = new Xalatath();
        public static readonly FireDeflector FireDeflector = new FireDeflector();
        public static readonly Scales Scales = new Scales();
        public static readonly TidalArmour TidalArmour = new TidalArmour();
        public static readonly GiantSlayer GiantSlayer = new GiantSlayer();
        public static readonly IcarusesTouch IcarusesTouch = new IcarusesTouch();
        public static readonly EyeOfSauron EyeOfSauron = new EyeOfSauron();
        public static readonly NinjaYoroi NinjaYoroi = new NinjaYoroi();
        public static readonly InfinityEdge InfinityEdge = new InfinityEdge();
        public static readonly RadusBiceps RadusBiceps = new RadusBiceps();
        public static readonly Dreams Dreams = new Dreams();
        public static readonly TheRing TheRing = new TheRing();
        public static readonly SaroniteScales SaroniteScales = new SaroniteScales();
        public static readonly OrbOfTheTitans OrbOfTheTitans = new OrbOfTheTitans();
        public static readonly SaroniteTentacles SaroniteTentacles = new SaroniteTentacles();
        public static readonly WillPower WillPower = new WillPower();

        public static readonly Dictionary<int, Item> Items = new Dictionary<int, Item>
        {
            {1, NoArmour}, {2, NoWeapon}, {3, HealthPotion}, {4, ManaPotion}, {5, GrainOfSalt}, {6, ManaElixir},
            {7, SanityPotion}, {8, DefensePotion}, {9, OffensePotion}, {10, Bandage}, {11, Cloth}, {12, TemArmour},
            {13, Eclipse}, {14, ToyKnife}, {15, Words}, {16, SteelPlateau}, {17, DoubleEdgedSword}, {18, TacosWhisper},
            {19, TitansFindings}, {20, TwoHandedMace}, {21, BootsOfDodge}, {22, LastStand}, {23, BoilingBlood}, 
            {24, LanguageHacker}, {25, TankBuster}, {26, Xalatath}, {27, FireDeflector}, {28, Scales}, {29, TidalArmour},
            {30, GiantSlayer}, {31, IcarusesTouch}, {32, EyeOfSauron}, {33, NinjaYoroi}, {34, InfinityEdge},
            {35, RadusBiceps}, {36, Dreams}, {37, TheRing}, {38, SaroniteScales}, {39, OrbOfTheTitans}, {40, SaroniteTentacles},
            {41, WillPower}
        };

        private static readonly Dictionary<int, List<Item>> Weapons = new Dictionary<int, List<Item>>
        {
            {2, new List<Item> {Eclipse, ToyKnife, Words}},
            {3, new List<Item> {DoubleEdgedSword, TacosWhisper, TitansFindings, TwoHandedMace}},
            {4, new List<Item> {BoilingBlood, LanguageHacker, TankBuster, Xalatath}},
            {5, new List<Item> {GiantSlayer, IcarusesTouch}},
            {6, new List<Item> {InfinityEdge, RadusBiceps}},
            {7, new List<Item> {Dreams, TheRing}}
        };

        private static readonly Dictionary<int, List<Item>> Armours = new Dictionary<int, List<Item>>
        {
            {2, new List<Item>{Bandage, Cloth, TemArmour}},
            {3, new List<Item>{SteelPlateau, WillPower}},
            {4, new List<Item>{BootsOfDodge, LastStand}},
            {5, new List<Item>{FireDeflector, Scales, TidalArmour}},
            {6, new List<Item>{EyeOfSauron, NinjaYoroi}},
            {7, new List<Item>{}}
        };

        private static readonly List<Item> Potions = new List<Item>
        {
            DefensePotion, GrainOfSalt, HealthPotion, ManaElixir, ManaPotion, OffensePotion, SanityPotion
        };
        
        public static int FindIdForItem(Item item)
        {
            foreach (var element in Items)
            {
                if (element.Value == item)
                    return element.Key;
                
            }
            return -1;
        }

        private static void PrintCurrentOptions(string type, int level = -1)
        {
            switch (type)
            {
                case "weapons":
                    foreach (var weapon in Weapons[level])
                        Console.WriteLine($"{weapon}gold: {weapon.GetPrice()}\n");
                    break;
                case "armours":
                    foreach (var armour in Armours[level])
                        Console.WriteLine($"{armour}gold: {armour.GetPrice()}\n");
                    break;
                case "potions":
                    foreach (var potion in Potions)
                        Console.WriteLine($"{potion}gold: {potion.GetPrice()}\n");
                    break;
            }
        }

        public static void BuyItem(HumanPlayer player, int level)
        {
            var options = new string[] {"weapons", "armours", "potions", "back"};
            var choice = ConsoleHelper.MultipleChoice(15, "Choose the type of the item you want to buy", options);
            if (choice == 3)
                return;

            var type = options[choice];
            var chosenLevel = -1;
            if (type == "potions")
                PrintCurrentOptions(type);
            else
            {
                var possibleLevels = new string[level];
                for (var i = 2; i < level + 1; i++)
                    possibleLevels[i - 2] = $"level {i}";
                possibleLevels[level - 1] = "back";

                chosenLevel = ConsoleHelper.MultipleChoice(15, "Choose the level of the item you want to buy",
                    possibleLevels);
                if (chosenLevel == level - 1)
                    return;
                
                chosenLevel += 2;
                PrintCurrentOptions(type, chosenLevel);
                
            }
            
            Console.WriteLine("The item you want to buy is:");
            var readString = Console.ReadLine();
            var splitString = readString!.Split("*");
            
            if (splitString.Length != 1 && splitString.Length != 2)
                throw new InvalidBuyingStatementException(readString);

            int numberOfBuys;
            string chosenItem;
            if (splitString.Length == 1)
            {
                chosenItem = splitString[0];
                numberOfBuys = 1;
            }
            else
            {
                chosenItem = splitString[0];
                if (!int.TryParse(splitString[1].Trim(), out numberOfBuys))
                    throw new InvalidInputTypeException(typeof(int), typeof(string));
            }
            
            var item = FindItem(type, chosenItem, chosenLevel);
            for (var i = 0; i < numberOfBuys; i++)
            {
                player.BuyItem(item);
                Console.WriteLine($"You have bought {item.GetName()}!\n");
            }
        }

        public static void SellItem(HumanPlayer player)
        {
            var itemsString = player.GetInventoryItems();
            var option = ConsoleHelper.MultipleChoice(15, "The item you want to sell is:", itemsString);
            if (option == 8)
                return;
            var toStr = player.SellItem(option);
            Console.WriteLine(toStr);
        }
        
        private static Item FindItem(string type, string name, int level = -1)
        {
            switch (type)
            {
                case "potions":
                    foreach (var potion in Potions)
                        if (potion.GetName().ToLower().Equals(name.Trim().ToLower()))
                            return potion;
                    throw new InvalidBuyException(name);
                case "weapons":
                    foreach (var weapon in Weapons[level])
                        if (weapon.GetName().ToLower().Equals(name.Trim().ToLower()))
                            return weapon;
                    throw new InvalidBuyException(name);
                case "armours":
                    foreach (var armour in Armours[level])
                        if (armour.GetName().ToLower().Equals(name.Trim().ToLower()))
                            return armour;
                    throw new InvalidBuyException(name);
            }
            return null;
        }
    }
}