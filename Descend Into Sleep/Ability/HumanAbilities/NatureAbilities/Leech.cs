using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class Leech : Ability
    {
        private Queue<double> ArmourLeechedQueue;
        
        public Leech() : base("Leech")
        {
            ManaCost = 30;
            ArmourLeechedQueue = new Queue<double>();
            TurnsUntilDecast = 3;
            ScalingPerLevel = 0.04; 
            Description = $"You leech {ScalingPerLevel * Level} * OpponentDefenseValue armour off your opponent\n";
        }

        public override void ResetDescription()
        {
            Description = $"You leech {ScalingPerLevel * Level} * OpponentDefenseValue armour off your opponent\n";
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var armourLeeched = opponent.GetDefenseValue() * ScalingPerLevel * Level;
            caster.IncreaseDefenseValue(armourLeeched);
            opponent.IncreaseDefenseValue(-armourLeeched);
            ArmourLeechedQueue.Enqueue(armourLeeched);
            toStr += $"{caster.GetName()} has leeched {Math.Round(armourLeeched, 2)} armour from {opponent.GetName()} for {TurnsUntilDecast} turns!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} armour, while " +
                     $"{opponent.GetName()} has {Math.Round(opponent.GetDefenseValue(), 2)} armour.\n"; 
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (ArmourLeechedQueue.Count == 0)
                throw new EmptyQueueException("Armour Leeched");
            var armourLeeched = ArmourLeechedQueue.Dequeue();
            caster.IncreaseDefenseValue(-armourLeeched);
            opponent.IncreaseDefenseValue(armourLeeched);
            var toStr = "Leeched armour is now gone!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} armour, while " +
                     $"{opponent.GetName()} has {Math.Round(opponent.GetDefenseValue(), 2)} armour.\n";
            return toStr;
        }
    }
}