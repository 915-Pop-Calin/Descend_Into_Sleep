using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TacosWhisper: IWeapon, IActive, IObtainable
    {
        private int _turnCounter;
        
        public TacosWhisper()
        {
            _turnCounter = 0;
        }

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
            return  "Each fourth shot strikes thrice";
        }
        
        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var toStr = "";
            if (_turnCounter == 3)
            {
                caster.DealDirectDamage(opponent, 2 * damageDealt);
                _turnCounter = 0;
                toStr += $"Taco's whisper has dealt {2 * damageDealt} damage with the fourth shot!\n";
                toStr += $"{opponent.GetName()} is left with {Math.Round(opponent.GetAttackValue())} health!\n";
            }
            else
            {
                _turnCounter++;
            }
            return toStr;
        }
        
        public double GetPrice()
        {
            return 1000;
        }
    }
}