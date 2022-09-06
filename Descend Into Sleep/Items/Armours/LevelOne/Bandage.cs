using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class Bandage : IArmour, IObtainable
    {
        public static readonly Bandage BANDAGE = new Bandage();

        public string GetName()
        {
            return "Worn Bandage";
        }

        public string GetDescription()
        {
            return "Bandaid solution for beginners";
        }

        public double GetDefenseValue()
        {
            return 3;
        }

        public double GetPrice()
        {
            return 50;
        }

        public int AvailabilityLevel()
        {
            return 2;
        }

        private Bandage()
        {
        }
    }
}