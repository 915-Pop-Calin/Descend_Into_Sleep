using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class LastChance : Ability
    {
        private readonly Queue<IWeapon> WeaponQueue;
        private const double DEFENSE_LOST = 200;

        public LastChance() : base("Last Chance")
        {
            ManaCost = 15;
            WeaponQueue = new Queue<IWeapon>();
            ScalingPerLevel = 1.5;
            TurnsUntilDecast = 2;
            Description = $"Your Life Steal is increased by {ScalingPerLevel * Level}, but your" +
                          $" defense is decreased by {DEFENSE_LOST} for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"Your Life Steal is increased by {ScalingPerLevel * Level}, but your" +
                          $" defense is decreased by {DEFENSE_LOST} for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            caster.IncreaseDefenseValue(-DEFENSE_LOST);
            double addedLifesteal = ScalingPerLevel * Level;
            if (caster is HumanPlayer humanPlayer)
            {
                // humanPlayer.AddLifeStealToWeapon(addedLifesteal);
                // TODO: change this ability bitte!
                IWeapon currentWeapon = humanPlayer.GetWeapon();
                WeaponQueue.Enqueue(currentWeapon);
            }
            else
                throw new SchoolException("Mage");

            toStr +=
                $"{caster.GetName()}'s life steal was increased by {Math.Round(addedLifesteal, 2)} and defense value " +
                $"was reduced by {DEFENSE_LOST}!\n";
            // toStr += $"{caster.GetName()} now has {Math.Round(caster.GetWeapon().GetLifeSteal(), 2)} life steal and " +
            // $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            var addedLifesteal = ScalingPerLevel * Level;
            if (WeaponQueue.Count == 0)
                throw new EmptyQueueException("Weapon");
            var changedWeapon = WeaponQueue.Dequeue();
            // var currentLifesteal = changedWeapon.GetLifeSteal();
            // changedWeapon.SetLifeSteal(currentLifesteal - addedLifesteal);
            caster.IncreaseDefenseValue(DEFENSE_LOST);

            var toStr = $"{caster.GetName()}'s defense and life steal were brought back to normal!\n";
            // toStr += $"{caster.GetName()} now has {Math.Round(caster.GetWeapon().GetLifeSteal(), 2)} lifesteal and " +
            // $"{Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
    }
}