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
            Description = "You heal for 10 health points.\n";
            HealingValue = 10;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            humanPlayer.Heal(HealingValue);
            var toStr = $"{humanPlayer.GetName()} has healed for {HealingValue} health points!\n";
            return toStr;
        }
    }
}