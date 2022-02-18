using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public abstract class Item
    {
        protected double Attack;
        protected double Defense;
        protected double Health;
        protected bool _Active;
        protected string Description;
        protected bool _Passive;
        protected bool Reflector;
        protected bool Broken;
        protected string Name;
        protected double HealthPoints;
        
        public double GetAttackValue()
        {
            return Attack;
        }

        public double GetDefenseValue()
        {
            return Defense;
        }

        public void SetDefenseValue(double newDefenseValue)
        {
            Defense = newDefenseValue;
        }

        public double GetHealth()
        {
            return Health;
        }

        public void SetHealth(double newHealth)
        {
            Health = newHealth;
        }
        
        public bool HasActive()
        {
            return _Active;
        }

        public void SetActive()
        {
            _Active = true;
        }

        public bool HasPassive()
        {
            return _Passive;
        }

        public void SetPassive()
        {
            _Passive = true;
        }

        public void SetReflector()
        {
            Reflector = true;
        }
        
        public bool IsReflector()
        {
            return Reflector;
        }

        public string GetName()
        {
            return Name;
        }

        public virtual string TakeHit(double attackValue)
        {
            return "";
        }

        public virtual string Active(double damageDealt, Character caster, Character opponent)
        {
            return "";
        }
        
        public virtual string Passive(Character caster, Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>>
                listOfTurns, int turnCounter)
        {
            return "";
        }
        
        public bool IsBroken()
        {
            return Broken;
        }
    }
    
    
}