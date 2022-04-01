using ConsoleApp12.Characters;
namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class IcarusesTouch: IWeapon, IActive, IObtainable, IDefense
    {
        private readonly DotEffect DotEffect;

        public IcarusesTouch()
        {
            DotEffect = new DotEffect(5, 3);
        }
        public double GetAttackValue()
        {
            return 0;
        }

        public double GetDefenseValue()
        {
            return 3;
        }
        

        public string GetName()
        {
            return "Icarus's Touch";
        }

        public string GetDescription()
        {
            return "Puts a DOT on the enemy taking 3 damage per turn for 5 turns";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            opponent.AddDotEffect(DotEffect);
            var toStr = $"{opponent.GetName()} will take {DotEffect.DamagePerTurn} damage every turn for " +
                        $"{DotEffect.NumberOfTurns} turns!\n";
            return toStr;
        }
        
        public double GetPrice()
        {
            return 3600;
        }
    }
}