using System.Reflection;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters
{
    public class SideEnemy: Character
    {
        public SideEnemy(string name, double attack, double defense, Weapon weapon, Armour armour, double health) :
            base(name, attack, defense, weapon, armour, health)
        {
            ;
        }
        
    }
}