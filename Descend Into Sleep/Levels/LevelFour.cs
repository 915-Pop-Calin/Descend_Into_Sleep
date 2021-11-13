using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelFour;

namespace ConsoleApp12.Levels
{
    public class LevelFour: Level
    {
        public LevelFour(HumanPlayer humanPlayer) : base(4, humanPlayer)
        {
            MainEnemy = typeof(Cthulhu);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(ParanoiaInducer));
            SideEnemies.Add(typeof(TentacledManifestation));
            SideEnemies.Add(typeof(UnknownPresence));
        }
    }
}