using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class Amalgamation: SideEnemy
    {
        public Amalgamation() : base("Amalgamation", 7, 50, AllItems.NoWeapon, AllItems.Cloth, 
            50, new List<string> {"feed", "caress", "tame"}, 0.85, 2)
        {
        }        
    }
}