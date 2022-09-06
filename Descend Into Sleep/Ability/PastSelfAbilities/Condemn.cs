using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.PastSelfAbilities
{
    public class Condemn : Ability
    {
        private readonly double PercentageSanityLost;

        public Condemn(int strengthLevel) : base("Condemn")
        {
            switch (strengthLevel)
            {
                case 1:
                    PercentageSanityLost = 0.15;
                    break;
                case 2:
                    PercentageSanityLost = 0.25;
                    break;
                case 3:
                    PercentageSanityLost = 0.45;
                    break;
                default:
                    PercentageSanityLost = 0;
                    break;
            }
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            double sanityValue = opponent.GetMaximumSanity();
            double sanityLost = PercentageSanityLost * sanityValue;
            opponent.ReduceSanity(sanityLost);
            return
                $"{caster.GetName()} condemns {opponent.GetName()} for everything it has done!\n{opponent.GetName()} loses" +
                $" {PercentageSanityLost * 100}% of its maximum sanity!\n{opponent.GetName()} is left" +
                $" with {Math.Round(opponent.GetSanity(), 2)} sanity!\n";
        }

        protected override string Decast(Character caster, Character opponent)
        {
            throw new NonexistentDecastException(Name);
        }
    }
}