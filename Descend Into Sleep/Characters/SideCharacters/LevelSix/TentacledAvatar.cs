using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class TentacledAvatar: PhysicalVoidSideEnemy
    {
        public TentacledAvatar() : base("Tentacled Avatar", 30, 40, AllItems.Xalatath, AllItems.NoArmour, 100)
        {
            Level = 6;
        }
    }
}