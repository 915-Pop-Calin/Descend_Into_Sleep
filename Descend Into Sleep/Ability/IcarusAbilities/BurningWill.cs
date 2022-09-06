using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.IcarusAbilities
{
    public class BurningWill : Ability
    {
        private const double DAMAGE_PER_TURN = 7;
        private const int NUMBER_OF_TURNS = 3;

        public BurningWill() : base("Burning Will")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            DotEffect firstDOTEffect = new DotEffect(NUMBER_OF_TURNS, DAMAGE_PER_TURN);
            DotEffect secondDOTEffect = new DotEffect(NUMBER_OF_TURNS, DAMAGE_PER_TURN);
            DotEffect thirdDOTEffect = new DotEffect(NUMBER_OF_TURNS, DAMAGE_PER_TURN);
            opponent.AddDotEffect(firstDOTEffect);
            opponent.AddDotEffect(secondDOTEffect);
            opponent.AddDotEffect(thirdDOTEffect);
            string toStr = $"{caster.GetName()} sets everything ablaze!\n";
            toStr +=
                $"{opponent.GetName()} will take {DAMAGE_PER_TURN} every turn for {NUMBER_OF_TURNS} turns THRICE!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}