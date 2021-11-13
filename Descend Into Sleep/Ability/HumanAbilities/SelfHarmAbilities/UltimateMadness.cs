using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class UltimateMadness: Ability
    {
        private Queue<double> IncreasedDamageQueue;
        
        public UltimateMadness() : base("Ultimate Madness")
        {
            Description = "You gain attack depending on your missing health.\n";
            ManaCost = 25;
            IncreasedDamageQueue = new Queue<double>();
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var casterName = caster.GetName();
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var totalHealth = caster.GetMaximumHealthPoints();
            var percentageMissing = missingHealth / totalHealth;
            var damageIncrease = Math.Pow(percentageMissing, Level);
            var attackIncrease = caster.GetAttackValue() * damageIncrease;
            IncreasedDamageQueue.Enqueue(attackIncrease);
            caster.IncreaseAttackValue(attackIncrease);
            toStr += "Due to " + casterName + " missing " + missingHealth +
                     " of its health, their attack is increased by ";
            toStr += attackIncrease + " for " + TurnsUntilDecast + " turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (IncreasedDamageQueue.Count == 0)
                throw new EmptyQueueException("Increased Damage");
            var casterName = caster.GetName();
            var attackIncrease = IncreasedDamageQueue.Dequeue();
            caster.DecreaseAttackValue(attackIncrease);
            var toStr = casterName + "'s attack value was brought back to normal!\n";
            return toStr;
        }
    }
}