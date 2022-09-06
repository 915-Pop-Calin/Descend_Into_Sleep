using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class Words : IWeapon, IObtainable
    {
        public static readonly Words WORDS = new Words();

        public double GetAttackValue()
        {
            return 0;
        }

        public string GetName()
        {
            return "Words";
        }

        public string GetDescription()
        {
            return "It is said that words cannot hurt you";
        }

        public double GetPrice()
        {
            return 50;
        }

        public int AvailabilityLevel()
        {
            return 2;
        }

        private Words()
        {
        }
    }
}