using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class Cloth : IArmour, IObtainable
    {
        public static readonly Cloth CLOTH = new Cloth();

        public string GetName()
        {
            return "Cloth";
        }

        public string GetDescription()
        {
            return "Strong armour made out of cloth";
        }

        public double GetDefenseValue()
        {
            return 10;
        }


        public double GetPrice()
        {
            return 100;
        }

        public int AvailabilityLevel()
        {
            return 2;
        }

        private Cloth()
        {
        }
    }
}