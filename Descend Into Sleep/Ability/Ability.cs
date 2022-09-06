using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

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

        public virtual void ResetDescription()
        {
            
        }

        public void LevelUp()
        {
            Level++;
            ResetDescription();
        }

        public int GetLevel()
        {
            return Level;
        }

        public double GetManaCost()
        {
            return ManaCost;
        }

        public string GetName()
        {
            return Name;
        }
        
        public abstract string Cast(Character caster, Character opponent, ListOfTurns listOfTurns,
            int turnCounter);

        protected abstract string Decast(Character caster, Character opponent);

        protected void AddToDecastingQueue(Character caster, Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            listOfTurns.Add(turnCounter + TurnsUntilDecast, (c1, c2) => Decast(c1, c2));
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