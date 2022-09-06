using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class ManaElixir : IPotion, IObtainable
    {
        public static readonly ManaElixir MANA_ELIXIR = new ManaElixir();
        private const double MANA_RESTORED_PER_LEVEL = 1.5;

        public string GetName()
        {
            return "Mana Elixir";
        }

        public string GetDescription()
        {
            return $"You restore {MANA_RESTORED_PER_LEVEL} mana per level.\n";
        }


        public string UseItem(Character character)
        {
            var playerLevel = character.GetLevel();
            var manaRestored = MANA_RESTORED_PER_LEVEL * playerLevel;
            character.GainMana(manaRestored);
            var toStr = $"{character.GetName()} has restored {manaRestored} of their mana!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetMana(), 2)} mana!\n";
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

        private ManaElixir()
        {
        }
    }
}