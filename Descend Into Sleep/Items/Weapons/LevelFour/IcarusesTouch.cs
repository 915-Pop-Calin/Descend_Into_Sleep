using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class IcarusesTouch: Weapon
    {
        public IcarusesTouch() : base(0, 3, 0)
        {
            Name = "Icarus's Touch";
            var dotEffect = new DotEffect(5, 3);
            DotEffect = dotEffect;
            SetActive();
            Description = "Puts a DOT on the enemy taking 3 damage per turn for 5 turns";
        }

        public override string Active(double damageDealt, Character caster, Character opponent)
        {
            opponent.AddDotEffect(DotEffect);
            var toStr = $"{opponent.GetName()} will take {DotEffect.DamagePerTurn} damage every turn for " +
                        $"{DotEffect.NumberOfTurns} turns!\n";
            return toStr;
        }
        
        public override double GetPrice()
        {
            return 3600;
        }
    }
}