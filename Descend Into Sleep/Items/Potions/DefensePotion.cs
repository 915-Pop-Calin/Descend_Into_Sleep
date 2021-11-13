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
            Description = "You increase your defense points at the cost of your health.\n";
            Name = "Defense Potion";
            DefenseGained = 20;
            HealthLost = 5;
        }

        public override string UseItem(HumanPlayer humanPlayer)
        {
            var playerName = humanPlayer.GetName();
            var originalDefense = humanPlayer.GetInnateDefense();
            var newDefense = originalDefense + DefenseGained;
            humanPlayer.SetInnateDefense(newDefense);
            humanPlayer.PermanentlyReduceHealthPoints(HealthLost);
            var toStr = playerName + "'s defense has been increased by" + DefenseGained.ToString();
            toStr += ", but their health points were reduced by " + HealthLost.ToString() + "!\n";
            return toStr;
        }
    }
}