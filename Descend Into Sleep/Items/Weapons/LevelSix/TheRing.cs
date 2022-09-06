using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelSix
{
    public class TheRing : IWeapon, IActive, IObtainable
    {
        public static readonly TheRing THE_RING = new TheRing();
        private const int DAMAGE_INCREASE = 1;

        public double GetAttackValue()
        {
            return 1;
        }

        public string GetName()
        {
            return "The Ring";
        }

        public string GetDescription()
        {
            return $"You gain {DAMAGE_INCREASE} attack each attack";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
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
            caster.SetInnateAttack(currentInnateAttack + DAMAGE_INCREASE);
            toStr += $"{caster.GetName()}'s attack was increased by {DAMAGE_INCREASE}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)}";
            return toStr;
        }

        public double GetPrice()
        {
            return 9000;
        }

        public int AvailabilityLevel()
        {
            return 7;
        }

        private TheRing()
        {
        }
    }
}