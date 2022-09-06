using ConsoleApp12.Characters;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class FireDeflector : IArmour, IActive, IObtainable
    {
        public static readonly FireDeflector FIRE_DEFLECTOR = new FireDeflector();
        private const double DEFLECTION_CHANCE = 0.1;

        public string GetName()
        {
            return "Fire Deflector";
        }

        public string GetDescription()
        {
            return $"Has {DEFLECTION_CHANCE * 100}% chance to deflect all DOT effects on the enemy";
        }

        public double GetDefenseValue()
        {
            return 75;
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var willDeflect = RandomHelper.IsSuccessfulTry(DEFLECTION_CHANCE);
            var toStr = "";
            if (willDeflect)
            {
                var dotEffects = caster.GetDotEffects();
                foreach (var dotEffect in dotEffects)
                    opponent.AddDotEffect(dotEffect);
                caster.ClearDotEffects();
                toStr = $"{caster.GetName()} has deflected his DOT effects onto {opponent.GetName()}!\n";
            }

            return toStr;
        }

        public double GetPrice()
        {
            return 3200;
        }

        public int AvailabilityLevel()
        {
            return 5;
        }

        private FireDeflector()
        {
        }
    }
}