using System.Net.Mail;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelOne;

namespace ConsoleApp12.Levels
{
    public class LevelOne: Level
    {
        public LevelOne(HumanPlayer humanPlayer) : base(1, humanPlayer)
        {
            MainEnemies.Enqueue(Tem.MainBoss);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(DogOfWisdom));
            SideEnemies.Add(typeof(DogOfRashness));
            SideEnemies.Add(typeof(DogOfWar));
            SideEnemies.Add(typeof(DogOfWrath));
        }
    }
}