using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class HealthPotion: Potion
    {

        private double HealingValue;
        
        public HealthPotion() : base()
        {
            Name = "Health Potion";
            HealingValue = 10;
            Description = $"You heal for {HealingValue} health points.\n";
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.Heal(HealingValue);
            var toStr = $"{humanPlayer.GetName()} has healed for {HealingValue} health points!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 10;
        }
    }
}