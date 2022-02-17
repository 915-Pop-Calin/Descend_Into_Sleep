using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class BlindingRage: Ability
    {
        private Queue<double> DefenseLostQueue;
        private Queue<double> AttackGainedQueue;
        private readonly double DefensePercentageLost;
        private readonly double AttackPercentageGained;
        
        public BlindingRage() : base("Blinding Rage")
        {
            Description = "Your Attack doubles while your defense is halved for 3 turns.\n";
            ManaCost = 15;
            DefenseLostQueue = new Queue<double>();
            AttackGainedQueue = new Queue<double>();
            DefensePercentageLost = 0.5;
            AttackPercentageGained = 1;
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var decreasedDefense = caster.GetDefenseValue() * DefensePercentageLost;
            var increasedAttack = caster.GetAttackValue() * AttackPercentageGained;
            AttackGainedQueue.Enqueue(increasedAttack);
            DefenseLostQueue.Enqueue(decreasedDefense);
            caster.IncreaseAttackValue(increasedAttack);
            caster.IncreaseDefenseValue(-decreasedDefense);
            toStr += $"In a blinding rage, {caster.GetName()}'s attack value was increased by {Math.Round(increasedAttack, 2)}";
            toStr += $" and their defense was decreased by {Math.Round(decreasedDefense, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (AttackGainedQueue.Count == 0 || DefenseLostQueue.Count == 0)
                throw new EmptyQueueException("Stats");
            var increasedAttack = AttackGainedQueue.Dequeue();
            var decreasedDefense = DefenseLostQueue.Dequeue();
            caster.IncreaseAttackValue(-increasedAttack);
            caster.IncreaseDefenseValue(decreasedDefense);
            var toStr = $"{caster.GetName()}'s attack value and defense were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}