using System;
using System.Collections.Generic;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class FireSideEnemy : SideEnemy
    {
        private const double MINIMUM_DOT_PERCENTAGE = 0.25;
        private const double MAXIMUM_DOT_PERCENTAGE = 0.5;
        private const int NUMBER_OF_TURNS = 5;

        protected FireSideEnemy(string name, double attackValue, double defenseValue, IWeapon weapon,
            IArmour armour, double healthValue, List<string> actions, double chanceOfSuccessfulAct, int level) :
            base(name, attackValue, defenseValue, weapon, armour, healthValue, actions, chanceOfSuccessfulAct, level)
        {
        }

        public override string Hit(Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            var minimumDOTDealtReal = MINIMUM_DOT_PERCENTAGE * Attack;
            var maximumDOTDealtReal = MAXIMUM_DOT_PERCENTAGE * Attack;
            var minimumDOTDealtInt = Convert.ToInt32(minimumDOTDealtReal);
            var maximumDOTDealtInt = Convert.ToInt32(maximumDOTDealtReal);
            var DOTDealt = RandomHelper.GenerateRandomInInterval(minimumDOTDealtInt, maximumDOTDealtInt);
            var DOTEffect = new DotEffect(NUMBER_OF_TURNS, DOTDealt);
            opponent.AddDotEffect(DOTEffect);
            var toStr =
                $"{opponent.GetName()} will take {DOTDealt} damage per turn for the next {NUMBER_OF_TURNS} turns!\n";
            return toStr;
        }
    }
}