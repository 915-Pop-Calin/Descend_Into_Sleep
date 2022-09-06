using System.Collections.Generic;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class PhysicalVoidSideEnemy : VoidSideEnemy
    {
        protected PhysicalVoidSideEnemy(string name, double attack, double defense, IWeapon weapon, IArmour armour,
            double health, List<string> actions, double chanceOfSuccessfulAct, int level) :
            base(name, attack, defense, weapon, armour, health, actions, chanceOfSuccessfulAct, level)
        {
        }

        public override string Hit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var toStr = PhysicalHit(opponent, listOfTurns, turnCounter);
            toStr += base.Hit(opponent, listOfTurns, turnCounter);
            return toStr;
        }
    }
}