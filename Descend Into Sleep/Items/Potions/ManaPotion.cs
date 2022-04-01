using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class ManaPotion: IPotion, IObtainable
    {

        private double RestoredValue;
        
        public ManaPotion()
        {
            RestoredValue = 10;
        }

        public string GetName()
        {
            return "Mana Potion";
        }

        public string GetDescription()
        {
            return $"You restore {RestoredValue} mana\n";
        }

        
        public string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.GainMana(RestoredValue);
            var toStr = $"{humanPlayer.GetName()} has restored {RestoredValue} of their mana!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetMana(), 2)} mana!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 20;
        }
    }
}