using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Strengthen: Ability
    {
        public Strengthen() : base("Strengthen")
        {
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
            Description = $"Your attack and defense are increased by {Math.Pow(ScalingPerLevel, Level)} " +
                          $"for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your attack and defense are increased by {Math.Pow(ScalingPerLevel, Level)} " +
                          $"for {TurnsUntilDecast} Turns\n";
        }
        
        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var valueIncreased = Math.Pow(ScalingPerLevel, Level);
            caster.IncreaseAttackValue(valueIncreased);
            caster.IncreaseDefenseValue(valueIncreased);
            toStr += $"{caster.GetName()}'s attack and defense values were increased by {Math.Round(valueIncreased, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            var valueIncreased = Math.Pow(ScalingPerLevel, Level);
            caster.IncreaseAttackValue(-valueIncreased);
            caster.IncreaseDefenseValue(-valueIncreased);
            var toStr = $"{caster.GetName()}'s attack and defense values were decreased back by {Math.Round(valueIncreased, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                     $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}