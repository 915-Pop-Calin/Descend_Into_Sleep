using System;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelThree;

namespace ConsoleApp12.Levels
{
    public class LevelThree: Level
    {
        public LevelThree(HumanPlayer humanPlayer) : base(3, humanPlayer)
        {
            MainEnemies.Enqueue(YoggSaron.MainBoss);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(VoidCorruptedCyclope));
            SideEnemies.Add(typeof(VoidCorruptedDog));
            SideEnemies.Add(typeof(VoidPossessedAmalgamation));
            SideEnemies.Add(typeof(TentacledMenace));
        }
    }
}