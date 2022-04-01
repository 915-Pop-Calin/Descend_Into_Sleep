using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class Eclipse: IWeapon, IObtainable, ILifeSteal, IDefense
    {

        public double GetAttackValue()
        {
            return 5;
        }

        public double GetDefenseValue()
        {
            return -3;
        }

        public string GetName()
        {
            return "Eclipse";
        }

        public string GetDescription()
        {
            return "Strong life stealer at the cost of your defense";
        }
        public double GetPrice()
        {
            return 400;
        }

        public double GetLifeSteal()
        {
            return 0.15;
        }
    }
}