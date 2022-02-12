using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items.Potions
{
    public class OffensePotion: Potion
    {
        private double AttackGained;
        private double DefenseLost;
        
        public OffensePotion() : base()
        {
            Name = "Offense Potion";
            Description = "You gain attack, but lose defense points in return.\n";
            AttackGained = 20;
            DefenseLost = 20;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var originalDefense = humanPlayer.GetInnateDefense();
            var originalAttack = humanPlayer.GetInnateAttack();
            var newDefense = originalDefense - DefenseLost;
            var newAttack = originalAttack + AttackGained;
            humanPlayer.SetInnateAttack(newAttack);
            humanPlayer.SetInnateDefense(newDefense);
            var toStr = $"{humanPlayer.GetName()}'s attack was increased by {AttackGained}, but their defense was " +
                        $"decreased by {DefenseLost}!\n";
            return toStr;
        }
    }
}