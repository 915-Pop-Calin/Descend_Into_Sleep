using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWar : SideEnemy
    {
        public DogOfWar() : base("Dog Of War", 7, 1, NoWeapon.NO_WEAPON, Cloth.CLOTH, 25,
            new List<string> {"pet", "run at", "love"}, 0.99, 1)
        {
        }
    }
}