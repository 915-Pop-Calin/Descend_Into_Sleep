using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class Cyclope: SideEnemy
    {
        public Cyclope() : base("Cyclope", 15, -5, AllItems.NoWeapon, AllItems.NoArmour, 20)
        {
            Level = 2;
        }
    }
}