using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Discourage : Ability
    {
        private readonly Queue<double> AttackValues;

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

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double currentAttackValue = opponent.GetAttackValue();
            AttackValues.Enqueue(currentAttackValue);
            opponent.IncreaseAttackValue(-currentAttackValue);
            toStr += $"{opponent.GetName()}'s attack value was decreased to 0!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (AttackValues.Count == 0)
                throw new EmptyQueueException("Attack Values");
            double attackValue = AttackValues.Dequeue();
            opponent.IncreaseAttackValue(attackValue);
            string toStr = $"{opponent.GetName()}'s attack was brought back to normal!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}