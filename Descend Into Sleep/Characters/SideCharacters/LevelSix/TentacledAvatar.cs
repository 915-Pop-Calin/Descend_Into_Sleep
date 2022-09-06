using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class TentacledAvatar : PhysicalVoidSideEnemy
    {
        public TentacledAvatar() : base("Tentacled Avatar", 30, 40, Xalatath.XALATATH, NoArmour.NO_ARMOUR, 100,
            new List<string> {"cut arm off", "enlighten", "worship", "beg for mercy"}, 0.5, 6)
        {
        }
    }
}