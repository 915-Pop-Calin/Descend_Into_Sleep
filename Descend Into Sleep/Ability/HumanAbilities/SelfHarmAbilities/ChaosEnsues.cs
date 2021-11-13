using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleApp12.Ability.HumanAbilities.NeutralAbilities;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class ChaosEnsues: Ability
    {
        private Queue<Weapon> WeaponQueue;
        private Queue<double> LifeStealQueue;
        private readonly double SetHealthPoints;
        private readonly int TurnCooldown;

        public ChaosEnsues() : base("Chaos Ensues")
        {
            Description = "You lose all your lifesteal and health, but your opponent is stunned.\n";
            ManaCost = 50;
            LifeStealQueue = new Queue<double>();
            WeaponQueue = new Queue<Weapon>();
            TurnsUntilDecast = 3;
            SetHealthPoints = 0.5;
            TurnCooldown = 7;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            var opponentName = opponent.GetName();
            var toStr = GetCastingString(caster);
            var currentWeapon = caster.GetWeapon();
            var removedLifesteal = currentWeapon.GetLifeSteal();
            LifeStealQueue.Enqueue(removedLifesteal);
            WeaponQueue.Enqueue(currentWeapon);
            if (caster is HumanPlayer humanPlayer)
                humanPlayer.RemoveLifeStealFromWeapon(removedLifesteal);
            else
                throw new SchoolException("SelfHarm");
            caster.SetHealthPoints(SetHealthPoints);
            opponent.Stun();
            Available = false;
            toStr += opponentName + " is now stunned!\nYou can no longer life steal!\nYour health is set to 0.5!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            Func<Character, Character, string> secondDecastFunction = delegate(Character caster, Character opponent){
                return SecondDecast(caster, opponent);
            };
            if (listOfTurns.ContainsKey(turnCounter + TurnCooldown))
                listOfTurns[turnCounter + TurnCooldown].Add(secondDecastFunction);
            else {
                listOfTurns[turnCounter + TurnCooldown] = new List<Func<Character, Character, string>>();
                listOfTurns[turnCounter + TurnCooldown].Add(secondDecastFunction);
            }

            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            if (WeaponQueue.Count == 0 || LifeStealQueue.Count == 0)
                throw new EmptyQueueException("Life Steal");
            var casterName = caster.GetName();
            var opponentName = opponent.GetName();
            var debuffedWeapon = WeaponQueue.Dequeue();
            var debuffValue = LifeStealQueue.Dequeue();
            debuffedWeapon.SetLifeSteal(debuffValue);
            var toStr = casterName + " can now life steal!\n" + opponentName + " is no longer stunned!\n";
            return toStr;
        }

        public string SecondDecast(Character caster, Character opponent)
        {
            Available = true;
            var toStr = Name + " is now available!\n";
            return toStr;
        }
    }
}