using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class SteelPlateau: Armour
    {
        public SteelPlateau() : base(0, 200)
        {
            SetEffect();
            Name = "Steel Plateau";
            Description = "Very strong armour which damages you each turn";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            caster.DealDirectDamage(caster, 5);
            var toStr = "Steel Plateau has dealt 5 True Damage to " + caster.GetName() + "!\n";
            toStr += caster.GetName() + " is left with " + caster.GetHealthPoints().ToString() + " health!\n";
            return toStr;
        }
    }
}