using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class LastStand: IArmour, IPassive, IObtainable
    {
        private readonly double DefenseLost;
        private readonly double Threshhold;
        private readonly int TurnsUntilDecast;
        
        public LastStand()
        {
            DefenseLost = 100;
            Threshhold = 0.3;
            TurnsUntilDecast = 3;
        }

        public string GetName()
        {
            return "Last Stand";
        }

        public string GetDescription()
        {
            return $"Great armour which gets your defense decreased by {DefenseLost} for {TurnsUntilDecast} Turns " +
                   $"if under {Threshhold * 100}% health";
        }
        
        public double GetDefenseValue()
        {
            return 400;
        }
        
        public string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = "";
            if (caster.GetHealthPoints() / caster.GetMaximumHealthPoints() < Threshhold)
            {
                caster.IncreaseDefenseValue(-DefenseLost);
                toStr = $"Due to {caster.GetName()} being under {Threshhold * 100}% health, his defense was reduced by {DefenseLost} " +
                        $"for {TurnsUntilDecast} turns!\n";
                toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
                
                // Func<Character, Character, string> decastFunction = delegate(Character caster, Character opponent)
                // {
                //     return Decast(caster, opponent);
                // };
                

                if (listOfTurns.ContainsKey(turnCounter + TurnsUntilDecast))
                    listOfTurns[turnCounter + TurnsUntilDecast].Add((c1, c2) => Decast(c1, c2));
                else
                {
                    listOfTurns[turnCounter + TurnsUntilDecast] = new List<Func<Character, Character, string>>();
                    listOfTurns[turnCounter + TurnsUntilDecast].Add((c1, c2) => Decast(c1, c2));
                }
            }

            return toStr;
        }

        private string Decast(Character caster, Character opponent)
        {
            caster.IncreaseDefenseValue(DefenseLost);
            var toStr = $"{caster.GetName()}'s defenses were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 1800;
        }
    }
}