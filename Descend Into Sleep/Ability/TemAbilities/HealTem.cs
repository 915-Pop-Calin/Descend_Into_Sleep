using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.TemAbilities
{
    public class HealTem : Ability
    {
        private const double HEALTH_HEALED = 1;

        public HealTem() : base("Heal Tem")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            caster.Heal(HEALTH_HEALED);
            string toStr = $"{caster.GetName()} heals for {HEALTH_HEALED}!\n";
            toStr += $"{caster.GetName()} now has {caster.GetHealthPoints()} health!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}