using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class GrainOfSalt: IPotion, IObtainable
    {
        private double HealingPerLevel;
        
        public GrainOfSalt()
        {
            HealingPerLevel = 1.5;
        }

        public string GetName()
        {
            return "Grain Of Salt";
        }

        public string GetDescription()
        {
            return $"You heal for {HealingPerLevel} * Level.\n";
        }

        
        public string UseItem(HumanPlayer humanPlayer)
        {
            var humanPlayerLevel = humanPlayer.GetLevel();
            var healingDone = HealingPerLevel * humanPlayerLevel;
            humanPlayer.Heal(healingDone);
            var toStr = $"{humanPlayer.GetName()} has healed for {healingDone} health points!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 50;
        }
    }
}