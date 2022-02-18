using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class PoisonousHare: SideEnemy
    {
        public PoisonousHare() : base("Poisonous Hare", 20, 5, AllItems.Eclipse, AllItems.NoArmour, 
            15, new List<string> {"feed", "cure", "caress"}, 0.85, 2)
        {
        }
    }
}