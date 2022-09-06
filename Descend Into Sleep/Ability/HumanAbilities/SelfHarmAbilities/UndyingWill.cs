using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class UndyingWill : Ability
    {
        private readonly Queue<double> AttackValuesQueue;
        private const double PERCENTAGE_INCREASED = 2;

        public UndyingWill() : base("Undying Will")
        {
            ManaCost = 75;
            TurnsUntilDecast = 3;
            AttackValuesQueue = new Queue<double>();
            Description = $"Your attack damage is increased by {PERCENTAGE_INCREASED} * AttackValue, " +
                          $"but you die after {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your attack damage is increased by {PERCENTAGE_INCREASED} * AttackValue, " +
                          $"but you die after {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double increasedAttackValue = PERCENTAGE_INCREASED * caster.GetAttackValue();;
            AttackValuesQueue.Enqueue(increasedAttackValue);
            caster.IncreaseAttackValue(increasedAttackValue);
            toStr += $"{caster.GetName()}'s attack value was increased with {Math.Round(increasedAttackValue, 2)}. " +
                     $"You have {TurnsUntilDecast} turns until you die!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (opponent.GetHealthPoints() > 0)
            {
                caster.ReduceHealthPoints(10000);
                return $"{caster.GetName()}'s health was reduced by 10000!\n";
            }

            if (AttackValuesQueue.Count == 0)
                throw new EmptyQueueException("Attack Values");
            double increasedAttackValue = AttackValuesQueue.Dequeue();
            caster.IncreaseAttackValue(-increasedAttackValue);
            return $"{caster.GetName()}'s attack value was brought back to normal!\n";
        }
    }
}