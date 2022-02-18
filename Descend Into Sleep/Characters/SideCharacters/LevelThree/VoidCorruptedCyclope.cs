using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class VoidCorruptedCyclope: VoidSideEnemy
    {
        public VoidCorruptedCyclope() : base("Void Corrupted Cyclope", 30, 5, AllItems.NoWeapon, 
            AllItems.NoArmour, 30, new List<string>{"poke", "hit", "purify"}, 0.75, 3)
        {
        }
    }
}