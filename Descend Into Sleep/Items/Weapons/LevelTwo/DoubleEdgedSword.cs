using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class DoubleEdgedSword : Weapon
    {
        public DoubleEdgedSword() : base(20, 0, 0)
        {
            Name = "Double Edged Sword";
            Description = "Huge Attack Weapon, but your opponent's attacks are stronger";
            SetPassive();
        }

        public override string Passive(Character caster, Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            opponent.IncreaseAttackValue(10);
            Func<Character, Character, string> decastFunction = delegate(Character caster, Character opponent)
            {
                return Decast(caster, opponent);
            };

            if (listOfTurns.ContainsKey(turnCounter + 2))
                listOfTurns[turnCounter + 2].Add(decastFunction);
            else
            {
                listOfTurns[turnCounter + 2] = new List<Func<Character, Character, string>>();
                listOfTurns[turnCounter + 2].Add(decastFunction);
            }

            var toStr = $"{opponent.GetName()}'s attack was increased by 10 for a turn by Double Edged Sword!\n";
            return toStr;
        }

        public string Decast(Character caster, Character opponent) {
        opponent.DecreaseAttackValue(10);
        var toStr = $"{opponent.GetName()}'s attack was decreased back by 10 by Double Edged Sword!\n";
        return toStr;
        }
        
    }
        
        
    }
