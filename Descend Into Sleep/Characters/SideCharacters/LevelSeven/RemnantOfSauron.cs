using System.Collections.Generic;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfSauron: PhysicalVoidSideEnemy
    {
        public RemnantOfSauron() : base("Remnant of Sauron", 75, 75, AllItems.NoWeapon, AllItems.NoArmour, 100,
            new List<string>{"praise", "worship", "pray", "stare into the eye"}, 0.5, 7)
        {
        }
    }
}