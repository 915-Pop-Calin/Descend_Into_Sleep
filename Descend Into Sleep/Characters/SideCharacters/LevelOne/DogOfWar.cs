using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWar: SideEnemy
    {
        public DogOfWar() : base("Dog Of War", 7, 1, new NoWeapon(), new Cloth(), 25)
        {
            Level = 1;
        }        
    }
}