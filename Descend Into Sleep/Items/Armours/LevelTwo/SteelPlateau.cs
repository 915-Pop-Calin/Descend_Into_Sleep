using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class SteelPlateau: Armour
    {
        public SteelPlateau() : base(0, 200, 0)
        {
            SetPassive();
            Name = "Steel Plateau";
            Description = "Very strong armour which damages you each turn";
        }

        public override string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster, 5);
            var toStr = $"Steel Plateau has dealt 5 True Damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
    }
}