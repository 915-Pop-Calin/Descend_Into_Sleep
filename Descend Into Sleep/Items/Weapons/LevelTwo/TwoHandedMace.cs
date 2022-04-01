using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TwoHandedMace: IWeapon, IPassive, IObtainable
    {
        public double GetAttackValue()
        {
            return 45;
        }
        
        public string GetName()
        {
            return "Two Handed Mace";
        }

        public string GetDescription()
        {
            return "Strong attack weapon, but stuns you for one turn";
        }
        
        public string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            String toStr;
            try
            {

                caster.Stun();

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

                toStr = $"{caster.GetName()} was stunned for a turn by Two Handed Mace!\n";
            }
            catch (StunException stunException)
            {
                toStr = stunException.Message;
            }

            return toStr;
        }

        private string Decast(Character caster, Character opponent)
        {
            caster.Unstun();
            var toStr = $"{caster.GetName()} can attack now!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 750;
        }
    }
}