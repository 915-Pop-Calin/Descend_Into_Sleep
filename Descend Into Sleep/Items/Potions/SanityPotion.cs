using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class SanityPotion : IPotion, IObtainable
    {
        public static readonly SanityPotion SANITY_POTION = new SanityPotion();
        private const double SANITY_RESTORING_VALUE = 30;

        public string GetName()
        {
            return "Sanity Potion";
        }

        public string GetDescription()
        {
            return $"You restore {SANITY_RESTORING_VALUE} of your sanity.\n";
        }


        public string UseItem(Character character)
        {
            character.RestoreSanity(SANITY_RESTORING_VALUE);
            var toStr = $"{character.GetName()}'s sanity was increased by {SANITY_RESTORING_VALUE}!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetSanity(), 2)} sanity!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 100;
        }

        public int AvailabilityLevel()
        {
            return 1;
        }

        private SanityPotion()
        {
        }
    }
}