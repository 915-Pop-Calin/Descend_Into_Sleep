using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public abstract class Armour: Item
    {
        protected double Dodge;
        protected double Sanity;

        protected Armour(double attack, double defense, double health)
        {
            Attack = attack;
            Defense = defense;
            Health = health;
            _Effect = false;
            Dodge = 0;
            Sanity = 0;
            Description = null;
            _Passive = false;
            Reflector = false;
        }
        
        public double GetDodge()
        {
            return Dodge;
        }

        public double GetSanity()
        {
            return Sanity;
        }
        
        public override string ToString()
        {
            var attackString = $", {Attack} ATTACK";
            var healthString = $", {Health} HEALTH";
            var dodgeString = $", {Dodge * 100}% DODGE";
            var sanityString = $", {Sanity} SANITY";
            var descriptionString = $": {Description}";
            return $"{Name} ARMOUR: {Defense} DEFENSE" + String.Concat(Enumerable.Repeat(attackString, Convert.ToInt16(Defense != 0))) +
                   String.Concat(Enumerable.Repeat(healthString, Convert.ToInt16(Health != 0))) + 
                   String.Concat(Enumerable.Repeat(dodgeString, Convert.ToInt16(Dodge != 0)))
                   + String.Concat(Enumerable.Repeat(sanityString, Convert.ToInt16(Sanity != 0)))
                   + String.Concat(Enumerable.Repeat(descriptionString, Convert.ToInt16(Description != null))) + "\n";
        }
    }
}