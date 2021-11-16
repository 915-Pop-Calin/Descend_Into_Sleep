using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfSauron: PhysicalVoidSideEnemy
    {
        public RemnantOfSauron() : base("Remnant of Sauron", 75, 75, new NoWeapon(), new NoArmour(), 100)
        {
            Level = 7;
        }
    }
}