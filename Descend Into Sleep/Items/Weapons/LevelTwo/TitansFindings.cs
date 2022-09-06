using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TitansFindings : IWeapon, IActive, IObtainable
    {
        public static readonly TitansFindings TITANS_FINDINGS = new TitansFindings();
        private const int MINIMUM_SANITY_REDUCED = 1;
        private const int MAXIMUM_SANITY_REDUCED = 10;

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
            return
                $"You restore between {MINIMUM_SANITY_REDUCED} and {MAXIMUM_SANITY_REDUCED} sanity whenever you attack";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            int randomChoice = RandomHelper.GenerateRandomInInterval(MINIMUM_SANITY_REDUCED, MAXIMUM_SANITY_REDUCED);
            caster.RestoreSanity(randomChoice);
            var toStr = $"{caster.GetName()} has restored {randomChoice} of his sanity!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetSanity(), 2)} sanity!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 1000;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private TitansFindings()
        {
        }
    }
}