using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class FireDeflector:  Armour
    {
        public FireDeflector(): base(0, 75, 0)
        {
            SetEffect();
            Name = "Fire Deflector";
            Description = "Has the chance to deflect all DOT effects on his enemies";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var randomObject = new Random();
            var randomChoice = randomObject.Next(1, 11);
            var toStr = "";
            if (randomChoice == 1)
            {
                var dotEffects = caster.GetDotEffects();
                foreach (var dotEffect in dotEffects)
                    opponent.AddDotEffect(dotEffect);
                caster.ClearDotEffects();
                toStr = caster.GetName() + " has deflected his DOT effects onto " + opponent.GetName() + "!\n";
            }
            return toStr;
        }
    }
}