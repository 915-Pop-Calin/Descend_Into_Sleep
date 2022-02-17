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
            var valueDecreased = Math.Pow(ScalingPerLevel, Level);
            if (opponent.GetAttackValue() < valueDecreased || opponent.GetDefenseValue() < valueDecreased)
                throw new NegativeAttackException(opponent.GetName());
            var toStr = GetCastingString(caster);
            opponent.IncreaseAttackValue(-valueDecreased);
            opponent.IncreaseDefenseValue(-valueDecreased);
            toStr += $"{opponent.GetName()}'s attack and defense values were decreased by {Math.Round(valueDecreased, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var valueDecreased = Math.Pow(ScalingPerLevel, Level);
            opponent.IncreaseAttackValue(valueDecreased);
            opponent.IncreaseDefenseValue(valueDecreased);
            var toStr = $"{opponent.GetName()}'s attack and defense values were increased back by {Math.Round(valueDecreased, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}