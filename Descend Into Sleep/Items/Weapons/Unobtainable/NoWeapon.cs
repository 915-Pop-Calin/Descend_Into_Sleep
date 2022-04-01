using System.ComponentModel;

namespace ConsoleApp12.Items
{
    public class NoWeapon: IWeapon
    {
        public double GetAttackValue()
        {
            return 0;
        }
        
        public string GetName()
        {
            return "No Weapon";
        }

        public string GetDescription()
        {
            return "You are wearing no weapon";
        }
    }
}