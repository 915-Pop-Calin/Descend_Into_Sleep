using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfSauron : PhysicalVoidSideEnemy
    {
        public RemnantOfSauron() : base("Remnant of Sauron", 75, 75, NoWeapon.NO_WEAPON, NoArmour.NO_ARMOUR, 100,
            new List<string> {"praise", "worship", "pray", "stare into the eye"}, 0.5, 7)
        {
        }
    }
}