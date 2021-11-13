using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

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
            var randomObject = new Random();
            var sanityReduced = randomObject.Next(MinimumSanityReduced, MaximumSanityReduced);
            opponent.ReduceSanity(sanityReduced);
            var toStr = opponentName + "'s sanity was reduced by " + sanityReduced + "!\n";
            toStr += opponentName + " has " + opponent.GetSanity() + " left!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}