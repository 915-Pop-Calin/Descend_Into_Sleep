using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class BurningLava: Ability
    {

        private readonly int NumberOfTurns;
        private readonly double MissingHealthPercentageTransformed;
        
        public BurningLava() : base("Burning Lava")
        {
            ManaCost = 20;
            NumberOfTurns = 5;
            MissingHealthPercentageTransformed = 0.5;
            Description = $"Your opponent takes {Level * MissingHealthPercentageTransformed} * missingHealth true " +
                          $"damage over {NumberOfTurns} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent takes {Level * MissingHealthPercentageTransformed} * missingHealth true " +
                          $"damage over {NumberOfTurns} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var totalDamageDealt = Level * missingHealth * MissingHealthPercentageTransformed;
            var damagePerTurn = totalDamageDealt / NumberOfTurns;
            var dotEffect = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            toStr += $"Due to {caster.GetName()} missing {Math.Round(missingHealth, 2)} health," +
                     $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage per turn for {NumberOfTurns} turns!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}