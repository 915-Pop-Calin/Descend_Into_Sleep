using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class TrueDamage: Ability
    {
        private Queue<double> ArmourPenetrationQueue;
        private readonly double SetArmourPenetration;
        
        public TrueDamage() : base("True Damage")
        {
            ManaCost = 35;
            ArmourPenetrationQueue = new Queue<double>();
            SetArmourPenetration = 0.75;
            TurnsUntilDecast = 3;
            Description = $"Your armour penetration is increased to {SetArmourPenetration} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your armour penetration is increased to {SetArmourPenetration} for {TurnsUntilDecast} Turns\n";
        }
        
        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            var gainedArmourPenetration = SetArmourPenetration - caster.GetArmourPenetration();
            ArmourPenetrationQueue.Enqueue(gainedArmourPenetration);
            caster.IncreaseArmourPenetration(gainedArmourPenetration);
            toStr += $"{caster.GetName()}'s armour penetration was set to {SetArmourPenetration}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (ArmourPenetrationQueue.Count == 0)
                throw new EmptyQueueException("Armour Penetration");
            var armourPenetrationGained = ArmourPenetrationQueue.Dequeue();
            caster.IncreaseArmourPenetration(-armourPenetrationGained);
            var toStr = $"{caster.GetName()}'s armour penetration was brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetArmourPenetration(), 2)} armour penetration!\n";
            return toStr;
        }
    }
}