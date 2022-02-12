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
            Description = "You restore 1.5 mana per level.\n";
            ManaRestoredPerLevel = 1.5;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var playerLevel = humanPlayer.GetLevel();
            var manaRestored = ManaRestoredPerLevel * playerLevel;
            humanPlayer.GainMana(manaRestored);
            var toStr = $"{humanPlayer.GetName()} has restored {manaRestored} of their mana!\n";
            return toStr;
        }
    }
}