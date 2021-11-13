using System;
using System.ComponentModel;
using System.IO;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelThree;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Potions;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game.Game();
            game.StartGame();
        }
    }
}