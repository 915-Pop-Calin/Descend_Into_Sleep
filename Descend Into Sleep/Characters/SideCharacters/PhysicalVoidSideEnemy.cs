using System;
using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class PhysicalVoidSideEnemy: VoidSideEnemy
    {
        public PhysicalVoidSideEnemy(string name, double attack, double defense, Weapon weapon, Armour armour,
            double health) :
            base(name, attack, defense, weapon, armour, health)
        {
        }

        public override string Hit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = PhysicalHit(opponent, listOfTurns, turnCounter);
            toStr += base.Hit(opponent, listOfTurns, turnCounter);
            return toStr;
        }
        
    }
}