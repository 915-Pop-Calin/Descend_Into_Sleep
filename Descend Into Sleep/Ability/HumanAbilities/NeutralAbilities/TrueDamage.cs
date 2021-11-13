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
            Description = "Your armour penetration is increased for 3 turns\n";
            ManaCost = 35;
            ArmourPenetrationQueue = new Queue<double>();
            SetArmourPenetration = 0.75;
            TurnsUntilDecast = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            var toStr = GetCastingString(caster);
            var gainedArmourPenetration = SetArmourPenetration - caster.GetArmourPenetration();
            ArmourPenetrationQueue.Enqueue(gainedArmourPenetration);
            caster.IncreaseArmourPenetration(gainedArmourPenetration);
            toStr += casterName + "'s armour penetration was set to " + SetArmourPenetration + "!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var casterName = caster.GetName();
            if (ArmourPenetrationQueue.Count == 0)
                throw new EmptyQueueException("Armour Penetration");
            var armourPenetrationGained = ArmourPenetrationQueue.Dequeue();
            caster.DecreaseArmourPenetration(armourPenetrationGained);
            var toStr = casterName + "'s armour penetration was brought back to normal!\n";
            return toStr;
        }
    }
}