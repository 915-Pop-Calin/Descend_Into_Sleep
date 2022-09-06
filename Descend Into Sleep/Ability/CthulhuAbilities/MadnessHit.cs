using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.CthulhuAbilities
{
    public class MadnessHit : Ability
    {
        private const double LINEAR_DAMAGE_ENHANCER = 2;
        private const double FIXED_DAMAGE_ENHANCER = 1;

        public MadnessHit() : base("Madness Hit")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double percentageMissing =
                (opponent.GetMaximumSanity() - opponent.GetSanity()) / opponent.GetMaximumSanity();
            double enhancer = FIXED_DAMAGE_ENHANCER + LINEAR_DAMAGE_ENHANCER * percentageMissing;
            double damageTaken = caster.GetAttackValue() * enhancer;
            opponent.ReduceHealthPoints(damageTaken);

            string toStr = $"{caster.GetName()} hits everything around it in its madness!\n";
            toStr +=
                $"{opponent.GetName()} has taken {Math.Round(damageTaken, 2)} true damage due to their missing sanity!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}