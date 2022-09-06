using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class BootsOfDodge : IArmour, IObtainable, IDodge
    {
        public static readonly BootsOfDodge BOOTS_OF_DODGE = new BootsOfDodge();

        public string GetName()
        {
            return "Boots Of Dodge";
        }

        public string GetDescription()
        {
            return $"Gives you {GetDodge() * 100}% chances of dodging auto attacks";
        }

        public double GetDefenseValue()
        {
            return 10;
        }


        public double GetDodge()
        {
            return 0.15;
        }

        public double GetPrice()
        {
            return 1500;
        }

        public int AvailabilityLevel()
        {
            return 4;
        }

        private BootsOfDodge()
        {
        }
    }
}