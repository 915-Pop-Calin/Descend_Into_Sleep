using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelSix
{
    public class Dreams : IWeapon, IActive, IObtainable
    {
        public static readonly Dreams DREAMS = new Dreams();
        private const int MINIMUM_SANITY_REDUCED = 15;
        private const int MAXIMUM_SANITY_REDUCED = 26;

        public double GetAttackValue()
        {
            return 2;
        }

        public string GetName()
        {
            return "Dreams";
        }

        public string GetDescription()
        {
            return $"Makes your enemy lose between {MINIMUM_SANITY_REDUCED} and {MAXIMUM_SANITY_REDUCED} sanity," +
                   $" but it has no effect on monsters";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var randomInsanityReduced =
                RandomHelper.GenerateRandomInInterval(MINIMUM_SANITY_REDUCED, MAXIMUM_SANITY_REDUCED);
            opponent.ReduceSanity(randomInsanityReduced);
            var toStr =
                $"{opponent.GetName()}'s sanity was reduced by {randomInsanityReduced}!\n{opponent.GetName()} " +
                $"has {Math.Round(opponent.GetSanity(), 2)} sanity left!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 2400;
        }

        public int AvailabilityLevel()
        {
            return 7;
        }

        private Dreams()
        {
        }
    }
}