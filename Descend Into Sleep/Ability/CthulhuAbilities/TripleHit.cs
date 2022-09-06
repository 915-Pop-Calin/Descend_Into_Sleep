using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.CthulhuAbilities
{
    public class TripleHit : Ability
    {
        private const double PERCENTAGE_OF_ATTACK_USED = 0.5;

        public TripleHit() : base("Triple Hit")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = $"{caster.GetName()} has cast Triple Hit!\n";
            double initialAttackValue = caster.GetAttackValue();
            double currentAttackValue = initialAttackValue * PERCENTAGE_OF_ATTACK_USED;

            caster.SetAttackValue(currentAttackValue);
            toStr +=
                $"{caster.GetName()}'s attack has been set to {Math.Round(currentAttackValue, 2)} for the next 3 hits!\n";
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            caster.SetAttackValue(initialAttackValue);

            toStr += $"{caster.GetName()}'s attack has been set back to {Math.Round(initialAttackValue, 2)}!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}