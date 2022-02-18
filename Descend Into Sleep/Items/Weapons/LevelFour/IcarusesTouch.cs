using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class IcarusesTouch: Weapon
    {
        public IcarusesTouch() : base(0, 3, 0)
        {
            Name = "Icarus's Touch";
            Description = "Very strong DOTer";
            var dotEffect = new DotEffect(5, 3);
            DotEffect = dotEffect;
            SetActive();
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            opponent.AddDotEffect(DotEffect);
            var toStr = $"{opponent.GetName()} will take {DotEffect.DamagePerTurn} damage every turn for " +
                        $"{DotEffect.NumberOfTurns} turns!\n";
            return toStr;
        }
    }
}