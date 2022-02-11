using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.YoggSaronAbilities
{
    public class Madness: Ability
    {
        private readonly int MinimumSanityReduced;
        private readonly int MaximumSanityReduced;
        
        public Madness() : base("Madness")
        {
            MinimumSanityReduced = 1;
            MaximumSanityReduced = 21;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var sanityReduced = RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            opponent.ReduceSanity(sanityReduced);
            var toStr = opponentName + "'s sanity was reduced by " + sanityReduced + "!\n";
            toStr += opponentName + " has " + Math.Round(opponent.GetSanity(), 2) + " left!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}