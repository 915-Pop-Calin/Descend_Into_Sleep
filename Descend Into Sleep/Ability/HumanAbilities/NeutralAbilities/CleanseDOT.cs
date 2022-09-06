using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class CleanseDOT : Ability
    {
        public CleanseDOT() : base("Cleanse DOT")
        {
            ManaCost = 15;
            Description = "You clear all the DOT effects which are currently affecting you\n";
        }

        public override void ResetDescription()
        {
            Description = "You clear all the DOT effects which are currently affecting you\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            caster.ClearDotEffects();
            toStr += $"{caster.GetName()} cleared all their dot effects!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}