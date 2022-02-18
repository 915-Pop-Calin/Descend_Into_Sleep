using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class BoilingBlood: Weapon
    {
        public BoilingBlood() : base(40, 0, 0)
        {
            SetPassive();
            SetLifeSteal(1.5);
            Name = "Boiling Blood";
            Description = "Very strong life stealer, but at a great cost";
        }

        public override string Passive(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster,40);
            var toStr = $"Boiling Blood has dealt 40 true damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
    }
}