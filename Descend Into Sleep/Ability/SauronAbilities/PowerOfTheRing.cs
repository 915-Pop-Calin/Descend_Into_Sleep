using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.SauronAbilities
{
    public class PowerOfTheRing: Ability
    {
        private Queue<double> AttackGainedQueue;
        private Queue<double> DefenseGainedQueue;
        private readonly double StatsMultiplier;
        
        public PowerOfTheRing() : base("Power Of The Ring")
        {
            AttackGainedQueue = new Queue<double>();
            DefenseGainedQueue = new Queue<double>();
            StatsMultiplier = 4;
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var attackValue = caster.GetAttackValue();
            var increasedAttackValue = attackValue * StatsMultiplier;
            var defenseValue = caster.GetDefenseValue();
            var increasedDefenseValue = defenseValue * StatsMultiplier;
            AttackGainedQueue.Enqueue(increasedAttackValue);
            DefenseGainedQueue.Enqueue(increasedDefenseValue);
            caster.IncreaseAttackValue(increasedAttackValue);
            caster.IncreaseDefenseValue(increasedDefenseValue);

            var toStr = $"{caster.GetName()}'s ring empowers him!\n";
            toStr += $"{caster.GetName()}'s defense and attack were multiplied by 5!\n";
            toStr += $"{caster.GetName()} gained {Math.Round(increasedAttackValue, 2)} attack and " +
                     $"{Math.Round(increasedDefenseValue,2)} defense!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var increasedAttackValue = AttackGainedQueue.Dequeue();
            var increasedDefenseValue = DefenseGainedQueue.Dequeue();
            caster.IncreaseAttackValue(-increasedAttackValue);
            caster.IncreaseDefenseValue(-increasedDefenseValue);
            var toStr = $"{caster.GetName()}'s defense and attack were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}