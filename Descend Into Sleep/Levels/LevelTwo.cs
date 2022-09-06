using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelTwo;

namespace ConsoleApp12.Levels
{
    public class LevelTwo : Level
    {
        public LevelTwo(HumanPlayer humanPlayer) : base(2, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(Amalgamation), 3}, {typeof(Cyclope), 3}, {typeof(PoisonousHare), 3}, {typeof(TortoiseOfWisdom), 4}
        }, new Queue<Character>(new[] {SpaghettiMonster.SPAGHETTI_MONSTER}))
        {
        }
    }
}