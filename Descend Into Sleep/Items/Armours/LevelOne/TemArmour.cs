
namespace ConsoleApp12.Items.Armours.LevelOne
{
    public class TemArmour: IArmour, IObtainable
    {
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
    }
}