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
            var currentAttack = caster.GetAttackValue();
            var attackDecreased = currentAttack - AttackLeft;
            caster.IncreaseAttackValue(-attackDecreased);
            AttackReducedQueue.Enqueue(attackDecreased);
            caster.IncreaseDefenseValue(IncreasedDefense);
            var toStr = $"{caster.GetName()} enters into a defensive stance!\n";
            toStr += $"{caster.GetName()} gains {IncreasedDefense} but their attack is set to {AttackLeft}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var attackDecreased = AttackReducedQueue.Dequeue();
            caster.IncreaseAttackValue(attackDecreased);
            caster.IncreaseDefenseValue(-IncreasedDefense);
            var toStr = $"{caster.GetName()}'s defense and attack were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}