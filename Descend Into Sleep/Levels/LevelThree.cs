using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelThree;

namespace ConsoleApp12.Levels
{
    public class LevelThree : Level
    {
        public LevelThree(HumanPlayer humanPlayer) : base(3, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(VoidCorruptedCyclope), 4}, {typeof(VoidCorruptedDog), 4}, {typeof(VoidPossessedAmalgamation), 4},
            {typeof(TentacledMenace), 4}
        }, new Queue<Character>(new[] {YoggSaron.YOGG_SARON}))
        {
        }
    }
}