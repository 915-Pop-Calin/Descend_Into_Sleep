using System;
using System.Collections.Generic;
using System.Net.Mail;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelOne;

namespace ConsoleApp12.Levels
{
    public class LevelOne: Level
    {
        public LevelOne(HumanPlayer humanPlayer) : base(1, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(DogOfRashness), 3}, {typeof(DogOfWar), 2}, {typeof(DogOfWrath), 3}, {typeof(DogOfWisdom), 2}
        }, new Queue<Character>(new[] {Tem.MainBoss}), new Shop.Shop(humanPlayer, 1))
        {

        }
    }
}