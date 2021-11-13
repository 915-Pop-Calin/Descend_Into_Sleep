using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelThree;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class VoidPossessedAmalgamation: VoidSideEnemy
    {
        public VoidPossessedAmalgamation() : base("Void Possessed Amalgamation", 15, 50, new NoWeapon(),
            new BootsOfDodge(), 50)
        {
            
        }
    }
}