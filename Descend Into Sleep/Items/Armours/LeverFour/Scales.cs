using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class Scales : IArmour, IObtainable, IHealth
    {
        public static readonly Scales SCALES = new Scales();

        public string GetName()
        {
            return "Scales";
        }

        public string GetDescription()
        {
            return "Scales of C'Thulhu. Does not serve as a great armour";
        }

        public double GetDefenseValue()
        {
            return 20;
        }

        public double GetHealth()
        {
            return 100;
        }

        public double GetPrice()
        {
            return 1000;
        }

        public int AvailabilityLevel()
        {
            return 5;
        }

        private Scales()
        {
        }
    }
}