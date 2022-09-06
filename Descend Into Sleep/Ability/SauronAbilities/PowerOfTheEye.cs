using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.SauronAbilities
{
    public class PowerOfTheEye : Ability
    {
        private const double SANITY_REDUCED = 50;

        public PowerOfTheEye() : base("Power Of The Eye")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            opponent.ReduceSanity(SANITY_REDUCED);
            string toStr = $"{opponent.GetName()} has looked into the Eye of Sauron!\n";
            toStr += $"{opponent.GetName()}'s sanity is reduced by {SANITY_REDUCED}!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetSanity())} sanity!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}