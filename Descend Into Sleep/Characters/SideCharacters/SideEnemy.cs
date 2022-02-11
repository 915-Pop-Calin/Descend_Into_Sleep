using System.Reflection;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public abstract class SideEnemy: Character
    {
        protected SideEnemy(string name, double attack, double defense, Weapon weapon, Armour armour, double health) :
            base(name, attack, defense, weapon, armour, health)
        {
            ;
        }
        
    }
}