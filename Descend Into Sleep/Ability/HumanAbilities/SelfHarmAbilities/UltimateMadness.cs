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
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var totalHealth = caster.GetMaximumHealthPoints();
            var percentageMissing = missingHealth / totalHealth;
            var damageIncrease = Math.Pow(percentageMissing, Level);
            var attackIncrease = caster.GetAttackValue() * damageIncrease;
            IncreasedDamageQueue.Enqueue(attackIncrease);
            caster.IncreaseAttackValue(attackIncrease);
            toStr += $"Due to {caster.GetName()} missing {Math.Round(missingHealth, 2)} of its health, their attack is increased by " +
                     $"{Math.Round(attackIncrease, 2)} for {TurnsUntilDecast} turns!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (IncreasedDamageQueue.Count == 0)
                throw new EmptyQueueException("Increased Damage");
            var attackIncrease = IncreasedDamageQueue.Dequeue();
            caster.IncreaseAttackValue(-attackIncrease);
            var toStr = $"{caster.GetName()}'s attack value was brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}