using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LevelFive
{
    public class NinjaYoroi : IArmour, IObtainable, IDodge
    {
        public static readonly NinjaYoroi NINJA_YOROI = new NinjaYoroi();

        public string GetName()
        {
            return "Ninja Yoroi";
        }

        public string GetDescription()
        {
            return $"Armour with no defense points but gives {100 * GetDodge()}% dodge chance";
        }

        public double GetDefenseValue()
        {
            return 0;
        }

        public double GetDodge()
        {
            return 0.5;
        }

        public double GetPrice()
        {
            return 5000;
        }

        public int AvailabilityLevel()
        {
            return 6;
        }

        private NinjaYoroi()
        {
        }
    }
}