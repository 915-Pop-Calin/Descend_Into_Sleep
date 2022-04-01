using System.Collections.Generic;
using System.Reflection;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class SideEnemy: Character
    {
        protected SideEnemy(string name, double attack, double defense, IWeapon weapon, IArmour armour, double health,
            List<string> actions, double chanceOfSuccessfulAct, int level) :
            base(name, attack, defense, weapon, armour, health, actions, chanceOfSuccessfulAct, level)
        {
            
        }
        
    }
}