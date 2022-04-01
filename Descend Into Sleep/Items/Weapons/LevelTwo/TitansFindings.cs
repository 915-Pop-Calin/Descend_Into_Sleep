using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TitansFindings: IWeapon, IActive, IObtainable
    {
        private readonly int MinimumSanityReduced;
        private readonly int MaximumSanityReduced;
        
        public TitansFindings()
        {
            MinimumSanityReduced = 1;
            MaximumSanityReduced = 10;
        }

        public double GetAttackValue()
        {
            return 5;
        }

        public string GetName()
        {
            return "Titan's Findings";
        }

        public string GetDescription()
        {
            return $"You restore between {MinimumSanityReduced} and {MaximumSanityReduced} sanity whenever you attack";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            int randomChoice = RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            caster.RestoreSanity(randomChoice);
            var toStr = $"{caster.GetName()} has restored {randomChoice} of his sanity!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 1000;
        }
    }
}