using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelFive;
using ConsoleApp12.Characters.SideCharacters.LevelThree;

namespace ConsoleApp12.Levels
{
    public class LevelFive: Level
    {
        public LevelFive(HumanPlayer humanPlayer) : base(5, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(BurningCitizen), 4}, {typeof(ExtinguishedFlame), 4}, {typeof(SonOfTheSun), 4}, {typeof(WorshipperOfTheSun), 4}
        }, new Queue<Character>(new[] {Icarus.MainBoss}), new Shop.Shop(humanPlayer, 4))
        {
        }

    }
}