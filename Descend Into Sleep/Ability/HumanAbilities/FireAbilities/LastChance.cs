using System;
using System.Collections.Generic;
using System.Data;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class LastChance: Ability
    {
        private Queue<Weapon> WeaponQueue;
        private readonly double DefenseLost;

        public LastChance() : base("Last Chance")
        {
            ManaCost = 15;
            Description = "Your life steal is greatly increased but your defense is decreased by 200\n";
            WeaponQueue = new Queue<Weapon>();
            DefenseLost = 200;
            ScalingPerLevel = 1.5;
            TurnsUntilDecast = 2;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = GetCastingString(caster);
            caster.DecreaseDefenseValue(DefenseLost);
            var addedLifesteal = ScalingPerLevel * Level;
            if (caster is HumanPlayer humanPlayer)
            {
                humanPlayer.AddLifeStealToWeapon(addedLifesteal);
                var currentWeapon = humanPlayer.GetWeapon();
                WeaponQueue.Enqueue(currentWeapon);
            }
            else
                throw new SchoolException("Mage");
            toStr += $"{caster.GetName()}'s lifesteal was increased by {addedLifesteal} and defense value was reduced by {DefenseLost}!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            var addedLifesteal = ScalingPerLevel * Level;
            if (WeaponQueue.Count == 0)
                throw new EmptyQueueException("Weapon");
            var changedWeapon = WeaponQueue.Dequeue();
            var currentLifesteal = changedWeapon.GetLifeSteal();
            changedWeapon.SetLifeSteal(currentLifesteal - addedLifesteal);
            caster.IncreaseDefenseValue(DefenseLost);
            var toStr = $"{caster.GetName()}'s defense and lifesteal were brought back to normal!\n";
            return toStr;
        }
    }
}