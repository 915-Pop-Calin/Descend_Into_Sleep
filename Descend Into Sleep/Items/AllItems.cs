using System.Collections.Generic;
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

namespace ConsoleApp12.Items
{
    public class AllItems
    {
        public readonly static NoArmour NoArmour = new NoArmour();
        public readonly static NoWeapon NoWeapon = new NoWeapon();
        public readonly static HealthPotion HealthPotion = new HealthPotion();
        public readonly static ManaPotion ManaPotion = new ManaPotion();
        public readonly static GrainOfSalt GrainOfSalt = new GrainOfSalt();
        public readonly static ManaElixir ManaElixir = new ManaElixir();
        public readonly static SanityPotion SanityPotion = new SanityPotion();
        public readonly static DefensePotion DefensePotion = new DefensePotion();
        public readonly static OffensePotion OffensePotion = new OffensePotion();
        public readonly static Bandage Bandage = new Bandage();
        public readonly static Cloth Cloth = new Cloth();
        public readonly static TemArmour TemArmour = new TemArmour();
        public readonly static Eclipse Eclipse = new Eclipse();
        public readonly static ToyKnife ToyKnife = new ToyKnife();
        public readonly static Words Words = new Words();
        public readonly static SteelPlateau SteelPlateau = new SteelPlateau();
        public readonly static DoubleEdgedSword DoubleEdgedSword = new DoubleEdgedSword();
        public readonly static TacosWhisper TacosWhisper = new TacosWhisper();
        public readonly static TitansFindings TitansFindings = new TitansFindings();
        public readonly static TwoHandedMace TwoHandedMace = new TwoHandedMace();
        public readonly static BootsOfDodge BootsOfDodge = new BootsOfDodge();
        public readonly static LastStand LastStand = new LastStand();
        public readonly static BoilingBlood BoilingBlood = new BoilingBlood();
        public readonly static LanguageHacker LanguageHacker = new LanguageHacker();
        public readonly static TankBuster TankBuster = new TankBuster();
        public readonly static Xalatath Xalatath = new Xalatath();
        public readonly static FireDeflector FireDeflector = new FireDeflector();
        public readonly static Scales Scales = new Scales();
        public readonly static TidalArmour TidalArmour = new TidalArmour();
        public readonly static GiantSlayer GiantSlayer = new GiantSlayer();
        public readonly static IcarusesTouch IcarusesTouch = new IcarusesTouch();
        public readonly static EyeOfSauron EyeOfSauron = new EyeOfSauron();
        public readonly static NinjaYoroi NinjaYoroi = new NinjaYoroi();
        public readonly static InfinityEdge InfinityEdge = new InfinityEdge();
        public readonly static RadusBiceps RadusBiceps = new RadusBiceps();
        public readonly static Dreams Dreams = new Dreams();
        public readonly static TheRing TheRing = new TheRing();
        public readonly static SaroniteScales SaroniteScales = new SaroniteScales();
        public readonly static OrbOfTheTitans OrbOfTheTitans = new OrbOfTheTitans();
        public readonly static SaroniteTentacles SaroniteTentacles = new SaroniteTentacles();

        public static Dictionary<int, Item> Items = new Dictionary<int, Item>()
        {
            {1, NoArmour}, {2, NoWeapon}, {3, HealthPotion}, {4, ManaPotion}, {5, GrainOfSalt}, {6, ManaElixir},
            {7, SanityPotion}, {8, DefensePotion}, {9, OffensePotion}, {10, Bandage}, {11, Cloth}, {12, TemArmour},
            {13, Eclipse}, {14, ToyKnife}, {15, Words}, {16, SteelPlateau}, {17, DoubleEdgedSword}, {18, TacosWhisper},
            {19, TitansFindings}, {20, TwoHandedMace}, {21, BootsOfDodge}, {22, LastStand}, {23, BoilingBlood}, 
            {24, LanguageHacker}, {25, TankBuster}, {26, Xalatath}, {27, FireDeflector}, {28, Scales}, {29, TidalArmour},
            {30, GiantSlayer}, {31, IcarusesTouch}, {32, EyeOfSauron}, {33, NinjaYoroi}, {34, InfinityEdge},
            {35, RadusBiceps}, {36, Dreams}, {37, TheRing}, {38, SaroniteScales}, {39, OrbOfTheTitans}, {40, SaroniteTentacles}
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
    }
}