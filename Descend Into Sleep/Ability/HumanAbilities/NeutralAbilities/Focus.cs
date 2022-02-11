using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Focus: Ability
    {

        private readonly int MinimumSanityRestored;
        private readonly int MaximumSanityRestored;
        
        public Focus() : base("Focus")
        {
            Description = "You gain some of your sanity back\n";
            ManaCost = 15;
            MinimumSanityRestored = 10;
            MaximumSanityRestored = 41;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var toStr = GetCastingString(caster);
            var sanityRestored = RandomHelper.GenerateRandomInInterval(MinimumSanityRestored, MaximumSanityRestored);
            caster.RestoreSanity(sanityRestored);
            toStr += casterName + " has restored " + sanityRestored + " sanity!\n";
            toStr += casterName + " is left with " + caster.GetSanity() + " sanity!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}