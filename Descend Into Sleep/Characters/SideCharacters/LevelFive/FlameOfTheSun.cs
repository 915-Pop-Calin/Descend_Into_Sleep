using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class FlameOfTheSun: FireSideEnemy
    {
        public FlameOfTheSun() : base("Flame Of The Sun", 30, 30, AllItems.NoWeapon, AllItems.NoArmour,
            200, new List<string>{"deflect", "seize", "hold", "throw"}, 0.6, 5)
        {
        }
    }
}