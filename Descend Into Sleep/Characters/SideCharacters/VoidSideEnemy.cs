using System;
using System.Collections.Generic;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class VoidSideEnemy : SideEnemy
    {
        private const double MINIMUM_PERCENTAGE_REDUCED = 0.75;
        private const double MAXIMUM_PERCENTAGE_REDUCED = 1.25;

        protected VoidSideEnemy(string name, double attackValue, double defenseValue, IWeapon weapon, IArmour armour,
            double healthValue, List<string> actions, double chanceOfSuccessfulAct, int level) :
            base(name, attackValue, defenseValue, weapon, armour, healthValue, actions, chanceOfSuccessfulAct, level)
        {
        }

        public override string Hit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var minimumSanityReducedReal = MINIMUM_PERCENTAGE_REDUCED * Attack;
            var maximumSanityReducedReal = MAXIMUM_PERCENTAGE_REDUCED * Attack;
            var minimumSanityReducedInt = Convert.ToInt32(minimumSanityReducedReal);
            var maximumSanityReducedInt = Convert.ToInt32(maximumSanityReducedReal);
            var sanityReduced = RandomHelper.GenerateRandomInInterval(minimumSanityReducedInt, maximumSanityReducedInt);
            opponent.ReduceSanity(sanityReduced);
            var toStr = $"{opponent.GetName()}'s sanity was reduced by {sanityReduced}!\n";
            toStr += $"{opponent.GetName()} is left with {opponent.GetSanity()} sanity!\n";
            return toStr;
        }

        public string PhysicalHit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            return base.Hit(opponent, listOfTurns, turnCounter);
        }
    }
}