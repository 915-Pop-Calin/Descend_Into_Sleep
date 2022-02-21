using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class FireDeflector:  Armour
    {
        private readonly double DeflectionChance;
        
        public FireDeflector(): base(0, 75, 0)
        {
            SetActive();
            DeflectionChance = 0.1;
            Name = "Fire Deflector";
            Description = $"Has {DeflectionChance * 100}% chance to deflect all DOT effects on the enemy";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            var willDeflect = RandomHelper.IsSuccessfulTry(DeflectionChance);
            var toStr = "";
            if (willDeflect)
            {
                var dotEffects = caster.GetDotEffects();
                foreach (var dotEffect in dotEffects)
                    opponent.AddDotEffect(dotEffect);
                caster.ClearDotEffects();
                toStr = $"{caster.GetName()} has deflected his DOT effects onto {opponent.GetName()}!\n";
            }
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 3200;
        }
        
    }
}