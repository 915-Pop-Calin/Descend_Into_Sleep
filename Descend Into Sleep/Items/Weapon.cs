using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public abstract class Weapon: Item
    {
        protected double LifeSteal;
        protected DotEffect DotEffect;
        protected double CriticalChance;
        protected double ArmorPenetration;

        protected Weapon(double attack, double defense, double health)
        {
            Attack = attack;
            Defense = defense;
            Health = health;
            LifeSteal = 0;
            _Effect = false;
            DotEffect = null;
            CriticalChance = 0;
            Description = null;
            _Passive = false;
            ArmorPenetration = 0;
            Reflector = false;
            HealthPoints = 0;
        }
        
        
        public void SetAttackValue(double newAttackValue)
        {
            Attack = newAttackValue;
        }
        
        public double GetLifeSteal()
        {
            return LifeSteal;
        }

        public void SetLifeSteal(double newLifeSteal)
        {
            LifeSteal = newLifeSteal;
        }
        
        public double GetCriticalChance()
        {
            return CriticalChance;
        }

        public double GetArmorPenetration()
        {
            return ArmorPenetration;
        }
        
        public override string ToString()
        {
            var defenseString = $", {Defense} DEFENSE";
            var healthString = $", {Health} HEALTH";
            var lifestealString = $", {LifeSteal * 100}% LIFESTEAL";
            var criticalChanceString = $", {CriticalChance * 100}% CRITICAL CHANCE";
            var armourPenetrationString = $", {ArmorPenetration * 100}% ARMOUR PENETRATION";
            var descriptionString = $": {Description}";
            return $"{Name} WEAPON: {Attack} ATTACK" + String.Concat(Enumerable.Repeat(defenseString, Convert.ToInt16(Defense != 0))) +
                     String.Concat(Enumerable.Repeat(healthString, Convert.ToInt16(Health != 0))) + 
                     String.Concat(Enumerable.Repeat(lifestealString, Convert.ToInt16(LifeSteal != 0)))
                     + String.Concat(Enumerable.Repeat(criticalChanceString, Convert.ToInt16(CriticalChance != 0)))
                     + String.Concat(Enumerable.Repeat(armourPenetrationString, Convert.ToInt16(ArmorPenetration != 0)))
                     + String.Concat(Enumerable.Repeat(descriptionString, Convert.ToInt16(Description != null))) + "\n";
        }
        
    }
}