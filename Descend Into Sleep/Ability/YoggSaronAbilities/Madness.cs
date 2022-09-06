using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.YoggSaronAbilities
{
    public class Madness : Ability
    {
        private const int MINIMUM_SANITY_REDUCED = 1;
        private const int MAXIMUM_SANITY_REDUCED = 21;

        public Madness() : base("Madness")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            int sanityReduced = RandomHelper.GenerateRandomInInterval(MINIMUM_SANITY_REDUCED, MAXIMUM_SANITY_REDUCED);
            opponent.ReduceSanity(sanityReduced);
            string toStr = $"{opponent.GetName()}'s sanity was reduced by {sanityReduced}!\n";
            toStr += $"{opponent.GetName()} has {Math.Round(opponent.GetSanity(), 2)} left!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}