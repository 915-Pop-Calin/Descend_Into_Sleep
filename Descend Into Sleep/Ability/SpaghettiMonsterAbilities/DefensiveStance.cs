using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.SpaghettiMonsterAbilities
{
    public class DefensiveStance: Ability
    {
        private Queue<double> AttackReducedQueue;
        private readonly double AttackLeft;
        private readonly double IncreasedDefense;

        public DefensiveStance() : base("Defensive Stance")
        {
            AttackReducedQueue = new Queue<double>();
            AttackLeft = 1;
            IncreasedDefense = 100;
            TurnsUntilDecast = 2;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var currentAttack = caster.GetAttackValue();
            var attackDecreased = currentAttack - AttackLeft;
            caster.DecreaseAttackValue(attackDecreased);
            AttackReducedQueue.Enqueue(attackDecreased);
            caster.IncreaseDefenseValue(IncreasedDefense);
            var toStr = casterName + " enters into a defensive stance!\n";
            toStr += casterName + " gains " + IncreasedDefense + " but their attack is set to " + AttackLeft + "!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var casterName = caster.GetName();
            var attackDecreased = AttackReducedQueue.Dequeue();
            caster.IncreaseAttackValue(attackDecreased);
            caster.DecreaseDefenseValue(IncreasedDefense);
            var toStr = casterName + "'s defense and attack were brought back to normal!\n";
            return toStr;
        }
    }
}