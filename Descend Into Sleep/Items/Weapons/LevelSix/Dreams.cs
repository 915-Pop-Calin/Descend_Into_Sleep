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
            Description = "Makes your enemy go insane. Has no effect on monsters";
            SetEffect();
            MinimumSanityReduced = 15;
            MaximumSanityReduced = 26;
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var randomInsanityReduced =
                RandomHelper.GenerateRandomInInterval(MinimumSanityReduced, MaximumSanityReduced);
            opponent.ReduceSanity(randomInsanityReduced);
            var toStr = $"{opponent.GetName()}'s sanity was reduced by {randomInsanityReduced}!\n{opponent.GetName()} " +
                        $"has {opponent.GetSanity()} sanity left!\n";
            return toStr;
        }
    }
}