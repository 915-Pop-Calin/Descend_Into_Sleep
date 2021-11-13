using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class LastStand: Armour
    {
        public LastStand() : base(0, 400)
        {
            Name = "Last Stand";
            Description = "Great armour which gets its defense decreased if under 30% HP";
            SetPassive();
        }

        public override string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = "";
            if (caster.GetHealthPoints() / caster.GetMaximumHealthPoints() < 0.3)
            {
                caster.DecreaseDefenseValue(100);
                toStr = "Due to " + caster.GetName() + " being under 30% HP, his defense was reduced by 100 for a turn!\n";
                
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
            caster.IncreaseDefenseValue(100);
            var toStr = caster.GetName() + "'s defenses were brought back to normal!\n";
            return toStr;
        }
    }
}