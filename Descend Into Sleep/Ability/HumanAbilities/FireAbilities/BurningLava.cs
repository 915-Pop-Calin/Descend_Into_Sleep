using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class BurningLava : Ability
    {
        private const int NUMBER_OF_TURNS = 5;
        private const double MISSING_HEALTH_PERCENTAGE_TRANSFORMED = 0.5;

        public BurningLava() : base("Burning Lava")
        {
            ManaCost = 20;
            Description = $"Your opponent takes {Level * MISSING_HEALTH_PERCENTAGE_TRANSFORMED} * missingHealth true " +
                          $"damage over {NUMBER_OF_TURNS} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent takes {Level * MISSING_HEALTH_PERCENTAGE_TRANSFORMED} * missingHealth true " +
                          $"damage over {NUMBER_OF_TURNS} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            double totalDamageDealt = Level * missingHealth * MISSING_HEALTH_PERCENTAGE_TRANSFORMED;
            double damagePerTurn = totalDamageDealt / NUMBER_OF_TURNS;
            DotEffect dotEffect = new DotEffect(NUMBER_OF_TURNS, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            toStr += $"Due to {caster.GetName()} missing {Math.Round(missingHealth, 2)} health," +
                     $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage per turn for {NUMBER_OF_TURNS} turns!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}