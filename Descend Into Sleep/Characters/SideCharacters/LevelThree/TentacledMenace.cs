using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class TentacledMenace: VoidSideEnemy
    {
        public TentacledMenace() : base("Tentacled Menace", 20, 10, AllItems.TankBuster, AllItems.LastStand, 30)
        {
            Level = 3;
        }
    }
}