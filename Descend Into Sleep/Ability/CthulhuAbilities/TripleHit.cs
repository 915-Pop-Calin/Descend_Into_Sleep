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
            var attackValue = caster.GetAttackValue();
            caster.SetAttackValue(PercentageOfAttackUsed * attackValue);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            toStr += caster.Hit(opponent, listOfTurns, turnCounter);
            caster.SetAttackValue(attackValue);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}