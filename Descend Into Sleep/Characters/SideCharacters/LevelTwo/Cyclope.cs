using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class Cyclope : SideEnemy
    {
        public Cyclope() : base("Cyclope", 15, -5, NoWeapon.NO_WEAPON, NoArmour.NO_ARMOUR, 20,
            new List<string> {"poke", "hit", "defend from"}, 0.85, 2)
        {
        }
    }
}