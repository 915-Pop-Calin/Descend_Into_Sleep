using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelThree
{
    public class TankBuster: Weapon
    {
        public TankBuster() : base(4, 0)
        {
            SetEffect();
            Description = "Each attack strikes twice";
            Name = "Tank Buster";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            caster.DealDirectDamage(opponent, damageDealt);
            var toStr = caster.GetName() + " did a double hit and dealt " + damageDealt.ToString() + " damage!\n";
            return toStr;
        }
    }
}