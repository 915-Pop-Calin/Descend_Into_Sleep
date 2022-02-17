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
        private readonly double SetHealthPoints;
        private readonly int TurnCooldown;

        public ChaosEnsues() : base("Chaos Ensues")
        {
            Description = "You lose all your lifesteal and health, but your opponent is stunned.\n";
            ManaCost = 50;
            TurnsUntilDecast = 3;
            SetHealthPoints = 0.5;
            TurnCooldown = 7;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            var toStr = GetCastingString(caster);
            caster.SetLifeStealStatus(false);
            caster.SetHealthPoints(SetHealthPoints);
            opponent.Stun();
            Available = false;
            toStr += $"{opponent.GetName()} is now stunned!\nYou can no longer life steal!\nYour health is set to 0.5!\n";
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
            caster.SetLifeStealStatus(true);
            opponent.Unstun();
            var toStr = $"{caster.GetName()} can now life steal!\n{opponent.GetName()} is no longer stunned!\n";
            return toStr;
        }

        private string SecondDecast(Character caster, Character opponent)
        {
            Available = true;
            var toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}