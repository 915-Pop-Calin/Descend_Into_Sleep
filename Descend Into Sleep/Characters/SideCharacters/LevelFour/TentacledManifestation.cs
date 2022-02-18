using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class TentacledManifestation: VoidSideEnemy
    {
        public TentacledManifestation() : base("Tentacled Manifestation", 50, 50, AllItems.NoWeapon, 
            AllItems.NoArmour, 100, new List<string>{"beg for mercy", "pray", "purify"}, 0.7, 4)
        {
        }
    }
}