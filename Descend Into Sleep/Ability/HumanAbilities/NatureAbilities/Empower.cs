using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class Empower: Ability
    {
        public Empower() : base("Empower")
        {
            ManaCost = 25;
            ScalingPerLevel = 0.1;
            Description = $"You heal {ScalingPerLevel * Level} * missingHealth health\n";
        }

        public override void ResetDescription()
        {
            Description = $"You heal {ScalingPerLevel * Level} * missingHealth health\n";
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var amountHealed = missingHealth * ScalingPerLevel * Level;
            toStr += $"{caster.GetName()} has healed for {Math.Round(amountHealed, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetHealthPoints())} health!\n";
            caster.Heal(amountHealed);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}