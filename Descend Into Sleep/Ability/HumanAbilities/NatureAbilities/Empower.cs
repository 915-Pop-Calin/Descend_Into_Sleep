using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class Empower : Ability
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

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            double amountHealed = missingHealth * ScalingPerLevel * Level;
            toStr += $"{caster.GetName()} has healed for {Math.Round(amountHealed, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetHealthPoints())} health!\n";
            caster.Heal(amountHealed);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}