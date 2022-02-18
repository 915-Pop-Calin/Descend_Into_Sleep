using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class Cyclope: SideEnemy
    {
        public Cyclope() : base("Cyclope", 15, -5, AllItems.NoWeapon, AllItems.NoArmour, 20,
            new List<string> {"poke", "hit", "defend from"}, 0.85, 2)
        {
        }
    }
}