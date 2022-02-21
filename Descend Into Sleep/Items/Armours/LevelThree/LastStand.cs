using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class LastStand: Armour
    {
        private readonly double DefenseLost;
        private readonly double Threshhold;
        private readonly int TurnsUntilDecast;
        
        public LastStand() : base(0, 400, 0)
        {
            Name = "Last Stand";
            SetPassive();
            DefenseLost = 100;
            Threshhold = 0.3;
            TurnsUntilDecast = 3;
            Description = $"Great armour which gets your defense decreased by {DefenseLost} for {TurnsUntilDecast} Turns " +
                          $"if under {Threshhold * 100}% health";
        }

        public override string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = "";
            if (caster.GetHealthPoints() / caster.GetMaximumHealthPoints() < Threshhold)
            {
                caster.IncreaseDefenseValue(-DefenseLost);
                toStr = $"Due to {caster.GetName()} being under {Threshhold * 100}% health, his defense was reduced by {DefenseLost} " +
                        $"for {TurnsUntilDecast} turns!\n";
                toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
                
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
            }

            return toStr;
        }

        public string Decast(Character caster, Character opponent)
        {
            caster.IncreaseDefenseValue(DefenseLost);
            var toStr = $"{caster.GetName()}'s defenses were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 1800;
        }
    }
}