using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelTwo
{
    public class TortoiseOfWisdom: SideEnemy
    {
        public TortoiseOfWisdom() : base("Tortoise of Wisdom", 7, 1, new NoWeapon(), new TemArmour(), 35)
        {
            
        }
    }
}