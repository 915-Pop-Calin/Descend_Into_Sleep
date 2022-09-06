using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class HealthPotion : IPotion, IObtainable
    {
        public static readonly HealthPotion HEALTH_POTION = new HealthPotion();
        private const double HEALING_VALUE = 10;

        public string GetName()
        {
            return "Health Potion";
        }

        public string GetDescription()
        {
            return $"You heal for {HEALING_VALUE} health points.\n";
        }


        public string UseItem(Character character)
        {
            character.Heal(HEALING_VALUE);
            var toStr = $"{character.GetName()} has healed for {HEALING_VALUE} health points!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 10;
        }

        public int AvailabilityLevel()
        {
            return 1;
        }

        private HealthPotion()
        {
        }
    }
}