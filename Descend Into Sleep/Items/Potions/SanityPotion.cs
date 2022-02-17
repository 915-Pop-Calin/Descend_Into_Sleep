using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class SanityPotion: Potion
    {
        private double SanityRestoringValue;
        
        public SanityPotion(): base()
        {
            Name = "Sanity Potion";
            Description = "You restore 30 of your sanity.\n";
            SanityRestoringValue = 30;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.RestoreSanity(SanityRestoringValue);
            var toStr = $"{humanPlayer.GetName()}'s sanity was increased by {SanityRestoringValue}!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
    }
}