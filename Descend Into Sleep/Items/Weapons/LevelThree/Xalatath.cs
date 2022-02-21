using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class Xalatath: Weapon
    {
        public Xalatath() : base(10, 0,0)
        {
            SetLifeSteal(0.75);
            Description = "Strong life stealer which lifesteals sanity as well";
            Name = "Xalatath";
            SetActive();
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            var minimumSanityRestored = Convert.ToInt32(Math.Floor(damageDealt / 2));
            var maximumSanityRestored = Convert.ToInt32(Math.Floor(damageDealt));
            var sanityRestored = RandomHelper.GenerateRandomInInterval(minimumSanityRestored, maximumSanityRestored);
            caster.RestoreSanity(sanityRestored);
            var toStr = $"{caster.GetName()} has restored {sanityRestored} of his sanity!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 1800;
        }
    }
}