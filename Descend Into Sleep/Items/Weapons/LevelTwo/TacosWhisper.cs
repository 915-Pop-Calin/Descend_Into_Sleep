using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelTwo
{
    public class TacosWhisper: Weapon
    {
        private int _turnCounter;
        
        public TacosWhisper() : base(5, 0)
        {
            SetEffect();
            _turnCounter = 0;
            Description = "Each fourth shot strikes thrice";
            Name = "Taco's Whisper";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var toStr = "";
            if (_turnCounter == 3)
            {
                caster.DealDirectDamage(opponent, 2 * damageDealt);
                _turnCounter = 0;
                toStr += "Taco's whisper has dealt " + (2 * damageDealt).ToString() +
                         " damage with the fourth shot!\n";
            }
            else
            {
                _turnCounter++;
            }
            return toStr;
        }
    }
}