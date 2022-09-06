using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class LastStand : IArmour, IPassive, IObtainable
    {
        public static readonly LastStand LAST_STAND = new LastStand();
        private const double DEFENSE_LOST = 100;
        private const double THRESHHOLD = 0.3;
        private const int TURNS_UNTIL_DECAST = 3;

        public string GetName()
        {
            return "Last Stand";
        }

        public string GetDescription()
        {
            return $"Great armour which gets your defense decreased by {DEFENSE_LOST} for {TURNS_UNTIL_DECAST} Turns " +
                   $"if under {THRESHHOLD * 100}% health";
        }

        public double GetDefenseValue()
        {
            return 400;
        }

        public string Passive(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var toStr = "";
            if (caster.GetHealthPoints() / caster.GetMaximumHealthPoints() < THRESHHOLD)
            {
                caster.IncreaseDefenseValue(-DEFENSE_LOST);
                toStr =
                    $"Due to {caster.GetName()} being under {THRESHHOLD * 100}% health, his defense was reduced by {DEFENSE_LOST} " +
                    $"for {TURNS_UNTIL_DECAST} turns!\n";
                toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
                listOfTurns.Add(turnCounter + TURNS_UNTIL_DECAST, (c1, c2) => Decast(c1, c2));
            }

            return toStr;
        }

        private string Decast(Character caster, Character opponent)
        {
            caster.IncreaseDefenseValue(DEFENSE_LOST);
            var toStr = $"{caster.GetName()}'s defenses were brought back to normal!\n";
            toStr += $"{caster.GetName()} now has {Math.Round(caster.GetDefenseValue(), 2)} defense!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 1800;
        }

        public int AvailabilityLevel()
        {
            return 4;
        }

        private LastStand()
        {
        }
    }
}