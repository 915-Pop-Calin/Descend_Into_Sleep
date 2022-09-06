using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class DefensePotion : IPotion, IObtainable
    {
        public static readonly DefensePotion DEFENSE_POTION = new DefensePotion();
        private const double DEFENSE_GAINED = 20;
        private const double HEALTH_LOST = 5;

        public string GetName()
        {
            return "Defense Potion";
        }

        public string GetDescription()
        {
            return $"You gain {DEFENSE_GAINED} defense, but you lose {HEALTH_LOST} health permanently\n";
        }

        public string UseItem(Character character)
        {
            var originalDefense = character.GetInnateDefense();
            var newDefense = originalDefense + DEFENSE_GAINED;
            character.SetInnateDefense(newDefense);
            character.LoseHealthPoints(HEALTH_LOST);
            var toStr = $"{character.GetName()}'s defense has been increased by {DEFENSE_GAINED}," +
                        $" but their health points were reduced by {HEALTH_LOST}!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetDefenseValue())} defense and " +
                     $"{Math.Round(character.GetHealthPoints(), 2)} health!\n";
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

        private DefensePotion()
        {
        }
    }
}