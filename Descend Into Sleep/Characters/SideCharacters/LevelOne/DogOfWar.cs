using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWar: SideEnemy
    {
        public DogOfWar() : base("Dog Of War", 7, 1, AllItems.NoWeapon, AllItems.Cloth, 25,
            new List<string> { "pet", "run at", "love"}, 0.99, 1)
        {
        }        
    }
}