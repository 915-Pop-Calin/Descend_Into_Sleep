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
            var playerName = humanPlayer.GetName();
            var playerLevel = humanPlayer.GetLevel();
            var manaRestored = ManaRestoredPerLevel * playerLevel;
            humanPlayer.GainMana(manaRestored);
            var toStr = playerName + " has restored " + manaRestored.ToString() + " of their mana!\n";
            return toStr;
        }
    }
}