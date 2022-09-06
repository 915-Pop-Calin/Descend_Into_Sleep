using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class GiantSlayer : IWeapon, IObtainable, IArmourPenetration
    {
        public static readonly GiantSlayer GIANT_SLAYER = new GiantSlayer();

        public double GetAttackValue()
        {
            return 20;
        }

        public string GetName()
        {
            return "Giant Slayer";
        }

        public string GetDescription()
        {
            return "Great against high armour monsters";
        }

        public double GetArmorPenetration()
        {
            return 0.4;
        }

        public double GetPrice()
        {
            return 3600;
        }

        public int AvailabilityLevel()
        {
            return 5;
        }

        private GiantSlayer()
        {
        }
    }
}