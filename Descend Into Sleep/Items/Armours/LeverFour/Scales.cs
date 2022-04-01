
namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class Scales: IArmour, IObtainable, IHealth
    {
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
    }
}