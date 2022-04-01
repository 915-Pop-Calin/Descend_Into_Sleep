using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class HealthPotion: IPotion, IObtainable
    {

        private double HealingValue;
        
        public HealthPotion() : base()
        {
            HealingValue = 10;
        }

        public string GetName()
        {
            return "Health Potion";
        }

        public string GetDescription()
        {
            return $"You heal for {HealingValue} health points.\n";
        }

        
        public string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.Heal(HealingValue);
            var toStr = $"{humanPlayer.GetName()} has healed for {HealingValue} health points!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 10;
        }
    }
}