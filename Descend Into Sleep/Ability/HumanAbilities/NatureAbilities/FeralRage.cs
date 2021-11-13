using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class FeralRage: Ability
    {
        private Queue<double> GainedArmourQueue;

        public FeralRage() : base("Feral Rage")
        {
            Description = "You gain armour proportional to your attack value and to the level of this ability\n";
            ManaCost = 25;
            GainedArmourQueue = new Queue<double>();
            ScalingPerLevel = 0.2;
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var toStr = GetCastingString(caster);
            var attackDamage = caster.GetAttackValue();
            var gainedArmour = ScalingPerLevel * Level * attackDamage;
            GainedArmourQueue.Enqueue(gainedArmour);
            toStr += casterName + " has gained " + gainedArmour + " armour for " + TurnsUntilDecast
                     + " turns!\n";
            caster.IncreaseDefenseValue(gainedArmour);
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var casterName = caster.GetName();
            if (GainedArmourQueue.Count == 0)
                throw new EmptyQueueException("Gained Armour");
            var gainedArmour = GainedArmourQueue.Dequeue();
            caster.DecreaseDefenseValue(gainedArmour);
            var toStr = casterName + "'s armour was brought back to normal!\n";
            return toStr;
        }
    }
}