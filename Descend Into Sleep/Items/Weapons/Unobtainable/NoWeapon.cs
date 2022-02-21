using System.ComponentModel;

namespace ConsoleApp12.Items
{
    public class NoWeapon: Weapon
    {
        public NoWeapon(): base(0, 0, 0)
        {
            Description = "You are wearing no weapon";
            Name = "No Weapon";
        }
        
        public override double GetPrice()
        {
            return -1;
        }
    }
}