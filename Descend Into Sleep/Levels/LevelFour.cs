using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelFour;
using ConsoleApp12.Characters.SideCharacters.LevelThree;

namespace ConsoleApp12.Levels
{
    public class LevelFour: Level
    {
        public LevelFour(HumanPlayer humanPlayer) : base(4, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(ParanoiaInducer), 5}, {typeof(TentacledManifestation), 5}, {typeof(UnknownPresence), 6}
        }, new Queue<Character>(new[] {Cthulhu.MainBoss}), new Shop.Shop(humanPlayer, 4))
        {
        }
    }
}