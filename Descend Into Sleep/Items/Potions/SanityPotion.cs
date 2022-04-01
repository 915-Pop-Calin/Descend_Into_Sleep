using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class SanityPotion: IPotion, IObtainable
    {
        private double SanityRestoringValue;
        
        public SanityPotion()
        {
            SanityRestoringValue = 30;
        }

        public string GetName()
        {
            return "Sanity Potion";
        }

        public string GetDescription()
        {
            return $"You restore {SanityRestoringValue} of your sanity.\n";
        }

        
        public string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.RestoreSanity(SanityRestoringValue);
            var toStr = $"{humanPlayer.GetName()}'s sanity was increased by {SanityRestoringValue}!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetSanity(), 2)} sanity!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 100;
        }
    }
}