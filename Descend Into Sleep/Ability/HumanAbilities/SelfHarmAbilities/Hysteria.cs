using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class Hysteria : Ability
    {
        private readonly Queue<double> IncreasedDamageQueue;

        public Hysteria() : base("Hysteria")
        {
            ManaCost = 25;
            IncreasedDamageQueue = new Queue<double>();
            TurnsUntilDecast = 3;
            Description = $"You gain (0.3 + percentageMissingHealth) ** {Level} attack for" +
                          $" {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You gain (0.3 + percentageMissingHealth) ** {Level} attack for" +
                          $" {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            double percentageMissing = 0.3 + missingHealth /
                caster.GetMaximumHealthPoints();
            double damageIncrease = Math.Pow(percentageMissing, Level);
            double attackIncrease = caster.GetAttackValue() * damageIncrease;
            IncreasedDamageQueue.Enqueue(attackIncrease);
            caster.IncreaseAttackValue(attackIncrease);
            toStr +=
                $"Due to {caster.GetName()} missing {Math.Round(missingHealth, 2)} of its health, their attack is increased by " +
                $"{Math.Round(attackIncrease, 2)} for {TurnsUntilDecast} turns!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (IncreasedDamageQueue.Count == 0)
                throw new EmptyQueueException("Increased Damage");
            double attackIncrease = IncreasedDamageQueue.Dequeue();
            caster.IncreaseAttackValue(-attackIncrease);
            string toStr = $"{caster.GetName()}'s attack value was brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}