using System;
using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class VoidSideEnemy: SideEnemy
    {

        private readonly double MinimumPercentageReduced;
        private readonly double MaximumPercentageReduced;
        
        public VoidSideEnemy(string name, double attackValue, double defenseValue, IWeapon weapon, IArmour armour,
            double healthValue, List<string> actions, double chanceOfSuccessfulAct, int level) : 
            base(name, attackValue, defenseValue, weapon, armour, healthValue, actions, chanceOfSuccessfulAct, level)
        {
            MinimumPercentageReduced = 0.75;
            MaximumPercentageReduced = 1.25;
        }

        public override string Hit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var minimumSanityReducedReal = MinimumPercentageReduced * Attack;
            var maximumSanityReducedReal = MaximumPercentageReduced * Attack;
            var minimumSanityReducedInt = Convert.ToInt32(minimumSanityReducedReal);
            var maximumSanityReducedInt = Convert.ToInt32(maximumSanityReducedReal);
            var sanityReduced = RandomHelper.GenerateRandomInInterval(minimumSanityReducedInt, maximumSanityReducedInt);
            opponent.ReduceSanity(sanityReduced);
            var toStr = $"{opponent.GetName()}'s sanity was reduced by {sanityReduced}!\n";
            toStr += $"{opponent.GetName()} is left with {opponent.GetSanity()} sanity!\n";
            return toStr;
        }
        
        public string PhysicalHit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            return base.Hit(opponent, listOfTurns, turnCounter);
        }
    }
}