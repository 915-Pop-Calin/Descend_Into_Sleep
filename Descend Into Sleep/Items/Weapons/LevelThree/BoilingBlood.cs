using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class BoilingBlood: Weapon
    {
        public BoilingBlood() : base(40, 0, 0)
        {
            SetEffect();
            SetLifeSteal(1.5);
            Name = "Boiling Blood";
            Description = "Very strong life stealer, but at a great cost";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            caster.DealDirectDamage(caster,40);
            var toStr = $"Boiling Blood has dealt 40 true damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
    }
}