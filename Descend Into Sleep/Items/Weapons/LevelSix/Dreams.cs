using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelSix
{
    public class Dreams: IWeapon, IActive, IObtainable
    {
        private readonly int MinimumSanityReduced;
        private readonly int MaximumSanityReduced;

        public Dreams()
        {
            MinimumSanityReduced = 15;
            MaximumSanityReduced = 26;
        }

        public double GetAttackValue()
        {
            return 2;
        }
        
        public string GetName()
        {
            return "Dreams";
        }

        public string GetDescription()
        {
            return  $"Makes your enemy lose between {MinimumSanityReduced} and {MaximumSanityReduced} sanity," +
                    $" but it has no effect on monsters";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var randomInsanityReduced =
                RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            opponent.ReduceSanity(randomInsanityReduced);
            var toStr = $"{opponent.GetName()}'s sanity was reduced by {randomInsanityReduced}!\n{opponent.GetName()} " +
                        $"has {Math.Round(opponent.GetSanity(), 2)} sanity left!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 2400;
        }
    }
    
    
}