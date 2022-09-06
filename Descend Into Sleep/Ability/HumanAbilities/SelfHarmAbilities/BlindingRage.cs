using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class BlindingRage : Ability
    {
        private readonly Queue<double> DefenseLostQueue;
        private readonly Queue<double> AttackGainedQueue;
        private const double DEFENSE_PERCENTAGE_LOST = 0.5;
        private const double ATTACK_PERCENTAGE_GAINED = 1;

        public BlindingRage() : base("Blinding Rage")
        {
            ManaCost = 15;
            DefenseLostQueue = new Queue<double>();
            AttackGainedQueue = new Queue<double>();
            TurnsUntilDecast = 3;
            Description = $"You gain {ATTACK_PERCENTAGE_GAINED} * AttackValue while losing " +
                          $"{DEFENSE_PERCENTAGE_LOST} * DefenseValue for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You gain {ATTACK_PERCENTAGE_GAINED} * AttackValue while losing " +
                          $"{DEFENSE_PERCENTAGE_LOST} * DefenseValue for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double decreasedDefense = caster.GetDefenseValue() * DEFENSE_PERCENTAGE_LOST;
            double increasedAttack = caster.GetAttackValue() * ATTACK_PERCENTAGE_GAINED;
            AttackGainedQueue.Enqueue(increasedAttack);
            DefenseLostQueue.Enqueue(decreasedDefense);
            caster.IncreaseAttackValue(increasedAttack);
            caster.IncreaseDefenseValue(-decreasedDefense);
            toStr +=
                $"In a blinding rage, {caster.GetName()}'s attack value was increased by {Math.Round(increasedAttack, 2)}";
            toStr += $" and their defense was decreased by {Math.Round(decreasedDefense, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (AttackGainedQueue.Count == 0 || DefenseLostQueue.Count == 0)
                throw new EmptyQueueException("Stats");
            double increasedAttack = AttackGainedQueue.Dequeue();
            double decreasedDefense = DefenseLostQueue.Dequeue();
            caster.IncreaseAttackValue(-increasedAttack);
            caster.IncreaseDefenseValue(decreasedDefense);
            string toStr = $"{caster.GetName()}'s attack value and defense were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}