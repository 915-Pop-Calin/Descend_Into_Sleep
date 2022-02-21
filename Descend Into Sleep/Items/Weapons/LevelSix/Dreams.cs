using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelSix
{
    public class Dreams: Weapon
    {
        private readonly int MinimumSanityReduced;
        private readonly int MaximumSanityReduced;

        public Dreams() : base(2, 0, 0)
        {
            Name = "Dreams";
            SetActive();
            MinimumSanityReduced = 15;
            MaximumSanityReduced = 26;
            Description = $"Makes your enemy lose between {MinimumSanityReduced} and {MaximumSanityReduced} sanity," +
                          $" but it has no effect on monsters";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            var randomInsanityReduced =
                RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            opponent.ReduceSanity(randomInsanityReduced);
            var toStr = $"{opponent.GetName()}'s sanity was reduced by {randomInsanityReduced}!\n{opponent.GetName()} " +
                        $"has {Math.Round(opponent.GetSanity(), 2)} sanity left!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 2400;
        }
    }
    
    
}