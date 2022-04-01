using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class SteelPlateau: IArmour, IPassive, IObtainable
    {
        private readonly double DamagePerTurn;
        public SteelPlateau()
        {
            DamagePerTurn = 5;
        }

        public string GetName()
        {
            return "Steel Plateau";
        }

        public string GetDescription()
        {
            return $"Very strong armour which deals {DamagePerTurn} true damage to you every Turn";
        }
        
        public double GetDefenseValue()
        {
            return 200;
        }
        
        public string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster, 5);
            var toStr = $"Steel Plateau has dealt {DamagePerTurn} True Damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 800;
        }
    }
}