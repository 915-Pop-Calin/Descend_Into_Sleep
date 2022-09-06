using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class OffensePotion : IPotion, IObtainable
    {
        public static readonly OffensePotion OFFENSE_POTION = new OffensePotion();
        private const double ATTACK_GAINED = 20;
        private const double DEFENSE_LOST = 20;

        public string GetName()
        {
            return "Offense Potion";
        }

        public string GetDescription()
        {
            return $"You gain {ATTACK_GAINED} attack, but you lose {DEFENSE_LOST} defense permanently\n";
        }


        public string UseItem(Character character)
        {
            var originalDefense = character.GetInnateDefense();
            var originalAttack = character.GetInnateAttack();
            var newDefense = originalDefense - DEFENSE_LOST;
            var newAttack = originalAttack + ATTACK_GAINED;
            character.SetInnateAttack(newAttack);
            character.SetInnateDefense(newDefense);
            var toStr = $"{character.GetName()}'s attack was increased by {ATTACK_GAINED}, but their defense was " +
                        $"decreased by {DEFENSE_LOST}!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetAttackValue(), 2)} attack and" +
                     $" {Math.Round(character.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 100;
        }

        public int AvailabilityLevel()
        {
            return 1;
        }

        private OffensePotion()
        {
        }
    }
}