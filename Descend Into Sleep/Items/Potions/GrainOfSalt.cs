using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class GrainOfSalt: Potion
    {
        private double HealingPerLevel;
        
        public GrainOfSalt() : base()
        {
            Name = "Grain Of Salt";
            Description = "You heal for 1.5 health points per level.\n";
            HealingPerLevel = 1.5;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var humanPlayerLevel = humanPlayer.GetLevel();
            var healingDone = HealingPerLevel * humanPlayerLevel;
            humanPlayer.Heal(healingDone);
            var toStr = $"{humanPlayer.GetName()} has healed for {healingDone} health points!\n";
            return toStr;
        }
    }
}