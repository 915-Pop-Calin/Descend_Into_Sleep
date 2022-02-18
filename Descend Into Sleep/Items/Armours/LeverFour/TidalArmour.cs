using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class TidalArmour: Armour
    {
        public TidalArmour() : base(0, 30, 0)
        {
            Name = "Tidal Armour";
            Description = "Each turn, all DOT effects have their number of turns decreased by 1";
            SetActive();
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            caster.DecreaseDotEffects(1);
            var toStr = $"{caster.GetName()} has decreased the number of turns of all DOT effects by 1!\n";
            return toStr;
        }
    }
}