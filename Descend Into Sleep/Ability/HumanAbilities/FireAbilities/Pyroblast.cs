using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class Pyroblast : Ability
    {
        private const int NUMBER_OF_TURNS = 7;

        public Pyroblast() : base("Pyroblast")
        {
            ManaCost = 50;
            ScalingPerLevel = 1.5;
            TurnsUntilDecast = 7;
            Description = $"Your opponent takes {ScalingPerLevel * Level} * AttackValue true damage over " +
                          $"{NUMBER_OF_TURNS} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your opponent takes {ScalingPerLevel * Level} * AttackValue true damage over " +
                          $"{NUMBER_OF_TURNS} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            string toStr = GetCastingString(caster);
            double totalDamageDealt = caster.GetAttackValue() * ScalingPerLevel * Level;
            double damagePerTurn = totalDamageDealt / NUMBER_OF_TURNS;
            DotEffect damageOverTime = new DotEffect(NUMBER_OF_TURNS, damagePerTurn);
            opponent.AddDotEffect(damageOverTime);
            toStr +=
                $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage over {NUMBER_OF_TURNS} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            Available = false;
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            Available = true;
            var toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}