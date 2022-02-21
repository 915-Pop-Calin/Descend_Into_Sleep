using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelSix;
using ConsoleApp12.Characters.SideCharacters.LevelThree;

namespace ConsoleApp12.Levels
{
    public class LevelSix: Level
    {
        public LevelSix(HumanPlayer humanPlayer) : base(6, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(CorruptedProphet), 4}, {typeof(PossessedGoblin), 4}, {typeof(TentacledAvatar), 4}, {typeof(VoidInfusedOrc), 4}
        }, new Queue<Character>(new[] {Sauron.MainBoss}))
        {
        }

    }
}