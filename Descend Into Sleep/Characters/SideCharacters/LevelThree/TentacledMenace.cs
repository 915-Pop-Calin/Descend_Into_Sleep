using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class TentacledMenace : VoidSideEnemy
    {
        public TentacledMenace() : base("Tentacled Menace", 20, -100, TankBuster.TANK_BUSTER,
            LastStand.LAST_STAND, 30, new List<string> {"straighten", "weave", "put a bow"},
            0.75, 3)
        {
        }
    }
}