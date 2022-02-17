using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.PastSelfAbilities
{
    public class Judge: Ability
    {
        private readonly double PercentageHealthLost;

        public Judge(int StrengthLevel) : base("Judge")
        {
            switch (StrengthLevel)
            {
                case 1:
                    PercentageHealthLost = 0.25;
                    break;
                case 2:
                    PercentageHealthLost = 0.4;
                    break;
                case 3:
                    PercentageHealthLost = 0.65;
                    break;
                default:
                    PercentageHealthLost = 0;
                    break;
            }
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var healthPoints = opponent.GetMaximumHealthPoints();
            var damageTaken = PercentageHealthLost * healthPoints;
            opponent.TakeMitigatedDamage(damageTaken);
            return $"{caster.GetName()} judges {opponent.GetName()} for all its actions!\n{opponent.GetName()} takes" +
                   $" {PercentageHealthLost * 100}% of its maximum health!\n{opponent.GetName()} is left" +
                   $" with {Math.Round(opponent.GetHealthPoints(), 2)} health!\n";
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}