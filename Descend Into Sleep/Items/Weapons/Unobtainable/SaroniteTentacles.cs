using System;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.Unobtainable
{
    public class SaroniteTentacles : IWeapon, IReflector, IHealth
    {
        public static readonly SaroniteTentacles SARONITE_TENTACLES = new SaroniteTentacles();
        private bool Broken;
        private double HealthPoints;
        private double AttackValue;
        private string Name;

        public string TakeHit(double attackValue)
        {
            HealthPoints -= attackValue;
            if (HealthPoints <= 0)
            {
                AttackValue = 0;
                Name = "Broken Saronite Tentacles";
                Broken = true;
            }

            var toStr = $"{Name} have taken {Math.Round(attackValue)} damage!\n";
            toStr += $"{Name} are left with {HealthPoints} health!\n";
            return toStr;
        }

        public bool IsBroken()
        {
            return Broken;
        }

        public double GetAttackValue()
        {
            return AttackValue;
        }

        public double GetHealth()
        {
            return HealthPoints;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetDescription()
        {
            return "Tentacles of Sauron";
        }

        private SaroniteTentacles()
        {
            HealthPoints = 100;
            AttackValue = 20;
            Name = "Saronite Tentacles";
            Broken = false;
        }
    }
}