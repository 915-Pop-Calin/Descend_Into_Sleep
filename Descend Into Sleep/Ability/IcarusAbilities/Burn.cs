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
            var damagePerTurn = NumberOfTurns * TurnScaling;
            var dotEffect = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            var toStr = $"{caster.GetName()} burns everything around it!\n";
            toStr += $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage every turn for {NumberOfTurns} turns!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}