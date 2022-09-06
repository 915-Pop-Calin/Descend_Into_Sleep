using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TwoHandedMace : IWeapon, IPassive, IObtainable
    {
        public static readonly TwoHandedMace TWO_HANDED_MACE = new TwoHandedMace();

        public double GetAttackValue()
        {
            return 45;
        }

        public string GetName()
        {
            return "Two Handed Mace";
        }

        public string GetDescription()
        {
            return "Strong attack weapon, but stuns you for one turn";
        }

        public string Passive(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            String toStr;
            try
            {
                caster.Stun();
                listOfTurns.Add(turnCounter + 2, (c1, c2) => Decast(c1, c2));
                toStr = $"{caster.GetName()} was stunned for a turn by Two Handed Mace!\n";
            }
            catch (StunException stunException)
            {
                toStr = stunException.Message;
            }

            return toStr;
        }

        private string Decast(Character caster, Character opponent)
        {
            caster.Unstun();
            var toStr = $"{caster.GetName()} can attack now!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 750;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private TwoHandedMace()
        {
        }
    }
}