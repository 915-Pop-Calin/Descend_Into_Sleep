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
            ManaCost = 15;
            TurnsUntilDecast = 3;
            ScalingPerLevel = 2.5;
            Description = $"Your attack is increased by {Math.Pow(ScalingPerLevel, Level)} while your opponent's attack " +
                          $"is decreased by {Math.Pow(ScalingPerLevel, Level)} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your attack is increased by {Math.Pow(ScalingPerLevel, Level)} while your opponent's attack " +
                          $"is decreased by {Math.Pow(ScalingPerLevel, Level)} for {TurnsUntilDecast} Turns\n";    
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var difference = Math.Pow(ScalingPerLevel, Level);
            if (opponent.GetAttackValue() <= difference)
                throw new NegativeAttackException(opponent.GetName());
            opponent.IncreaseAttackValue(-difference);
            caster.IncreaseAttackValue(difference);
            toStr += $"{caster.GetName()}'s attack was increased by {Math.Round(difference, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            toStr += $"{opponent.GetName()}'s attack was decreased by {Math.Round(difference, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var difference = ScalingPerLevel * Level;
            opponent.IncreaseAttackValue(difference);
            caster.IncreaseAttackValue(-difference);
            var toStr = $"{caster.GetName()}'s attack was decreased back by {Math.Round(difference, 2)}!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack!\n";
            toStr += $"{opponent.GetName()}'s attack was increased back by {Math.Round(difference, 2)}!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue(), 2)} attack!\n";
            return toStr;
        }
    }
}