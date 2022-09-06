using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class CorruptedProphet : PhysicalVoidSideEnemy
    {
        public CorruptedProphet() : base("Corrupted Prophet", 60, -20, BoilingBlood.BOILING_BLOOD, LastStand.LAST_STAND,
            100, new List<string> {"analyse", "understand", "explain yourself", "purify"}, 0.5, 6)
        {
        }
    }
}