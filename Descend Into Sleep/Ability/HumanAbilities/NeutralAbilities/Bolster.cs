using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Bolster : Ability
    {
        public Bolster() : base("Bolster")
        {
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
            Description =
                $"Your attack is increased by {Math.Pow(ScalingPerLevel, Level)} while your opponent's attack " +
                $"is decreased by {Math.Pow(ScalingPerLevel, Level)} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description =
                $"Your attack is increased by {Math.Pow(ScalingPerLevel, Level)} while your opponent's attack " +
                $"is decreased by {Math.Pow(ScalingPerLevel, Level)} for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double difference = Math.Pow(ScalingPerLevel, Level);
            if (opponent.GetAttackValue() <= difference)
                throw new NegativeAttackException(opponent.GetName());
            opponent.IncreaseAttackValue(-difference);
            caster.IncreaseAttackValue(difference);
            toStr += $"{caster.GetName()}'s attack was increased by {Math.Round(difference, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            toStr += $"{opponent.GetName()}'s attack was decreased by {Math.Round(difference, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            double difference = ScalingPerLevel * Level;
            opponent.IncreaseAttackValue(difference);
            caster.IncreaseAttackValue(-difference);
            string toStr = $"{caster.GetName()}'s attack was decreased back by {Math.Round(difference, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            toStr += $"{opponent.GetName()}'s attack was increased back by {Math.Round(difference, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}