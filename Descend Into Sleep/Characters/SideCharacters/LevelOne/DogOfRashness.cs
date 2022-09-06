using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfRashness : SideEnemy
    {
        public DogOfRashness() : base("Dog Of Rashness", 7, 1, Eclipse.ECLIPSE, Bandage.BANDAGE,
            15, new List<string> {"pet", "run at", "love"}, 0.99, 1)
        {
        }
    }
}