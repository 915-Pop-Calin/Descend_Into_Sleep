using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class PoisonousHare : SideEnemy
    {
        public PoisonousHare() : base("Poisonous Hare", 20, 5, Eclipse.ECLIPSE, NoArmour.NO_ARMOUR,
            15, new List<string> {"feed", "cure", "caress"}, 0.85, 2)
        {
        }
    }
}