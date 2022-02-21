using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class ManaElixir: Potion
    {
        private double ManaRestoredPerLevel;
        
        public ManaElixir() : base()
        {
            Name = "Mana Elixir";
            ManaRestoredPerLevel = 1.5;
            Description = $"You restore {ManaRestoredPerLevel} mana per level.\n";
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var playerLevel = humanPlayer.GetLevel();
            var manaRestored = ManaRestoredPerLevel * playerLevel;
            humanPlayer.GainMana(manaRestored);
            var toStr = $"{humanPlayer.GetName()} has restored {manaRestored} of their mana!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetMana(), 2)} mana!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 100;
        }
    }
}