
namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class WillPower: IArmour, IObtainable, ISanity
    {
        public string GetName()
        {
            return "Will Power";
        }

        public string GetDescription()
        {
            return $"Gives you {GetSanity()} extra sanity";
        }
        
        public double GetDefenseValue()
        {
            return 10;
        }
        
        public double GetSanity()
        {
            return 50;
        }


        
        public double GetPrice()
        {
            return 800;
        }
    }
}