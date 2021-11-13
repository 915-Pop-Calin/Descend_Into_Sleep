using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public abstract class Weapon: Item
    {
        protected double LifeSteal;
        protected DotEffect DotEffect;
        protected double CriticalChance;
        protected double ArmorPenetration;

        protected Weapon(double attack, double defense)
        {
            Attack = attack;
            Defense = defense;
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
        
        
        public void IncrementAttackValue()
        {
            Attack++;
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

        public void SetDotEffect(DotEffect dotEffect)
        {
            DotEffect = dotEffect;
        }

        public DotEffect GetDotEffect()
        {
            return DotEffect;
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
            var toStr = "";
            toStr += Name + " WEAPON: " + Attack + " ATTACK, ";
            toStr += Defense + " DEFENSE";
            if (Description != null)
                toStr += ", " + Description;
            toStr += "\n";
            return toStr;
        }
        
    }
}