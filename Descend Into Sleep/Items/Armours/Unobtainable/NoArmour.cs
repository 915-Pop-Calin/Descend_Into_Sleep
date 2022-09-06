using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.Unobtainable
{
    public class NoArmour : IArmour
    {
        public static readonly NoArmour NO_ARMOUR = new NoArmour();

        public string GetName()
        {
            return "No Armour";
        }

        public string GetDescription()
        {
            return "You are wearing no weapon";
        }

        public double GetDefenseValue()
        {
            return 0;
        }

        private NoArmour()
        {
        }
    }
}