using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class TrueDamage : Ability
    {
        private readonly Queue<double> ArmourPenetrationQueue;
        private const double SET_ARMOUR_PENETRATION = 0.75;

        public TrueDamage() : base("True Damage")
        {
            ManaCost = 35;
            ArmourPenetrationQueue = new Queue<double>();
            TurnsUntilDecast = 3;
            Description =
                $"Your armour penetration is increased to {SET_ARMOUR_PENETRATION} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description =
                $"Your armour penetration is increased to {SET_ARMOUR_PENETRATION} for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            double gainedArmourPenetration = SET_ARMOUR_PENETRATION - caster.GetArmourPenetration();
            ArmourPenetrationQueue.Enqueue(gainedArmourPenetration);
            caster.IncreaseArmourPenetration(gainedArmourPenetration);
            toStr += $"{caster.GetName()}'s armour penetration was set to {SET_ARMOUR_PENETRATION}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            if (ArmourPenetrationQueue.Count == 0)
                throw new EmptyQueueException("Armour Penetration");
            double armourPenetrationGained = ArmourPenetrationQueue.Dequeue();
            caster.IncreaseArmourPenetration(-armourPenetrationGained);
            string toStr = $"{caster.GetName()}'s armour penetration was brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetArmourPenetration(), 2)} armour penetration!\n";
            return toStr;
        }
    }
}