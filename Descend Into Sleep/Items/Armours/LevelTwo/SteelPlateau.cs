using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class SteelPlateau : IArmour, IPassive, IObtainable
    {
        public static readonly SteelPlateau STEEL_PLATEAU = new SteelPlateau();
        private const double DAMAGE_PER_TURN = 5;

        public string GetName()
        {
            return "Steel Plateau";
        }

        public string GetDescription()
        {
            return $"Very strong armour which deals {DAMAGE_PER_TURN} true damage to you every Turn";
        }

        public double GetDefenseValue()
        {
            return 200;
        }

        public string Passive(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            caster.DealDirectDamage(caster, 5);
            var toStr = $"Steel Plateau has dealt {DAMAGE_PER_TURN} True Damage to {caster.GetName()}!\n";
            toStr += $"{caster.GetName()} is left with {Math.Round(caster.GetHealthPoints(), 2)} health!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 800;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private SteelPlateau()
        {
        }
    }
}