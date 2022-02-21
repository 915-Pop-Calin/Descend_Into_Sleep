using System;
using ConsoleApp12.Ability.TemAbilities;

namespace ConsoleApp12.Items.Weapons.Unobtainable
{
    public class SaroniteTentacles: Weapon
    {
        public SaroniteTentacles() : base(20, 0, 100)
        {
            HealthPoints = 100;
            Broken = false;
            SetReflector();
            Name = "Saronite Tentacles";
        }

        public override string TakeHit(double attackValue)
        {
            HealthPoints -= attackValue;
            if (HealthPoints <= 0)
            {
                SetAttackValue(0);
                Name = "Broken Saronite Tentacles";
                Broken = true;
            }

            var toStr = $"{Name} have taken {Math.Round(attackValue)} damage!\n";
            toStr += $"{Name} are left with {HealthPoints} health!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return -1;
        }
    }
}