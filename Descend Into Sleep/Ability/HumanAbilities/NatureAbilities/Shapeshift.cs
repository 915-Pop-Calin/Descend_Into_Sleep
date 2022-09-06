using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class Shapeshift : Ability
    {
        private readonly Queue<List<double>> StatsGainedQueue;
        private const double ARMOUR_SCALING_PER_LEVEL = 75;
        private const double HEALTH_SCALING_PER_LEVEL = 75;
        private const double ATTACK_SCALING_PER_LEVEL = 15;
        private const int TURN_COOLDOWN = 7;

        public Shapeshift() : base("Shapeshift")
        {
            StatsGainedQueue = new Queue<List<double>>();
            ManaCost = 50;
            TurnsUntilDecast = 3;
            Description = $"You shapeshift and gain {ARMOUR_SCALING_PER_LEVEL * Level} armour, " +
                          $"{HEALTH_SCALING_PER_LEVEL * Level} health and {ATTACK_SCALING_PER_LEVEL * Level} attack " +
                          $"for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You shapeshift and gain {ARMOUR_SCALING_PER_LEVEL * Level} armour, " +
                          $"{HEALTH_SCALING_PER_LEVEL * Level} health and {ATTACK_SCALING_PER_LEVEL * Level} attack " +
                          $"for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            string toStr = GetCastingString(caster);
            double armourGained = ARMOUR_SCALING_PER_LEVEL * Level;
            double healthGained = HEALTH_SCALING_PER_LEVEL * Level;
            double attackGained = ATTACK_SCALING_PER_LEVEL * Level;
            List<double> statsGained = new List<double>();
            statsGained.Add(armourGained);
            statsGained.Add(healthGained);
            statsGained.Add(attackGained);
            StatsGainedQueue.Enqueue(statsGained);
            caster.IncreaseAttackValue(attackGained);
            caster.GainHealthPoints(healthGained);
            caster.IncreaseDefenseValue(armourGained);
            toStr +=
                $"{caster.GetName()}'s health was increased by {healthGained}, their attack value was increased by ";
            toStr += $"{attackGained} and their defense was increased by {armourGained}!\n";
            toStr +=
                $"{caster.GetName()} now has {Math.Round(caster.GetHealthPoints(), 2)} health, {Math.Round(caster.GetAttackValue(), 2)} " +
                $" attack and {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            Available = false;
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            listOfTurns.Add(turnCounter + TURN_COOLDOWN, (c1, c2) => SecondDecast(c1, c2));
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (StatsGainedQueue.Count == 0)
                throw new EmptyQueueException("Stats Gained");
            string casterName = caster.GetName();
            List<double> statsGained = StatsGainedQueue.Dequeue();
            double armourGained = statsGained[0];
            double healthGained = statsGained[1];
            double attackGained = statsGained[2];
            caster.LoseHealthPoints(healthGained);
            caster.IncreaseAttackValue(-attackGained);
            caster.IncreaseDefenseValue(-armourGained);
            string toStr = $"{casterName}'s shapeshift has ended!\n Their stats were brought back to normal!\n";
            toStr +=
                $"{caster.GetName()} now has {Math.Round(caster.GetHealthPoints(), 2)} health, {Math.Round(caster.GetAttackValue(), 2)} " +
                $" attack and {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }

        private string SecondDecast(Character character, Character opponent)
        {
            Available = true;
            var toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}