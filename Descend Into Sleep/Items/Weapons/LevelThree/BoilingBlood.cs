using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class BoilingBlood : IWeapon, IPassive, IObtainable, ILifeSteal
    {
        public static readonly BoilingBlood BOILING_BLOOD = new BoilingBlood();
        private const double DAMAGE_PER_TURN = 40;

        public double GetAttackValue()
        {
            return 40;
        }

        public string GetName()
        {
            return "Boiling Blood";
        }

        public string GetDescription()
        {
            return $"Very strong life stealer, but you take {DAMAGE_PER_TURN} true damage every turn";
        }

        public double GetLifeSteal()
        {
            return 1.5;
        }

        public string Passive(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster, DAMAGE_PER_TURN);
            var toStr = $"Boiling Blood has dealt {DAMAGE_PER_TURN} true damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 1500;
        }

        public int AvailabilityLevel()
        {
            return 4;
        }

        private BoilingBlood()
        {
        }
    }
}