
namespace ConsoleApp12.Items
{
    public class NoArmour: IArmour
    {
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

    }
}