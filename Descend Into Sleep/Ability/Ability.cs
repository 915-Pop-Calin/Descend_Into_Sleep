using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability
{
    public abstract class Ability
    {
        protected string Description;
        protected int Level;
        protected bool Available;
        protected double ManaCost;
        protected readonly string Name;
        protected int TurnsUntilDecast;
        protected double ScalingPerLevel;

        protected Ability(string name)
        {
            Description = null;
            Level = 1;
            Available = true;
            ManaCost = 0;
            Name = name;
        }
        
        public string GetDescription()
        {
            return $"{Description}level:{Level}\nmana cost:{ManaCost}\n";
        }

        public void LevelUp()
        {
            Level++;
        }

        public int GetLevel()
        {
            return Level;
        }

        public void SetAvailability(bool availability)
        {
            Available = availability;
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public double GetManaCost()
        {
            return ManaCost;
        }

        public string GetName()
        {
            return Name;
        }
        
        public abstract string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns,
            int turnCounter);

        public abstract string Decast(Character caster, Character opponent);

        protected void AddToDecastingQueue(Character caster, Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns,
            int turnCounter)
        {
            Func<Character, Character, string> decastFunction = delegate(Character caster, Character opponent){ 
                return Decast(caster, opponent); 
            };
        
            if (listOfTurns.ContainsKey(turnCounter + TurnsUntilDecast))
                listOfTurns[turnCounter + TurnsUntilDecast].Add(decastFunction);
            else {
                listOfTurns[turnCounter + TurnsUntilDecast] = new List<Func<Character, Character, string>>();
                listOfTurns[turnCounter + TurnsUntilDecast].Add(decastFunction);
            }
        }

        protected string GetCastingString(Character caster)
        {
            var casterManaLeft = caster.GetMana();
            var toStr = $"{caster.GetName()} has used {ManaCost} mana to cast {Name}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(casterManaLeft, 2)} mana!\n";
            return toStr;
        }
        
    }
}