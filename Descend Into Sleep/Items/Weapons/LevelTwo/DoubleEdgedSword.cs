using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class DoubleEdgedSword : IWeapon, IPassive, IObtainable
    {
        public static readonly DoubleEdgedSword DOUBLE_EDGED_SWORD = new DoubleEdgedSword();
        private const double VALUE_INCREASED = 10;
        private const int TURNS_UNTIL_DECAST = 2;

        public double GetAttackValue()
        {
            return 20;
        }

        public string GetName()
        {
            return "Double Edged Sword";
        }

        public string GetDescription()
        {
            return
                $"Huge attack weapon, but your opponent gains {VALUE_INCREASED} attack for {TURNS_UNTIL_DECAST} Turns";
        }

        public double GetPrice()
        {
            return 800;
        }

        public string Passive(Character caster, Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            opponent.IncreaseAttackValue(VALUE_INCREASED);
            listOfTurns.Add(turnCounter + TURNS_UNTIL_DECAST, (c1, c2) => Decast(c1, c2));

            var toStr =
                $"{opponent.GetName()}'s attack was increased by {VALUE_INCREASED} for {TURNS_UNTIL_DECAST} turns by Double Edged Sword!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue())} attack!\n";
            return toStr;
        }

        private string Decast(Character caster, Character opponent)
        {
            opponent.IncreaseAttackValue(-VALUE_INCREASED);
            var toStr =
                $"{opponent.GetName()}'s attack was decreased back by {VALUE_INCREASED} by Double Edged Sword!\n";
            toStr += $"{opponent.GetName()} now has {Math.Round(opponent.GetAttackValue())} attack!\n";
            return toStr;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private DoubleEdgedSword()
        {
        }
    }
}