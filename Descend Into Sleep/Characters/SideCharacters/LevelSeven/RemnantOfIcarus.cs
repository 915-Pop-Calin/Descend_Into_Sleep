using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelSix;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfIcarus: FireSideEnemy
    {
        public RemnantOfIcarus() : base("Remnant of Icarus", 75, 50, AllItems.Dreams, AllItems.Bandage, 100)
        {
            Level = 7;
        }
    }
}