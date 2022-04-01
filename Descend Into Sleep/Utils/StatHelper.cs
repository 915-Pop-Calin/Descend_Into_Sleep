using ConsoleApp12.Characters.SideCharacters.LevelOne;

namespace ConsoleApp12.Utils
{
    public class StatHelper
    {
        public double ArmourPenetration;
        public double Attack;
        public double CriticalChance;
        public double Defense;
        public double Health;
        public double LifeSteal;
        public double Sanity;

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