﻿using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class UndyingWill: Ability
    {
        private Queue<double> AttackValuesQueue;
        private readonly double PercentageIncreased;
        
        public UndyingWill() : base("Undying Will")
        {
            Description = "your attack damage is greatly increased, but you die after 3 turns\n";
            ManaCost = 75;
            TurnsUntilDecast = 3;
            AttackValuesQueue = new Queue<double>();
            PercentageIncreased = 2;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var casterName = caster.GetName();
            var attackValue = caster.GetAttackValue();
            var increasedAttackValue = PercentageIncreased * attackValue;
            AttackValuesQueue.Enqueue(increasedAttackValue);
            caster.IncreaseAttackValue(increasedAttackValue);
            toStr += casterName + "'s attack value was increased with " + increasedAttackValue +
                     ". You have 3 turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var opponentHealth = opponent.GetHealthPoints();
            var casterName = caster.GetName();
            var toStr = "";
            if (opponentHealth > 0)
            {
                caster.ReduceHealthPoints(10000);
                toStr += casterName + "'s health was reduced by 10000!\n";
            }
            else
            {
                if (AttackValuesQueue.Count == 0)
                    throw new EmptyQueueException("Attack Values");
                var increasedAttackValue = AttackValuesQueue.Dequeue();
                caster.DecreaseAttackValue(increasedAttackValue);
                toStr += casterName + "'s attack value was brought back to normal!\n";
            }

            return toStr;
        }
    }
}