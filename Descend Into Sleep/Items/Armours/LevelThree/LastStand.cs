using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class LastStand: Armour
    {
        private readonly double DefenseLost;
        private readonly double Threshhold;
        
        public LastStand() : base(0, 400, 0)
        {
            Name = "Last Stand";
            Description = "Great armour which gets its defense decreased if under 30% HP";
            SetPassive();
            DefenseLost = 100;
            Threshhold = 0.3;
        }

        public override string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = "";
            if (caster.GetHealthPoints() / caster.GetMaximumHealthPoints() < Threshhold)
            {
                caster.IncreaseDefenseValue(-DefenseLost);
                toStr = $"Due to {caster.GetName()} being under {Threshhold * 100}% HP, his defense was reduced by {DefenseLost} " +
                        $"for 3 turns!\n";
                toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
                
                Func<Character, Character, string> decastFunction = delegate(Character caster, Character opponent)
                {
                    return Decast(caster, opponent);
                };

                if (listOfTurns.ContainsKey(turnCounter + 3))
                    listOfTurns[turnCounter + 3].Add(decastFunction);
                else
                {
                    listOfTurns[turnCounter + 3] = new List<Func<Character, Character, string>>();
                    listOfTurns[turnCounter + 3].Add(decastFunction);
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
    }
}