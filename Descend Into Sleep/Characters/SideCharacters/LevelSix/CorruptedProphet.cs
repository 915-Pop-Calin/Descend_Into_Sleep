using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class CorruptedProphet: PhysicalVoidSideEnemy
    {
        public CorruptedProphet() : base("Corrupted Prophet", 60, -20, AllItems.BoilingBlood, AllItems.LastStand, 100)
        {
            Level = 6;
        }
    }
}