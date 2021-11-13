using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class NatureCleansing: Ability
    {
        public NatureCleansing() : base("Nature Cleansing")
        {
            Description = "You heal based on your missing health.\n";
            ManaCost = 25;
            ScalingPerLevel = 0.1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var toStr = GetCastingString(caster);
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var amountHealed = missingHealth * ScalingPerLevel * Level;
            toStr += casterName + " has healed for " + amountHealed + "!\n";
            caster.Heal(amountHealed);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}