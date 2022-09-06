using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class ManaPotion : IPotion, IObtainable
    {
        public static readonly ManaPotion MANA_POTION = new ManaPotion();
        private const double RESTORED_VALUE = 10;

        public string GetName()
        {
            return "Mana Potion";
        }

        public string GetDescription()
        {
            return $"You restore {RESTORED_VALUE} mana\n";
        }

        public string UseItem(Character character)
        {
            character.GainMana(RESTORED_VALUE);
            var toStr = $"{character.GetName()} has restored {RESTORED_VALUE} of their mana!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetMana(), 2)} mana!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 20;
        }

        public int AvailabilityLevel()
        {
            return 1;
        }

        private ManaPotion()
        {
        }
    }
}