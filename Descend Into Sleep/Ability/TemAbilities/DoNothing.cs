using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.TemAbilities
{
    public class DoNothing: Ability
    {
        public DoNothing() : base("Do Nothing")
        {
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var toStr = casterName + " does nothing!\n";
            toStr += "It seems pretty ineffective!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}