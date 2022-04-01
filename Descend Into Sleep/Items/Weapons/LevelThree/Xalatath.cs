using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class Xalatath: IWeapon, IActive, IObtainable, ILifeSteal
    {

        public double GetAttackValue()
        {
            return 10;
        }
        
        public string GetName()
        {
            return "Xalatath";
        }

        public string GetDescription()
        {
            return "Strong life stealer which lifesteals sanity as well";
        }
        

        public double GetLifeSteal()
        {
            return 0.75;
        }
        
        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var minimumSanityRestored = Convert.ToInt32(Math.Floor(damageDealt / 2));
            var maximumSanityRestored = Convert.ToInt32(Math.Floor(damageDealt));
            var sanityRestored = RandomHelper.GenerateRandomInInterval(minimumSanityRestored, maximumSanityRestored);
            caster.RestoreSanity(sanityRestored);
            var toStr = $"{caster.GetName()} has restored {sanityRestored} of his sanity!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 1800;
        }
    }
}