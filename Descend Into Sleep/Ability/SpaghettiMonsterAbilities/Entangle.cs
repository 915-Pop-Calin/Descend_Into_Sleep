using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.SpaghettiMonsterAbilities
{
    public class Entangle : Ability
    {
        public Entangle() : base("Entangle")
        {
            TurnsUntilDecast = 1;
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            opponent.Stun();
            string toStr = $"{caster.GetName()} entangles {opponent.GetName()}!\n";
            toStr += $"{opponent.GetName()} is stunned for {TurnsUntilDecast} turns!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            opponent.Unstun();
            string toStr = $"{opponent.GetName()} is no longer stunned!\n";
            return toStr;
        }
    }
}