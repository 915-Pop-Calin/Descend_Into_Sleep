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
            Description = "Your opponent takes damage over 7 turns proportional to your attack and this ability's level\n";
            ScalingPerLevel = 1.5;
            NumberOfTurns = 7;
            TurnsUntilDecast = 7;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            var opponentName = opponent.GetName();
            var toStr = GetCastingString(caster);
            var totalDamageDealt = caster.GetAttackValue() * ScalingPerLevel * Level;
            var damagePerTurn = totalDamageDealt / NumberOfTurns;
            var damageOverTime = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(damageOverTime);
            toStr += opponentName + " will take " + damagePerTurn + " damage over " + 
                     NumberOfTurns + " turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            Available = false;
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            Available = true;
            var toStr = "Pyroblast is now available!\n";
            return toStr;
        }
    }
}