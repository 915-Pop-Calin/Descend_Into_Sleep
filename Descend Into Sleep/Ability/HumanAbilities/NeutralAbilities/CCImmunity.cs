using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class CCImmunity: Ability
    {
        public CCImmunity(): base("CC Immunity")
        {
            ManaCost = 20;
            TurnsUntilDecast = 5;
            Description = $"You become immune to CC for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You become immune to CC for {TurnsUntilDecast} Turns\n";
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            caster.SetStunResistant(true);
            toStr += $"{caster.GetName()} is immune to CC for {TurnsUntilDecast} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            caster.SetStunResistant(false);
            var toStr = $"{caster.GetName()} is no longer immune to CC!\n";
            return toStr;
        }
    }
}