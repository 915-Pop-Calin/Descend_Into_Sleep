using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.CthulhuAbilities
{
    public class MadnessHit: Ability
    {

        private readonly double LinearDamageEnhancer;
        private readonly double FixedDamageEnhancer;
        
        public MadnessHit() : base("Madness Hit")
        {
            LinearDamageEnhancer = 2;
            FixedDamageEnhancer = 1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var currentSanity = opponent.GetSanity();
            var missingSanity = 100 - currentSanity;
            var percentageMissing = missingSanity / 100;
            var enhancer = FixedDamageEnhancer + LinearDamageEnhancer * percentageMissing;
            var attackValue = caster.GetAttackValue();
            var damageTaken = attackValue * enhancer;
            opponent.ReduceHealthPoints(damageTaken);
            
            var toStr = $"{caster.GetName()} hits everything around it in its madness!\n";
            toStr += $"{opponent.GetName()} has taken {Math.Round(damageTaken, 2)} true damage due to their missing sanity!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}