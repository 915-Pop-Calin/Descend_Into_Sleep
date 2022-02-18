using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class Ignite: Ability
    {
        private readonly int NumberOfTurns;
        private readonly double DefenseLost;
        
        public Ignite() : base("Ignite")
        {
            ManaCost = 25;
            ScalingPerLevel = 5;
            NumberOfTurns = 5;
            DefenseLost = 20;
            TurnsUntilDecast = 3;
            Description =  $"Your opponent takes {ScalingPerLevel * Level} true damage over {NumberOfTurns} Turns" +
                           $" and have their defense decreased by {DefenseLost} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description =  $"Your opponent takes {ScalingPerLevel * Level} true damage over {NumberOfTurns} Turns" +
                           $" and have their defense decreased by {DefenseLost} for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var damagePerTurn = ScalingPerLevel * Level;
            var damageOverTime = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(damageOverTime);
            opponent.IncreaseDefenseValue(-DefenseLost);
            toStr += $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage over {NumberOfTurns} turns" +
                     $" and their defense was decreased by {DefenseLost}!\n";
            toStr += $"{opponent.GetName()} has now {Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var toStr = $"{opponent.GetName()}'s defense value was brought back to normal!\n";
            opponent.IncreaseDefenseValue(DefenseLost);
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}