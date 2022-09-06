using System;
using System.Collections.Generic;
using System.Security;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Taunt: Ability
    {
        public Taunt() : base("Taunt")
        {
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
            Description = $"Your opponent's attack and defense values are decreased by {Math.Pow(ScalingPerLevel, Level)}" +
                          $" for {TurnsUntilDecast} turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent's attack and defense values are decreased by {Math.Pow(ScalingPerLevel, Level)}" +
                          $" for {TurnsUntilDecast} Turns\n";
        }
        
        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double valueDecreased = Math.Pow(ScalingPerLevel, Level);
            if (opponent.GetAttackValue() < valueDecreased || opponent.GetDefenseValue() < valueDecreased)
                throw new NegativeAttackException(opponent.GetName());
            string toStr = GetCastingString(caster);
            opponent.IncreaseAttackValue(-valueDecreased);
            opponent.IncreaseDefenseValue(-valueDecreased);
            toStr += $"{opponent.GetName()}'s attack and defense values were decreased by {Math.Round(valueDecreased, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            double valueDecreased = Math.Pow(ScalingPerLevel, Level);
            opponent.IncreaseAttackValue(valueDecreased);
            opponent.IncreaseDefenseValue(valueDecreased);
            string toStr = $"{opponent.GetName()}'s attack and defense values were increased back by {Math.Round(valueDecreased, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}