using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class CleanseDOT: Ability
    {
        public CleanseDOT() : base("Cleanse DOT")
        {
            ManaCost = 15;
            Description = "You clear all the DOT effects which are currently affecting you\n";
        }

        public override void ResetDescription()
        {
            Description = "You clear all the DOT effects which are currently affecting you\n";
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            caster.ClearDotEffects();
            toStr += $"{caster.GetName()} cleared all their dot effects!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}