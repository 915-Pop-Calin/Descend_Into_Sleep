using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class UnknownPresence: VoidSideEnemy
    {
        public UnknownPresence() : base("Unknown Presence", 30, 30, new GiantSlayer(), new Scales(), 60)
        {
            Level = 4;
        }
    }
}