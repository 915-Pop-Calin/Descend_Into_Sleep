using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class WillPower : IArmour, IObtainable, ISanity
    {
        public static readonly WillPower WILL_POWER = new WillPower();

        public string GetName()
        {
            return "Will Power";
        }

        public string GetDescription()
        {
            return $"Gives you {GetSanity()} extra sanity";
        }

        public double GetDefenseValue()
        {
            return 10;
        }

        public double GetSanity()
        {
            return 50;
        }

        public double GetPrice()
        {
            return 800;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private WillPower()
        {
        }
    }
}