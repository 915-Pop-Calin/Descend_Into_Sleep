using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelSix
{
    public class Dreams: Weapon
    {
        public Dreams() : base(2, 0, 0)
        {
            Name = "Dreams";
            Description = "Makes your enemy go insane. Has no effect on monsters";
            SetEffect();
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var randomObject = new Random();
            var randomInsanityReduced = randomObject.Next(15, 26);
            opponent.ReduceSanity(randomInsanityReduced);
            var toStr = opponent.GetName() + "'s sanity was reduced by " + randomInsanityReduced.ToString() + " !\n";
            toStr += opponent.GetName() + " has " + opponent.GetSanity().ToString() + " sanity left!\n";
            return toStr;
        }
    }
}