using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class VoidCorruptedCyclope : VoidSideEnemy
    {
        public VoidCorruptedCyclope() : base("Void Corrupted Cyclope", 30, 5, NoWeapon.NO_WEAPON,
            NoArmour.NO_ARMOUR, 30, new List<string> {"poke", "hit", "purify"}, 0.75, 3)
        {
        }
    }
}