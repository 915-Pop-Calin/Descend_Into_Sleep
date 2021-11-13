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
            var casterName = caster.GetName();
            var opponentName = opponent.GetName();
            opponent.Stun();
            var toStr = casterName + " casts " + Name + "!\n";
            toStr += opponentName + " is stunned for " + TurnsUntilDecast + " turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var opponentName = opponent.GetName();
            opponent.Unstun();
            var toStr = opponentName + " is no longer stunned!\n";
            return toStr;
        }
    }
}