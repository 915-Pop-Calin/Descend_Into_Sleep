using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.IcarusAbilities
{
    public class BurningWill: Ability
    {
        private readonly double DamagePerTurn;
        private readonly int NumberOfTurns;

        public BurningWill() : base("Burning Will")
        {
            DamagePerTurn = 7;
            NumberOfTurns = 3;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var firstDOTEffect = new DotEffect(NumberOfTurns, DamagePerTurn);
            var secondDOTEffect = new DotEffect(NumberOfTurns, DamagePerTurn);
            var thirdDOTEffect = new DotEffect(NumberOfTurns, DamagePerTurn);
            opponent.AddDotEffect(firstDOTEffect);
            opponent.AddDotEffect(secondDOTEffect);
            opponent.AddDotEffect(thirdDOTEffect);
            var toStr = $"{caster.GetName()} has cast {Name}!\n";
            toStr += $"{opponent.GetName()} will take {DamagePerTurn} every turn for {NumberOfTurns} turns THRICE!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}