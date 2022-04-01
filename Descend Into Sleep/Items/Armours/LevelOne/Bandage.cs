
namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class Bandage: IArmour, IObtainable
    {
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
    }
}