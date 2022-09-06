using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TacosWhisper : IWeapon, IActive, IObtainable
    {
        public static readonly TacosWhisper TACOS_WHISPER = new TacosWhisper();
        private int TurnCounter;

        public double GetAttackValue()
        {
            return 5;
        }

        public string GetName()
        {
            return "Taco's Whisper";
        }

        public string GetDescription()
        {
            return "Each fourth shot strikes thrice";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var toStr = "";
            if (TurnCounter == 3)
            {
                caster.DealDirectDamage(opponent, 2 * damageDealt);
                TurnCounter = 0;
                toStr += $"Taco's whisper has dealt {2 * damageDealt} damage with the fourth shot!\n";
                toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetAttackValue())} health!\n";
            }
            else
            {
                TurnCounter++;
            }

            return toStr;
        }

        public double GetPrice()
        {
            return 1000;
        }

        public int AvailabilityLevel()
        {
            return 3;
        }

        private TacosWhisper()
        {
            TurnCounter = 0;
        }
    }
}