using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.SauronAbilities
{
    public class PowerOfTheRing: Ability
    {
        private readonly Queue<double> AttackGainedQueue;
        private readonly Queue<double> DefenseGainedQueue;
        private const double STATS_MULTIPLIER = 4;
        
        public PowerOfTheRing() : base("Power Of The Ring")
        {
            AttackGainedQueue = new Queue<double>();
            DefenseGainedQueue = new Queue<double>();
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double attackValue = caster.GetAttackValue();
            double increasedAttackValue = attackValue * STATS_MULTIPLIER;
            double defenseValue = caster.GetDefenseValue();
            double increasedDefenseValue = defenseValue * STATS_MULTIPLIER;
            AttackGainedQueue.Enqueue(increasedAttackValue);
            DefenseGainedQueue.Enqueue(increasedDefenseValue);
            caster.IncreaseAttackValue(increasedAttackValue);
            caster.IncreaseDefenseValue(increasedDefenseValue);

            string toStr = $"{caster.GetName()}'s ring empowers him!\n";
            toStr += $"{caster.GetName()}'s defense and attack were multiplied by 5!\n";
            toStr += $"{caster.GetName()} gained {Math.Round(increasedAttackValue, 2)} attack and " +
                     $"{Math.Round(increasedDefenseValue,2)} defense!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            double increasedAttackValue = AttackGainedQueue.Dequeue();
            double increasedDefenseValue = DefenseGainedQueue.Dequeue();
            caster.IncreaseAttackValue(-increasedAttackValue);
            caster.IncreaseDefenseValue(-increasedDefenseValue);
            string toStr = $"{caster.GetName()}'s defense and attack were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}