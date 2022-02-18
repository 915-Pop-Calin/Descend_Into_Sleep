using System;
using System.Collections.Generic;
using System.Security.Authentication;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class Pyroblast: Ability
    {
        private readonly int NumberOfTurns;

        public Pyroblast() : base("Pyroblast")
        {
            ManaCost = 50; 
            ScalingPerLevel = 1.5;
            NumberOfTurns = 7;
            TurnsUntilDecast = 7;
            Description = $"Your opponent takes {ScalingPerLevel * Level} * AttackValue true damage over " +
                          $"{NumberOfTurns} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent takes {ScalingPerLevel * Level} * AttackValue true damage over " +
                          $"{NumberOfTurns} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            var toStr = GetCastingString(caster);
            var totalDamageDealt = caster.GetAttackValue() * ScalingPerLevel * Level;
            var damagePerTurn = totalDamageDealt / NumberOfTurns;
            var damageOverTime = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(damageOverTime);
            toStr += $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage over {NumberOfTurns} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            Available = false;
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            Available = true;
            var toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}