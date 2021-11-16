using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class CorruptedProphet: PhysicalVoidSideEnemy
    {
        public CorruptedProphet() : base("Corrupted Prophet", 60, -20, new BoilingBlood(), new LastStand(), 100)
        {
            Level = 6;
        }
    }
}