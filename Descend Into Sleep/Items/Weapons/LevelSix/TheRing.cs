using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LevelSix
{
    public class TheRing: Weapon
    {
        private readonly int DamageIncrease;
        
        public TheRing() : base(1, 0, 0)
        {
            SetActive();
            Name = "The Ring";
            DamageIncrease = 1;
            Description = $"You gain {DamageIncrease} attack each attack";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            var attackValue = GetAttackValue();
            var willCast = RandomHelper.IsSuccessfulTry(0.2);
            var toStr = "";
            if (willCast)
            {
                opponent.ReduceSanity(attackValue);
                toStr = $"{opponent.GetName()}'s sanity was reduced by {attackValue}!\n";
                toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetSanity(), 2)} sanity!\n";
            }

            var currentInnateAttack = caster.GetInnateAttack();
            caster.SetInnateAttack(currentInnateAttack + DamageIncrease);
            toStr += $"{caster.GetName()}'s attack was increased by {DamageIncrease}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)}";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 9000;
        }
    }
}