using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class TickingBomb: Ability
    {
        
        public TickingBomb() : base("Ticking Bomb")
        {
            ManaCost = 25;
            ScalingPerLevel = 0.75;
            TurnsUntilDecast = 5;
            Description = $"You place a bomb upon your enemy which will deal {ScalingPerLevel * Level} * AttackValue " +
                          $"true damage in {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You place a bomb upon your enemy which will deal {ScalingPerLevel * Level} * AttackValue " +
                          $"true damage in {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            toStr += $"A bomb placed upon {opponent.GetName()} will explode in {TurnsUntilDecast} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            double attackValue = caster.GetAttackValue();
            double totalDamageDealt = attackValue * ScalingPerLevel * Level;
            opponent.ReduceHealthPoints(totalDamageDealt);
            string toStr = $"The bomb has exploded!\n{opponent.GetName()} has taken {Math.Round(totalDamageDealt, 2)} damage!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
    }
}