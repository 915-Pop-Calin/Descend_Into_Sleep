
namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class Cloth: IArmour, IObtainable
    {
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
    }
}