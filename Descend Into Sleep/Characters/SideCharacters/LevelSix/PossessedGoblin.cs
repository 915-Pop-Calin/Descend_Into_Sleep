using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class PossessedGoblin : PhysicalVoidSideEnemy
    {
        public PossessedGoblin() : base("Possessed Goblin", 10, 100, NoWeapon.NO_WEAPON, NinjaYoroi.NINJA_YOROI, 300,
            new List<string> {"defend", "understand", "free", "purify"}, 0.5, 6)
        {
        }
    }
}