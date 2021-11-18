using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.SideCharacters.LevelSix
{
    public class VoidInfusedOrc: PhysicalVoidSideEnemy
    {
        public VoidInfusedOrc() : base("Void Infused Orc", 40, 40, AllItems.GiantSlayer, AllItems.Bandage, 50)
        {
            Level = 6;
        }
    }
}