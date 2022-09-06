using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class Reflection : Ability
    {
        public Reflection() : base("Reflection")
        {
            ManaCost = 30;
            ScalingPerLevel = 70;
            Description = $"You take {ScalingPerLevel * Level} damage and all the mitigated damage is " +
                          $"reflected upon your opponent\n";
        }

        public override void ResetDescription()
        {
            Description = $"You take {ScalingPerLevel * Level} damage and all the mitigated damage is " +
                          $"reflected upon your opponent\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double damageDealt = ScalingPerLevel * Level;
            double damageTaken = caster.TakeMitigatedDamage(damageDealt);
            double damageMitigated = damageDealt - damageTaken;
            opponent.ReduceHealthPoints(damageMitigated);
            toStr +=
                $"{caster.GetName()} took {Math.Round(damageDealt, 2)} damage, but mitigated {Math.Round(damageMitigated, 2)} of it!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints())} health!\n";
            toStr += $"{opponent.GetName()} took the mitigated damage!\n";
            toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}