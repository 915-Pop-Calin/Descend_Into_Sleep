using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TitansFindings: Weapon
    {
        private readonly int MinimumSanityReduced;
        private readonly int MaximumSanityReduced;
        
        public TitansFindings() : base(5, 0, 0)
        {
            SetEffect();
            Name = "Titan's Findings";
            Description = "You restore sanity whenever you attack";
            MinimumSanityReduced = 1;
            MaximumSanityReduced = 10;
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            int randomChoice = RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            caster.RestoreSanity(randomChoice);
            var toStr = $"{caster.GetName()} has restored {randomChoice} of his sanity!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
    }
}