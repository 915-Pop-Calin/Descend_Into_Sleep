using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class BoilingBlood: IWeapon, IPassive, IObtainable, ILifeSteal
    {
        private readonly double DamagePerTurn;
        
        public BoilingBlood()
        {
            DamagePerTurn = 40;
        }

        public double GetAttackValue()
        {
            return 40;
        }

        public string GetName()
        {
            return "Boiling Blood";
        }

        public string GetDescription()
        {
            return $"Very strong life stealer, but you take {DamagePerTurn} true damage every turn";
        }
        
        public double GetLifeSteal()
        {
            return 1.5;
        }
        
        public string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster,DamagePerTurn);
            var toStr = $"Boiling Blood has dealt {DamagePerTurn} true damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 1500;
        }
    }
}