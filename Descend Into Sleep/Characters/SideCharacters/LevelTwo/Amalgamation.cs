using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class Amalgamation : SideEnemy
    {
        public Amalgamation() : base("Amalgamation", 7, 50, NoWeapon.NO_WEAPON, Cloth.CLOTH,
            50, new List<string> {"feed", "caress", "tame"}, 0.85, 2)
        {
        }
    }
}