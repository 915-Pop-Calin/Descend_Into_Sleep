using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class PoisonousHare: SideEnemy
    {
        public PoisonousHare() : base("Poisonous Hare", 20, 5, new Eclipse(), new NoArmour(), 15)
        {
            
        }
    }
}