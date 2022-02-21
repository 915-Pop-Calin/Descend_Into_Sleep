using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class TankBuster: Weapon
    {
        public TankBuster() : base(4, 0, 0)
        {
            SetActive();
            Description = "Each attack strikes twice";
            Name = "Tank Buster";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            caster.DealDirectDamage(opponent, damageDealt);
            var toStr = $"{caster.GetName()} did a double hit and dealt {Math.Round(damageDealt, 2)} damage!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 1500;
        }
    }
}