using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWisdom: SideEnemy
    {
        public DogOfWisdom() : base("Dog of Wisdom", 3, 3, new ToyKnife(), new Bandage(), 25)
        {
            Level = 1;
        }
    }
}