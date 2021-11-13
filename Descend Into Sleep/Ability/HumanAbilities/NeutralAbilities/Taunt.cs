using System;
using System.Collections.Generic;
using System.Security;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Taunt: Ability
    {
        public Taunt() : base("Taunt")
        {
            Description = "Your opponent's attack and defense values are decreased for 3 turns\n";
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var valueDecreased = Math.Pow(ScalingPerLevel, Level);
            if (opponent.GetAttackValue() < valueDecreased || opponent.GetDefenseValue() < valueDecreased)
                throw new NegativeAttackException(opponentName);
            var toStr = GetCastingString(caster);
            opponent.DecreaseAttackValue(valueDecreased);
            opponent.DecreaseDefenseValue(valueDecreased);
            toStr += opponentName + "'s attack and defense values were decreased by " + valueDecreased +
                     "!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var opponentName = opponent.GetName();
            var valueDecreased = Math.Pow(ScalingPerLevel, Level);
            opponent.IncreaseAttackValue(valueDecreased);
            opponent.IncreaseDefenseValue(valueDecreased);
            var toStr = opponentName + "'s attack and defense values were increased back by " +
                        valueDecreased + "!\n";
            return toStr;
        }
    }
}