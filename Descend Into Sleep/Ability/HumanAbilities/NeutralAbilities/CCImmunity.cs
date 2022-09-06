using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NeutralAbilities
{
    public class CCImmunity : Ability
    {
        public CCImmunity() : base("CC Immunity")
        {
            ManaCost = 20;
            TurnsUntilDecast = 5;
            Description = $"You become immune to CC for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You become immune to CC for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            caster.SetStunResistant(true);
            toStr += $"{caster.GetName()} is immune to CC for {TurnsUntilDecast} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            caster.SetStunResistant(false);
            string toStr = $"{caster.GetName()} is no longer immune to CC!\n";
            return toStr;
        }
    }
}