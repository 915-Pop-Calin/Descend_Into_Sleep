using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class DoubleEdgedSword : IWeapon, IPassive, IObtainable
    {

        private readonly double ValueIncreased;
        private readonly int TurnsUntilDecast;
        
        public DoubleEdgedSword()
        {
            ValueIncreased = 10;
            TurnsUntilDecast = 2;
        }
        
        
        public double GetAttackValue()
        {
            return 20;
        }
        
        public string GetName()
        {
            return "Double Edged Sword";
        }

        public string GetDescription()
        {
            return $"Huge attack weapon, but your opponent gains {ValueIncreased} attack for {TurnsUntilDecast} Turns";
        }

        public double GetPrice()
        {
            return 800;
        }
        
        public string Passive(Character caster, Character opponent,
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

        private string Decast(Character caster, Character opponent) {
            opponent.IncreaseAttackValue(-ValueIncreased);
            var toStr = $"{opponent.GetName()}'s attack was decreased back by {ValueIncreased} by Double Edged Sword!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue())} attack!\n";
            return toStr;
        }
    }
        
        
    }
