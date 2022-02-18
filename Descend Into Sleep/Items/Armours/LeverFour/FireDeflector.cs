using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class FireDeflector:  Armour
    {
        public FireDeflector(): base(0, 75, 0)
        {
            SetActive();
            Name = "Fire Deflector";
            Description = "Has the chance to deflect all DOT effects on his enemies";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            var willDeflect = RandomHelper.IsSuccessfulTry(0.1);
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
    }
}