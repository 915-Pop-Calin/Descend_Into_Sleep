using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFive;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class ExtinguishedFlame: FireSideEnemy
    {
        public ExtinguishedFlame() : base("Extinguished Flame", 15, 50, AllItems.InfinityEdge, AllItems.NinjaYoroi, 100)
        {
            Level = 5;
        }
    }
}