using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class TidalArmour: Armour
    {
        public TidalArmour() : base(0, 30, 0)
        {
            Name = "Tidal Armour";
            Description = "Helps one put out fire";
            SetActive();
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            caster.DecreaseDotEffects(1);
            var toStr = $"{caster.GetName()} has decreased all dot effects applied by 1!\n";
            return toStr;
        }
    }
}