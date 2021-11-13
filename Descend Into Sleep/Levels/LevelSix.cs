using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelSix;

namespace ConsoleApp12.Levels
{
    public class LevelSix: Level
    {
        public LevelSix(HumanPlayer humanPlayer) : base(6, humanPlayer)
        {
            MainEnemy = typeof(Sauron);
            Shop = new Shop.Shop(Player, Number);
            SideEnemies.Add(typeof(CorruptedProphet));
            SideEnemies.Add(typeof(PossessedGoblin));
            SideEnemies.Add(typeof(TentacledAvatar));
            SideEnemies.Add(typeof(VoidInfusedOrc));
        }        
    }
}