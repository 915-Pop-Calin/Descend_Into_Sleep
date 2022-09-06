using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class TemArmour : IArmour, IObtainable
    {
        public static readonly TemArmour TEM_ARMOUR = new TemArmour();

        public string GetName()
        {
            return "Tem Armour";
        }

        public string GetDescription()
        {
            return "Strongest armour ever crafted by cats";
        }

        public double GetDefenseValue()
        {
            return 100;
        }

        public double GetPrice()
        {
            return 450;
        }

        public int AvailabilityLevel()
        {
            return 2;
        }

        private TemArmour()
        {
        }
    }
}