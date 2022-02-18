using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class TentacledAvatar: PhysicalVoidSideEnemy
    {
        public TentacledAvatar() : base("Tentacled Avatar", 30, 40, AllItems.Xalatath, AllItems.NoArmour, 100,
            new List<string>{"cut arm off", "enlighten", "worship", "beg for mercy"}, 0.5, 6)
        {
        }
    }
}