using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class TentacledManifestation: VoidSideEnemy
    {
        public TentacledManifestation() : base("Tentacled Manifestation", 50, 50, AllItems.NoWeapon, AllItems.NoArmour, 100)
        {
            Level = 4;
        }
    }
}