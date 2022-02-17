using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.SpaghettiMonsterAbilities
{
    public class Entangle: Ability
    {
        public Entangle() : base("Entangle")
        {
            TurnsUntilDecast = 1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            opponent.Stun();
            var toStr = $"{caster.GetName()} entangles {opponent.GetName()}!\n";
            toStr += $"{opponent.GetName()} is stunned for {TurnsUntilDecast} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            opponent.Unstun();
            var toStr = $"{opponent.GetName()} is no longer stunned!\n";
            return toStr;
        }
    }
}