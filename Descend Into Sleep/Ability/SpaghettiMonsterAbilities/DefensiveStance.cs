using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.SpaghettiMonsterAbilities
{
    public class DefensiveStance : Ability
    {
        private readonly Queue<double> AttackReducedQueue;
        private const double ATTACK_LEFT = 1;
        private const double INCREASED_DEFENSE = 100;

        public DefensiveStance() : base("Defensive Stance")
        {
            AttackReducedQueue = new Queue<double>();
            TurnsUntilDecast = 2;
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double currentAttack = caster.GetAttackValue();
            double attackDecreased = currentAttack - ATTACK_LEFT;
            caster.IncreaseAttackValue(-attackDecreased);
            AttackReducedQueue.Enqueue(attackDecreased);
            caster.IncreaseDefenseValue(INCREASED_DEFENSE);
            string toStr = $"{caster.GetName()} enters into a defensive stance!\n";
            toStr += $"{caster.GetName()} gains {INCREASED_DEFENSE} but their attack is set to {ATTACK_LEFT}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            double attackDecreased = AttackReducedQueue.Dequeue();
            caster.IncreaseAttackValue(attackDecreased);
            caster.IncreaseDefenseValue(-INCREASED_DEFENSE);
            string toStr = $"{caster.GetName()}'s defense and attack were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}