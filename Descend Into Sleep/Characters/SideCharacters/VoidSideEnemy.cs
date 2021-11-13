using System;
using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public class VoidSideEnemy: SideEnemy
    {

        private readonly double MinimumPercentageReduced;
        private readonly double MaximumPercentageReduced;
        
        public VoidSideEnemy(string name, double attackValue, double defenseValue, Weapon weapon, Armour armour,
            double healthValue) : base(name, attackValue, defenseValue, weapon, armour, healthValue)
        {
            MinimumPercentageReduced = 0.75;
            MaximumPercentageReduced = 1.25;
        }

        public override string Hit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var opponentName = opponent.GetName();
            var minimumSanityReducedReal = MinimumPercentageReduced * Attack;
            var maximumSanityReducedReal = MaximumPercentageReduced * Attack;
            var minimumSanityReducedInt = Convert.ToInt32(minimumSanityReducedReal);
            var maximumSanityReducedInt = Convert.ToInt32(maximumSanityReducedReal);
            var randomObject = new Random();
            var sanityReduced = randomObject.Next(minimumSanityReducedInt, maximumSanityReducedInt);
            opponent.ReduceSanity(sanityReduced);
            var toStr = opponentName + "'s sanity was reduced by " + sanityReduced + "!\n";
            toStr += opponentName + " is left with " + opponent.GetSanity() + " sanity!\n";
            return toStr;
        }
        
        public string PhysicalHit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            return base.Hit(opponent, listOfTurns, turnCounter);
        }
    }
}