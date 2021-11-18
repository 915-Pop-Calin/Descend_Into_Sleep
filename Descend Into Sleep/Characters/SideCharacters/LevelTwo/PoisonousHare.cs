using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class PoisonousHare: SideEnemy
    {
        public PoisonousHare() : base("Poisonous Hare", 20, 5, AllItems.Eclipse, AllItems.NoArmour, 15)
        {
            Level = 2;
        }
    }
}