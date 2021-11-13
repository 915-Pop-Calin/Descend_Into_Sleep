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
            var playerName = humanPlayer.GetName();
            humanPlayer.Heal(HealingValue);
            var toStr = playerName + " has healed for " + HealingValue.ToString() + " health points!\n";
            return toStr;
        }
    }
}