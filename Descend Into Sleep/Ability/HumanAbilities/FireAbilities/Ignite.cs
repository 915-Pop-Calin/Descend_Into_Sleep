using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class Ignite : Ability
    {
        private const int NUMBER_OF_TURNS = 5;
        private const double DEFENSE_LOST = 20;

        public Ignite() : base("Ignite")
        {
            ManaCost = 25;
            ScalingPerLevel = 5;
            TurnsUntilDecast = 3;
            Description = $"Your opponent takes {ScalingPerLevel * Level} true damage over {NUMBER_OF_TURNS} Turns" +
                          $" and have their defense decreased by {DEFENSE_LOST} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent takes {ScalingPerLevel * Level} true damage over {NUMBER_OF_TURNS} Turns" +
                          $" and have their defense decreased by {DEFENSE_LOST} for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double damagePerTurn = ScalingPerLevel * Level;
            DotEffect damageOverTime = new DotEffect(NUMBER_OF_TURNS, damagePerTurn);
            opponent.AddDotEffect(damageOverTime);
            opponent.IncreaseDefenseValue(-DEFENSE_LOST);
            toStr +=
                $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage over {NUMBER_OF_TURNS} turns" +
                $" and their defense was decreased by {DEFENSE_LOST}!\n";
            toStr += $"{opponent.GetName()} has now {Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            string toStr = $"{opponent.GetName()}'s defense value was brought back to normal!\n";
            opponent.IncreaseDefenseValue(DEFENSE_LOST);
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}