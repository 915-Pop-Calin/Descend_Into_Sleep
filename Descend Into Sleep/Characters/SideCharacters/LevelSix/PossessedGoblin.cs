using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class PossessedGoblin: PhysicalVoidSideEnemy
    {
        public PossessedGoblin() : base("Possessed Goblin", 10, 100, new NoWeapon(), new NinjaYoroi(), 300)
        {
            Level = 6;
        }
    }
}