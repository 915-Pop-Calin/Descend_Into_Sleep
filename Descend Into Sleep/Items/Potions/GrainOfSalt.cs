using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Potions
{
    public class GrainOfSalt : IPotion, IObtainable
    {
        public static readonly GrainOfSalt GRAIN_OF_SALT = new GrainOfSalt();
        private const double HEALING_PER_LEVEL = 1.5;

        public string GetName()
        {
            return "Grain Of Salt";
        }

        public string GetDescription()
        {
            return $"You heal for {HEALING_PER_LEVEL} * Level.\n";
        }


        public string UseItem(Character character)
        {
            var humanPlayerLevel = character.GetLevel();
            var healingDone = HEALING_PER_LEVEL * humanPlayerLevel;
            character.Heal(healingDone);
            var toStr = $"{character.GetName()} has healed for {healingDone} health points!\n";
            toStr += $"{character.GetName()} now has {Math.Round(character.GetHealthPoints(), 2)} health points!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 50;
        }

        public int AvailabilityLevel()
        {
            return 1;
        }

        private GrainOfSalt()
        {
        }
    }
}