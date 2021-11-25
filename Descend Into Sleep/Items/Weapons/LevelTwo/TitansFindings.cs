using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TitansFindings: Weapon
    {
        public TitansFindings() : base(5, 0, 0)
        {
            SetEffect();
            Name = "Titan's Findings";
            Description = "You restore sanity whenever you attack";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var randomObject = new Random();
            int randomChoice = randomObject.Next(1, 10);
            var toStr = caster.GetName() + " has restored " + randomChoice.ToString() + " of his sanity!\n";
            toStr += caster.GetName() + " is left with " + caster.GetSanity().ToString() + " sanity!\n";
            return toStr;
        }
    }
}