using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWrath : SideEnemy
    {
        public DogOfWrath() : base("Dog Of Wrath", 6, 3, Eclipse.ECLIPSE, NoArmour.NO_ARMOUR,
            15, new List<string> {"pet", "run at", "love"}, 0.99, 1)
        {
        }
    }
}