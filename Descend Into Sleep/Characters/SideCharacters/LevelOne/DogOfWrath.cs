using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWrath: SideEnemy
    {
        public DogOfWrath() : base("Dog Of Wrath", 6, 3, AllItems.Eclipse, AllItems.NoArmour, 
            15, new List<string> { "pet", "run at", "love"}, 0.99, 1)
        {
        }
    }
}