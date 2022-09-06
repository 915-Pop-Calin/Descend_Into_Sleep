using ConsoleApp12.Characters.SideCharacters.LevelOne;

namespace ConsoleApp12.Utils
{
    public class StatHelper
    {
        public readonly double ArmourPenetration;
        public readonly double Attack;
        public readonly double CriticalChance;
        public readonly double Defense;
        public readonly double Health;
        public readonly double LifeSteal;
        public readonly double Sanity;

        public StatHelper(double attack, double defense = 0, double health = 0, double armourPenetration = 0, double criticalChance = 0, double sanity = 0)
        {
            Attack = attack;
            Health = health;
            ArmourPenetration = armourPenetration;
            CriticalChance = criticalChance;
            Defense = defense;
            Sanity = sanity;
        }
    }
}