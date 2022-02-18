using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class DoubleEdgedSword : Weapon
    {

        private readonly double ValueIncreased;
        private readonly int TurnsUntilDecast;
        
        public DoubleEdgedSword() : base(20, 0, 0)
        {
            Name = "Double Edged Sword";
            SetPassive();
            ValueIncreased = 10;
            TurnsUntilDecast = 2;
            Description = $"Huge attack weapon, but your opponent gains {ValueIncreased} attack for {TurnsUntilDecast} Turns";
        }

        public override string Passive(Character caster, Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            opponent.IncreaseAttackValue(ValueIncreased);
            Func<Character, Character, string> decastFunction = delegate(Character caster, Character opponent)
            {
                return Decast(caster, opponent);
            };

            if (listOfTurns.ContainsKey(turnCounter + TurnsUntilDecast))
                listOfTurns[turnCounter + TurnsUntilDecast].Add(decastFunction);
            else
            {
                listOfTurns[turnCounter + TurnsUntilDecast] = new List<Func<Character, Character, string>>();
                listOfTurns[turnCounter + TurnsUntilDecast].Add(decastFunction);
            }

            var toStr = $"{opponent.GetName()}'s attack was increased by {ValueIncreased} for {TurnsUntilDecast} turns by Double Edged Sword!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue())} attack!\n";
            return toStr;
        }

        public string Decast(Character caster, Character opponent) {
        opponent.IncreaseAttackValue(-ValueIncreased);
        var toStr = $"{opponent.GetName()}'s attack was decreased back by {ValueIncreased} by Double Edged Sword!\n";
        toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue())} attack!\n";
        return toStr;
        }
        
    }
        
        
    }
