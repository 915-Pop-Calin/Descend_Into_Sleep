using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class TortoiseOfWisdom : SideEnemy
    {
        public TortoiseOfWisdom() : base("Tortoise of Wisdom", 7, 1, NoWeapon.NO_WEAPON, TemArmour.TEM_ARMOUR,
            35, new List<string> {"listen", "talk to", "hug"}, 0.85, 2)
        {
        }
    }
}