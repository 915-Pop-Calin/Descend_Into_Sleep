using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.PastSelfAbilities
{
    public class Condemn: Ability
    {
        private readonly double PercentageSanityLost;
        
        public Condemn(int StrengthLevel) : base("Condemn")
        {
            switch (StrengthLevel)
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

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var sanityValue = opponent.GetMaximumSanity();
            var sanityLost = PercentageSanityLost * sanityValue;
            opponent.ReduceSanity(sanityLost);
            return $"{caster.GetName()} condemns {opponent.GetName()} for everything it has done!\n{opponent.GetName()} loses" +
                        $" {PercentageSanityLost * 100}% of its maximum sanity!\n{opponent.GetName()} is left" +
                        $" with {Math.Round(opponent.GetSanity(), 2)} sanity!\n";
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}