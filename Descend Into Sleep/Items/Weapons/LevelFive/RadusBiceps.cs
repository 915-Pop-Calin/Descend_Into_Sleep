using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class RadusBiceps : IWeapon, IObtainable, ICriticalChance
    {
        public static readonly RadusBiceps RADUS_BICEPS = new RadusBiceps();

        public double GetAttackValue()
        {
            return 75;
        }

        public string GetName()
        {
            return "Radu's Biceps";
        }

        public string GetDescription()
        {
            return "Huge attack value, but it cannot critical strike";
        }


        public double GetCriticalChance()
        {
            return -0.15;
        }

        public double GetPrice()
        {
            return 3700;
        }

        public int AvailabilityLevel()
        {
            return 6;
        }

        private RadusBiceps()
        {
        }
    }
}