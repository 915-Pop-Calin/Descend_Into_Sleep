using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.IcarusAbilities
{
    public class Burn: Ability
    {
        private readonly int NumberOfTurns;
        private readonly double TurnScaling;

        public Burn() : base("Burn")
        {
            NumberOfTurns = 5;
            TurnScaling = 1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var opponentName = opponent.GetName();
            var damagePerTurn = NumberOfTurns * TurnScaling;
            var dotEffect = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            var toStr = casterName + " has cast " + Name + "!\n";
            toStr += opponentName + " will take " + damagePerTurn + " damage every turn for " + NumberOfTurns +
                     " turns!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}