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
            SetActive();
            Name = "Titan's Findings";
            MinimumSanityReduced = 1;
            MaximumSanityReduced = 10;       
            Description = $"You restore between {MinimumSanityReduced} and {MaximumSanityReduced} sanity whenever you attack";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            int randomChoice = RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            caster.RestoreSanity(randomChoice);
            var toStr = $"{caster.GetName()} has restored {randomChoice} of his sanity!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 1000;
        }
    }
}