using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.Unobtainable
{
    public class NoWeapon : IWeapon
    {
        public static readonly NoWeapon NO_WEAPON = new NoWeapon();

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

        private NoWeapon()
        {
        }
    }
}