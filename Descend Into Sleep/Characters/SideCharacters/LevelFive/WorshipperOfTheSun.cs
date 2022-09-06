using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class WorshipperOfTheSun : FireSideEnemy
    {
        public WorshipperOfTheSun() : base("Worshipper Of The Sun", 60, 60, NoWeapon.NO_WEAPON, NoArmour.NO_ARMOUR,
            150, new List<string> {"talk", "understand", "refrain", "enlighten"}, 0.6, 5)
        {
        }
    }
}