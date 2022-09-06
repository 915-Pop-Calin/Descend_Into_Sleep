using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class InfinityEdge : IWeapon, IObtainable, ICriticalChance
    {
        public static readonly InfinityEdge INFINITY_EDGE = new InfinityEdge();

        public double GetAttackValue()
        {
            return 20;
        }

        public string GetName()
        {
            return "Infinity Edge";
        }

        public string GetDescription()
        {
            return $"Increases your critical strike chance by {GetCriticalChance() * 100}%";
        }

        public double GetCriticalChance()
        {
            return 0.35;
        }

        public double GetPrice()
        {
            return 2500;
        }

        public int AvailabilityLevel()
        {
            return 6;
        }

        private InfinityEdge()
        {
        }
    }
}