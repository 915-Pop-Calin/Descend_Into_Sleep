using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class TentacledManifestation : VoidSideEnemy
    {
        public TentacledManifestation() : base("Tentacled Manifestation", 50, 50, NoWeapon.NO_WEAPON,
            NoArmour.NO_ARMOUR, 100, new List<string> {"beg for mercy", "pray", "purify"}, 0.7, 4)
        {
        }
    }
}