using System;
using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public class FireSideEnemy: SideEnemy
    {
        private readonly double MinimumDOTPercentage;
        private readonly double MaximumDOTPercentage;
        private readonly int NumberOfTurns;

        public FireSideEnemy(string name, double attackValue, double defenseValue, Weapon weapon,
            Armour armour, double healthValue) : base(name, attackValue, defenseValue, weapon, armour, healthValue)
        {
            MinimumDOTPercentage = 0.25;
            MaximumDOTPercentage = 0.5;
            NumberOfTurns = 5;
        }

        public override string Hit(Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var minimumDOTDealtReal = MinimumDOTPercentage * Attack;
            var maximumDOTDealtReal = MaximumDOTPercentage * Attack;
            var minimumDOTDealtInt = Convert.ToInt32(minimumDOTDealtReal);
            var maximumDOTDealtInt = Convert.ToInt32(maximumDOTDealtReal);
            var randomObject = new Random();
            var DOTDealt = randomObject.Next(minimumDOTDealtInt, maximumDOTDealtInt);
            var DOTEffect = new DotEffect(NumberOfTurns, DOTDealt);
            opponent.AddDotEffect(DOTEffect);
            var toStr = opponent + " will take " + DOTDealt + " damage per turn for the next " + NumberOfTurns +
                        " turns!\n";
            return toStr;
        }

    }
}