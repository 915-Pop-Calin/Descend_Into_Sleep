using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class ChaoticReflection: Ability
    {
        public ChaoticReflection() : base("Chaotic Reflection")
        {
            Description = "You take some damage and all mitigated damage is reflected on your opponent.\n";
            ManaCost = 30;
            ScalingPerLevel = 70;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var damageDealt = ScalingPerLevel * Level;
            var damageTaken = caster.TakeMitigatedDamage(damageDealt);
            var damageMitigated = damageDealt - damageTaken;
            opponent.ReduceHealthPoints(damageMitigated);
            toStr += $"{caster.GetName()} took {Math.Round(damageDealt, 2)} damage, but mitigated {Math.Round(damageMitigated, 2)} of it!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints())} health!\n";
            toStr += $"{opponent.GetName()} took the mitigated damage!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}