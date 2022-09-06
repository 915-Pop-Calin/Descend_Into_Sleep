using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class IcarusesTouch : IWeapon, IActive, IObtainable, IDefense
    {
        public static readonly IcarusesTouch ICARUSES_TOUCH = new IcarusesTouch();
        private const int NUMBER_OF_TURNS = 5;
        private const double DAMAGE_PER_TURN = 3;
        private static readonly DotEffect DOT_EFFECT = new DotEffect(NUMBER_OF_TURNS, DAMAGE_PER_TURN);

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
            opponent.AddDotEffect(DOT_EFFECT);
            var toStr = $"{opponent.GetName()} will take {DOT_EFFECT.DamagePerTurn} damage every turn for " +
                        $"{DOT_EFFECT.NumberOfTurns} turns!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 3600;
        }

        public int AvailabilityLevel()
        {
            return 5;
        }

        private IcarusesTouch()
        {
        }
    }
}