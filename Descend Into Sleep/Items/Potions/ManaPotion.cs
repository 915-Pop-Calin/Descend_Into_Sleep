using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class ManaPotion: Potion
    {

        private double RestoredValue;
        
        public ManaPotion(): base()
        {
            Name = "Mana Potion";
            RestoredValue = 10;
            Description = $"You restore {RestoredValue} mana\n";
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.GainMana(RestoredValue);
            var toStr = $"{humanPlayer.GetName()} has restored {RestoredValue} of their mana!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetMana(), 2)} mana!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 20;
        }
    }
}