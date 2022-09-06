using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Focus : Ability
    {
        private const int MINIMUM_SANITY_RESTORED = 25;
        private const int MAXIMUM_SANITY_RESTORED = 46;

        public Focus() : base("Focus")
        {
            ManaCost = 15;
            Description = $"You restore between {MINIMUM_SANITY_RESTORED} and {MAXIMUM_SANITY_RESTORED} sanity back\n";
        }

        public override void ResetDescription()
        {
            Description = $"You restore between {MINIMUM_SANITY_RESTORED} and {MAXIMUM_SANITY_RESTORED} sanity back\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            int sanityRestored =
                RandomHelper.GenerateRandomInInterval(MINIMUM_SANITY_RESTORED, MAXIMUM_SANITY_RESTORED);
            caster.RestoreSanity(sanityRestored);
            toStr += $"{caster.GetName()} has restored {sanityRestored} sanity!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}