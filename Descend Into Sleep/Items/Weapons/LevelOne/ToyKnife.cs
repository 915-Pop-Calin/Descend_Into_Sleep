using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class ToyKnife : IWeapon, IObtainable
    {
        public static readonly ToyKnife TOY_KNIFE = new ToyKnife();

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

        public int AvailabilityLevel()
        {
            return 2;
        }

        private ToyKnife()
        {
        }
    }
}