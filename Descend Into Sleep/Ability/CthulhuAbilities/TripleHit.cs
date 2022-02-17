using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.CthulhuAbilities
{
    public class TripleHit: Ability
    {
        private readonly double PercentageOfAttackUsed;
        
        public TripleHit() : base("Triple Hit")
        {
            PercentageOfAttackUsed = 0.5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = $"{caster.GetName()} has cast Triple Hit!\n";
            var initialAttackValue = caster.GetAttackValue();
            var currentAttackValue = initialAttackValue * PercentageOfAttackUsed;
            
            caster.SetAttackValue(currentAttackValue);
            toStr += $"{caster.GetName()}'s attack has been set to {Math.Round(currentAttackValue, 2)} for the next 3 hits!\n";
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            caster.SetAttackValue(initialAttackValue);
            
            toStr += $"{caster.GetName()}'s attack has been set back to {Math.Round(initialAttackValue, 2)}!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}