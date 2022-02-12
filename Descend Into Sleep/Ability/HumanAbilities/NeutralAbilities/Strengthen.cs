using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Strengthen: Ability
    {
        public Strengthen() : base("Strengthen")
        {
            Description = "Your attack and defense are increased for 3 turns\n";
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var valueIncreased = Math.Pow(ScalingPerLevel, Level);
            caster.IncreaseAttackValue(valueIncreased);
            caster.IncreaseDefenseValue(valueIncreased);
            toStr += $"{caster.GetName()}'s attack and defense values were increased by {valueIncreased}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var valueIncreased = Math.Pow(ScalingPerLevel, Level);
            caster.DecreaseAttackValue(valueIncreased);
            caster.DecreaseDefenseValue(valueIncreased);
            var toStr = $"{caster.GetName()}'s attack and defense values were decreased back by {valueIncreased}!\n";
            return toStr;
        }
    }
}