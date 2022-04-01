using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class OffensePotion: IPotion, IObtainable
    {
        private double AttackGained;
        private double DefenseLost;
        
        public OffensePotion()
        {
            AttackGained = 20;
            DefenseLost = 20;
        }

        public string GetName()
        {
            return "Offense Potion";
        }

        public string GetDescription()
        {
            return $"You gain {AttackGained} attack, but you lose {DefenseLost} defense permanently\n";
        }

        
        public string UseItem(HumanPlayer humanPlayer)
        {
            var originalDefense = humanPlayer.GetInnateDefense();
            var originalAttack = humanPlayer.GetInnateAttack();
            var newDefense = originalDefense - DefenseLost;
            var newAttack = originalAttack + AttackGained;
            humanPlayer.SetInnateAttack(newAttack);
            humanPlayer.SetInnateDefense(newDefense);
            var toStr = $"{humanPlayer.GetName()}'s attack was increased by {AttackGained}, but their defense was " +
                        $"decreased by {DefenseLost}!\n";
            toStr += $"{humanPlayer.GetName()} now has {Math.Round(humanPlayer.GetAttackValue(), 2)} attack and" +
                     $" {Math.Round(humanPlayer.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 100;
        }
    }
}