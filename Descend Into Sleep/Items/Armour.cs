using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public abstract class Armour: Item
    {
        protected double Dodge;

        protected Armour(double attack, double defense)
        {
            Attack = attack;
            Defense = defense;
            _Effect = false;
            Dodge = 0;
            Description = null;
            _Passive = false;
            Reflector = false;
        }
        
        public double GetDodge()
        {
            return Dodge;
        }

        public void SetDodge(double newDodgeValue)
        {
            Dodge = newDodgeValue;
        }
        
        public override string ToString()
        {
            var toStr = "";
            toStr += Name + " ARMOUR: " + Attack + " ATTACK, ";
            toStr += Defense + " DEFENSE";
            if (Description != null)
                toStr += ", " + Description;
            toStr += "\n";
            return toStr;
        }
    }
}