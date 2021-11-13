using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class TentacledMenace: VoidSideEnemy
    {
        public TentacledMenace() : base("Tentacled Menace", 30, 10, new TankBuster(), new LastStand(), 30)
        {
            
        }
    }
}