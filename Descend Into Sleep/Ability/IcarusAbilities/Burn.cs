using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.IcarusAbilities
{
    public class Burn : Ability
    {
        private const int NUMBER_OF_TURNS = 5;
        private const double TURN_SCALING = 1;

        public Burn() : base("Burn")
        {
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double damagePerTurn = NUMBER_OF_TURNS * TURN_SCALING;
            DotEffect dotEffect = new DotEffect(NUMBER_OF_TURNS, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            string toStr = $"{caster.GetName()} burns everything around it!\n";
            toStr +=
                $"{opponent.GetName()} will take {Math.Round(damagePerTurn, 2)} damage every turn for {NUMBER_OF_TURNS} turns!\n";
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}