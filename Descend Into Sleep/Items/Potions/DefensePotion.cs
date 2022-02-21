using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class DefensePotion: Potion
    {

        private double DefenseGained;
        private double HealthLost;
        
        public DefensePotion(): base()
        {
            Name = "Defense Potion";
            DefenseGained = 20;
            HealthLost = 5;
            Description = $"You gain {DefenseGained} defense, but you lose {HealthLost} health permanently\n";
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var originalDefense = humanPlayer.GetInnateDefense();
            var newDefense = originalDefense + DefenseGained;
            humanPlayer.SetInnateDefense(newDefense);
            humanPlayer.LoseHealthPoints(HealthLost);
            var toStr = $"{humanPlayer.GetName()}'s defense has been increased by {DefenseGained}," +
                        $" but their health points were reduced by {HealthLost}!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetDefenseValue())} defense and " +
                     $"{Math.Round(humanPlayer.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 100;
        }
    }
}