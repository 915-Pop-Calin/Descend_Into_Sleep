using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelFive;

namespace ConsoleApp12.Levels
{
    public class LevelFive: Level
    {
        public LevelFive(HumanPlayer humanPlayer) : base(5, humanPlayer)
        {
            MainEnemies.Enqueue(Icarus.MainBoss);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(BurningCitizen));
            SideEnemies.Add(typeof(ExtinguishedFlame));
            SideEnemies.Add(typeof(SonOfTheSun));
            SideEnemies.Add(typeof(WorshipperOfTheSun));
        }        
    }
}