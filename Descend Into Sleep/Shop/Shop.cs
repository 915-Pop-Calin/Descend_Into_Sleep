using System;
using System.Collections.Generic;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Items.Potions;
using ConsoleApp12.Items.Weapons.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFour;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Items.Weapons.LevelSix;
using ConsoleApp12.Items.Weapons.LevelThree;
using ConsoleApp12.Items.Weapons.LevelTwo;
using ConsoleApp12.Items.Weapons.Unobtainable;
using ConsoleApp12.Utils;
using ConsoleApp12.Utils.keysWork;

namespace ConsoleApp12.Shop
{
    public static class Shop
    {
        public static readonly Dictionary<int, IItem> ITEMS = new Dictionary<int, IItem>
        {
            {1, NoArmour.NO_ARMOUR},
            {2, NoWeapon.NO_WEAPON},
            {3, HealthPotion.HEALTH_POTION},
            {4, ManaPotion.MANA_POTION},
            {5, GrainOfSalt.GRAIN_OF_SALT},
            {6, ManaElixir.MANA_ELIXIR},
            {7, SanityPotion.SANITY_POTION},
            {8, DefensePotion.DEFENSE_POTION},
            {9, OffensePotion.OFFENSE_POTION},
            {10, Bandage.BANDAGE},
            {11, Cloth.CLOTH},
            {12, TemArmour.TEM_ARMOUR},
            {13, Eclipse.ECLIPSE},
            {14, ToyKnife.TOY_KNIFE},
            {15, Words.WORDS},
            {16, SteelPlateau.STEEL_PLATEAU},
            {17, DoubleEdgedSword.DOUBLE_EDGED_SWORD},
            {18, TacosWhisper.TACOS_WHISPER},
            {19, TitansFindings.TITANS_FINDINGS},
            {20, TwoHandedMace.TWO_HANDED_MACE},
            {21, BootsOfDodge.BOOTS_OF_DODGE},
            {22, LastStand.LAST_STAND},
            {23, BoilingBlood.BOILING_BLOOD},
            {24, LanguageHacker.LANGUAGE_HACKER},
            {25, TankBuster.TANK_BUSTER},
            {26, Xalatath.XALATATH},
            {27, FireDeflector.FIRE_DEFLECTOR},
            {28, Scales.SCALES},
            {29, TidalArmour.TIDAL_ARMOUR},
            {30, GiantSlayer.GIANT_SLAYER},
            {31, IcarusesTouch.ICARUSES_TOUCH},
            {32, EyeOfSauron.EYE_OF_SAURON},
            {33, NinjaYoroi.NINJA_YOROI},
            {34, InfinityEdge.INFINITY_EDGE},
            {35, RadusBiceps.RADUS_BICEPS},
            {36, Dreams.DREAMS},
            {37, TheRing.THE_RING},
            {38, SaroniteScales.SARONITE_SCALES},
            {39, OrbOfTheTitans.ORB_OF_THE_TITANS},
            {40, SaroniteTentacles.SARONITE_TENTACLES},
            {41, WillPower.WILL_POWER}
        };

        public static int FindIdForItem(IItem item)
        {
            foreach (KeyValuePair<int, IItem> element in ITEMS)
            {
                if (element.Value == item)
                    return element.Key;
            }

            return -1;
        }

        private static void PrintCurrentOptions(string type, int level = 1)
        {
            Func<IItem, bool> filterCriteria = GetFilterCriteriaForItem(type, level);
            foreach (KeyValuePair<int, IItem> itemWithId in ITEMS)
            {
                if (filterCriteria(itemWithId.Value))
                {
                    IObtainable item = itemWithId.Value as IObtainable;
                    Console.WriteLine($"{ItemHelper.ItemToString(item)}gold: {item.GetPrice()}\n");
                }
            }
        }

        public static void BuyItem(HumanPlayer player, int level)
        {
            string[] options = new string[] {"weapons", "armours", "potions", "back"};
            int choice = ConsoleHelper.MultipleChoice(15, "Choose the type of the item you want to buy", options);
            if (choice == 3)
                return;

            string type = options[choice];
            int chosenLevel = -1;
            if (type == "potions")
                PrintCurrentOptions(type);
            else
            {
                string[] possibleLevels = new string[level];
                for (int i = 2; i < level + 1; i++)
                    possibleLevels[i - 2] = $"level {i}";
                possibleLevels[level - 1] = "back";

                chosenLevel = ConsoleHelper.MultipleChoice(15, "Choose the level of the item you want to buy",
                    possibleLevels);
                if (chosenLevel == level - 1)
                    return;

                chosenLevel += 2;
                PrintCurrentOptions(type, chosenLevel);
            }

            Console.WriteLine($"{player.GetGold()} gold available\n");

            Console.WriteLine("The item you want to buy is:");
            string readString = Console.ReadLine();
            string[] splitString = readString!.Split("*");

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

            IObtainable item = FindItem(type, chosenItem, chosenLevel);
            for (int i = 0; i < numberOfBuys; i++)
            {
                player.BuyItem(item);
                Console.WriteLine($"You have bought {item.GetName()}!\n");
            }
        }

        public static void SellItem(HumanPlayer player)
        {
            string[] itemsString = player.GetInventoryItems();
            int option = ConsoleHelper.MultipleChoice(15, "The item you want to sell is:", itemsString);
            if (option == 8)
                return;
            string toStr = player.SellItem(option);
            Console.WriteLine(toStr);
            Console.WriteLine($"You have now {player.GetGold()} gold!\n");
        }

        private static IObtainable FindItem(string type, string name, int level = 1)
        {
            Func<IItem, bool> filterCriteria = GetFilterCriteriaForItem(type, level);
            foreach (KeyValuePair<int, IItem> itemWithId in ITEMS)
            {
                IItem item = itemWithId.Value;
                if (item.GetName().ToLower().Equals(name.Trim().ToLower())
                    && filterCriteria(item))
                {
                    return item as IObtainable;
                }
            }

            throw new InvalidBuyException(name);
        }

        private static Func<IItem, bool> GetFilterCriteriaForItem(string type, int level)
        {
            switch (type.ToLower().Trim())
            {
                case "weapons":
                    return (IItem item) =>
                        item is IWeapon &&
                        item is IObtainable obtainable &&
                        obtainable.AvailabilityLevel().Equals(level);
                case "armours":
                    return (IItem item) =>
                        item is IArmour &&
                        item is IObtainable obtainable &&
                        obtainable.AvailabilityLevel().Equals(level);

                case "potions":
                    return (IItem item) =>
                        item is IPotion &&
                        item is IObtainable obtainable &&
                        obtainable.AvailabilityLevel().Equals(level);
                default:
                    return (IItem item) =>
                        false;
            }
        }
    }
}