using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class FlameOfTheSun : FireSideEnemy
    {
        public FlameOfTheSun() : base("Flame Of The Sun", 30, 30, NoWeapon.NO_WEAPON, NoArmour.NO_ARMOUR,
            200, new List<string> {"deflect", "seize", "hold", "throw"}, 0.6, 5)
        {
        }
    }
}