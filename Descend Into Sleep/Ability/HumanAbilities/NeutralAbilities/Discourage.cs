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
            Description = "Enemy's attack value is reduced to 0 for a turn\n";
            ManaCost = 10;
            AttackValues = new Queue<double>();
            TurnsUntilDecast = 1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var toStr = GetCastingString(caster);
            var currentAttackValue = opponent.GetAttackValue();
            AttackValues.Enqueue(currentAttackValue);
            opponent.DecreaseAttackValue(currentAttackValue);
            toStr += opponentName + "'s attack value was decreased to 0!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (AttackValues.Count == 0)
                throw new EmptyQueueException("Attack Values");
            var attackValue = AttackValues.Dequeue();
            opponent.IncreaseAttackValue(attackValue);
            var toStr = opponent.GetName() + "'s attack was brought back to normal!\n";
            return toStr;
        }
    }
}