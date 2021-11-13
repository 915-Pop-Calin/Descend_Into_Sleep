using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.HumanAbilities.FireAbilities
{
    public class BurningLava: Ability
    {

        private readonly int NumberOfTurns;
        private readonly double MissingHealthPercentageTransformed;
        
        public BurningLava() : base("Burning Lava")
        {
            ManaCost = 20;
            Description = "Your opponent takes damage over some turns proportional to your missing health.\n";
            NumberOfTurns = 5;
            MissingHealthPercentageTransformed = 0.5;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var toStr = GetCastingString(caster);
            var missingHealth = caster.GetMaximumHealthPoints() - caster.GetHealthPoints();
            var totalDamageDealt = Level * missingHealth * MissingHealthPercentageTransformed;
            var damagePerTurn = totalDamageDealt / NumberOfTurns;
            var dotEffect = new DotEffect(NumberOfTurns, damagePerTurn);
            opponent.AddDotEffect(dotEffect);
            toStr += opponentName + " will take " + damagePerTurn + " damage per turn for" + 
                     NumberOfTurns + " turns!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }
    }
}