using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Discourage: Ability
    {
        private Queue<double> AttackValues;
        
        public Discourage() : base("Discourage")
        {
            ManaCost = 10;
            AttackValues = new Queue<double>();
            TurnsUntilDecast = 1;
            Description = "Enemy's attack value is reduced to 0 for a Turn\n";
        }

        public override void ResetDescription()
        {
            Description = "Enemy's attack value is reduced to 0 for a Turn\n";
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var currentAttackValue = opponent.GetAttackValue();
            AttackValues.Enqueue(currentAttackValue);
            opponent.IncreaseAttackValue(-currentAttackValue);
            toStr += $"{opponent.GetName()}'s attack value was decreased to 0!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (AttackValues.Count == 0)
                throw new EmptyQueueException("Attack Values");
            var attackValue = AttackValues.Dequeue();
            opponent.IncreaseAttackValue(attackValue);
            var toStr = $"{opponent.GetName()}'s attack was brought back to normal!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}