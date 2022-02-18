using System;
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
            HealingPerLevel = 1.5;
            Description = $"You heal for {HealingPerLevel} * Level.\n";
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var humanPlayerLevel = humanPlayer.GetLevel();
            var healingDone = HealingPerLevel * humanPlayerLevel;
            humanPlayer.Heal(healingDone);
            var toStr = $"{humanPlayer.GetName()} has healed for {healingDone} health points!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }
    }
}