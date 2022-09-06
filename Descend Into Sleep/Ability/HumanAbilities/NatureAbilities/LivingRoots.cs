using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.NatureAbilities
{
    public class LivingRoots : Ability
    {
        private const int MINIMUM_NUMBER_OF_IMMUNE_TURNS = 11;

        public LivingRoots() : base("Living Roots")
        {
            ManaCost = 25;
            TurnsUntilDecast = 3;
            Description = $"You stun your opponent for {TurnsUntilDecast} Turns, but they become immune to stuns " +
                          $"for {MINIMUM_NUMBER_OF_IMMUNE_TURNS - Level} Turns\n";
        }

        public override void ResetDescription()
        {
            Description = $"You stun your opponent for {TurnsUntilDecast} Turns, but they become immune to stuns " +
                          $"for {MINIMUM_NUMBER_OF_IMMUNE_TURNS - Level} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = GetCastingString(caster);
            int numbersOfTurnsImmune = MINIMUM_NUMBER_OF_IMMUNE_TURNS - Level;
            toStr += $"{opponent.GetName()} was stunned for {TurnsUntilDecast} turns!\n";
            opponent.Stun();
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            listOfTurns.Add(turnCounter + numbersOfTurnsImmune, (c1, c2) => SecondDecast(c1, c2));
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            string opponentName = opponent.GetName();
            opponent.Unstun();
            opponent.SetStunResistant(true);
            string toStr = $"{opponentName} is no longer stunned!\n";
            toStr += $"{opponentName} is now stun resistant!\n";
            return toStr;
        }

        public string SecondDecast(Character caster, Character opponent)
        {
            string opponentName = opponent.GetName();
            opponent.SetStunResistant(false);
            string toStr = $"{opponentName} is no longer stun resistant!\n";
            return toStr;
        }
    }
}