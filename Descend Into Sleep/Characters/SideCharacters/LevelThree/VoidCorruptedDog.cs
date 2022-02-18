using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class VoidCorruptedDog: VoidSideEnemy
    {
        public VoidCorruptedDog() : base("Void Corrupted Dog", 10, 50, AllItems.Xalatath, 
            AllItems.BootsOfDodge, 50, new List<string>{"pet", "love", "purify"}, 0.75, 3)
        {
        }
    }
}