using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelTwo;

namespace ConsoleApp12.Levels
{
    public class LevelTwo: Level
    {
        public LevelTwo(HumanPlayer humanPlayer) : base(2, humanPlayer)
        {
            MainEnemies.Enqueue(SpaghettiMonster.MainBoss);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(Amalgamation));
            SideEnemies.Add(typeof(Cyclope));
            SideEnemies.Add(typeof(PoisonousHare));
            SideEnemies.Add(typeof(TortoiseOfWisdom));
        }
    }
}