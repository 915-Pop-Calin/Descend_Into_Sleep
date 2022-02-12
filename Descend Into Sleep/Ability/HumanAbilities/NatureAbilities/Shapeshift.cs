using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Security.Authentication;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class Shapeshift: Ability
    {

        private Queue<List<double>> StatsGainedQueue;
        private readonly double ArmourScalingPerLevel;
        private readonly double HealthScalingPerLevel;
        private readonly double AttackScalingPerLevel;
        private readonly int TurnCooldown;
        
        public Shapeshift() : base("Shapeshift")
        {
            Description = "You shapeshift and increase all your stats.\n";
            StatsGainedQueue = new Queue<List<double>>();
            ManaCost = 50;
            TurnsUntilDecast = 3;
            ArmourScalingPerLevel = 75;
            HealthScalingPerLevel = 30;
            AttackScalingPerLevel = 15;
            TurnCooldown = 7;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            var toStr = GetCastingString(caster);
            var armourGained = ArmourScalingPerLevel * Level;
            var healthGained = HealthScalingPerLevel * Level;
            var attackGained = AttackScalingPerLevel * Level;
            var statsGained = new List<double>();
            statsGained.Add(armourGained);
            statsGained.Add(healthGained);
            statsGained.Add(attackGained);
            StatsGainedQueue.Enqueue(statsGained);
            caster.IncreaseAttackValue(attackGained);
            caster.GainHealthPoints(healthGained);
            caster.IncreaseDefenseValue(armourGained);
            toStr += $"{caster.GetName()}'s health was increased by {healthGained}, their attack value was increased by ";
            toStr += $"{attackGained} and their defense was increased by {armourGained}!\n";
            Available = false;
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);

            Func<Character, Character, string> secondDecastFunction = delegate(Character caster, Character opponent){
                return SecondDecast(caster, opponent);
            };
            if (listOfTurns.ContainsKey(turnCounter + TurnCooldown))
                listOfTurns[turnCounter + TurnCooldown].Add(secondDecastFunction);
            else {
                listOfTurns[turnCounter + TurnCooldown] = new List<Func<Character, Character, string>>();
                listOfTurns[turnCounter + TurnCooldown].Add(secondDecastFunction);
            }
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (StatsGainedQueue.Count == 0)
                throw new EmptyQueueException("Stats Gained");
            var casterName = caster.GetName();
            var statsGained = StatsGainedQueue.Dequeue();
            var armourGained = statsGained[0];
            var healthGained = statsGained[1];
            var attackGained = statsGained[2];
            caster.LoseHealthPoints(healthGained);
            caster.DecreaseAttackValue(attackGained);
            caster.DecreaseDefenseValue(armourGained);
            var toStr = $"{casterName}'s shapeshift has ended!\n Their stats were brought back to normal!\n";
            return toStr;
        }

        public string SecondDecast(Character character, Character opponent)
        {
            Available = true;
            var toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}