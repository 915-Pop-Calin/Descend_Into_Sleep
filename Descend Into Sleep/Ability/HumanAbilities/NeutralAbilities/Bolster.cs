using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class Bolster: Ability
    {
        public Bolster(): base("Bolster")
        {
            Description = "Your attack is increased while your opponent's attack is decreased for 3 turns\n";
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var difference = ScalingPerLevel * Level;
            if (opponent.GetAttackValue() <= difference)
                throw new NegativeAttackException(opponent.GetName());
            opponent.DecreaseAttackValue(difference);
            caster.IncreaseAttackValue(difference);
            toStr += $"{caster.GetName()}'s attack was increased by {difference}!\n";
            toStr += $"{opponent.GetName()}'s attack was decreased by {difference}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var difference = ScalingPerLevel * Level;
            opponent.IncreaseAttackValue(difference);
            caster.DecreaseAttackValue(difference);
            var toStr = $"{caster.GetName()}'s attack was decreased back by {difference}!\n";
            toStr += $"{opponent.GetName()}'s attack was increased back by {difference}!\n";
            return toStr;
        }
    }
}