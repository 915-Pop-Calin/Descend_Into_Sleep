using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class TidalArmour : IArmour, IActive, IObtainable
    {
        public static readonly TidalArmour TIDAL_ARMOUR = new TidalArmour();

        public string GetName()
        {
            return "Tidal Armour";
        }

        public string GetDescription()
        {
            return "Each turn, all DOT effects have their number of turns decreased by 1";
        }

        public double GetDefenseValue()
        {
            return 30;
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            caster.DecreaseDotEffects(1);
            var toStr = $"{caster.GetName()} has decreased the number of turns of all DOT effects by 1!\n";
            return toStr;
        }

        public double GetPrice()
        {
            return 2800;
        }

        public int AvailabilityLevel()
        {
            return 5;
        }

        private TidalArmour()
        {
        }
    }
}