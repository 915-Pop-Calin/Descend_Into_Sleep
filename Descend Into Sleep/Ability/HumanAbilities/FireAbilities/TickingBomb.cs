using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class TickingBomb: Ability
    {
        
        public TickingBomb() : base("Ticking Bomb")
        {
            ManaCost = 25;
            Description = "You place a bomb upon your enemy which will deal damage to him later\n";
            ScalingPerLevel = 0.75;
            TurnsUntilDecast = 5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var toStr = GetCastingString(caster);
            toStr += "A bomb placed upon " + opponentName + " will explode in " + TurnsUntilDecast + " turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var opponentName = opponent.GetName();
            var attackValue = caster.GetAttackValue();
            var totalDamageDealt = attackValue * ScalingPerLevel * Level;
            opponent.ReduceHealthPoints(totalDamageDealt);
            var toStr = "The bomb has exploded!\n" + opponentName + " has taken " + totalDamageDealt +
                        " damage!\n";
            return toStr;
        }
    }
}