using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class ToyKnife: IWeapon, IObtainable
    {
        public double GetAttackValue()
        {
            return 3;
        }
        public string GetName()
        {
            return "Toy Knife";
        }

        public string GetDescription()
        {
            return "Is this a game?";
        }

        public double GetPrice()
        {
            return 50;
        }

    }
}