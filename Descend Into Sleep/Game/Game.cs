using System;
using System.Collections.Generic;
using System.Net.Mail;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Levels;

namespace ConsoleApp12.Game
{
    public class Game
    {
        protected HumanPlayer Player;
        protected bool Dead;
        protected Character Enemy;
        protected Combat _Combat;
        protected bool InCombat;
        protected bool Exit;
        protected List<Type> Levels;
        protected List<SaveFile.SaveFile> ListOfSaveFiles;
        protected int Level;

        public Game()
        {
            Player = null;
            Dead = false;
            Enemy = null;
            _Combat = null;
            InCombat = true;
            Exit = false;
            Levels = new List<Type>();
            Levels.Add(new LevelOne(Player).GetType());
            Levels.Add(new LevelTwo(Player).GetType());
            Levels.Add(new LevelThree(Player).GetType());
            Levels.Add(new LevelFour(Player).GetType());
            Levels.Add(new LevelFive(Player).GetType());
            Levels.Add(new LevelSix(Player).GetType());
            Levels.Add(new LevelSeven(Player).GetType());

            ListOfSaveFiles = new List<SaveFile.SaveFile>();
            for (int i = 0; i <= 9; i++)
                ListOfSaveFiles.Add(new SaveFile.SaveFile(i));
 
            
            Level = 1;
        }

        private void StartCharacter()
        {
            Console.WriteLine("The name you want to use from now on is:\n");
            var name = Console.ReadLine();
            Console.WriteLine("Choose the difficulty you want to play on: easy, medium, hard, impossible:\n");
            var difficulty = Console.ReadLine();
            var humanPlayer = new HumanPlayer(name, difficulty, new ToyKnife(), new Bandage());
            Player = humanPlayer;
            
        }

        public void StartGame()
        {
            Console.WriteLine("Do you want to load your save file? Y/N\n");
            var decision = Console.ReadLine();
            switch (decision)
            {
                case "Y":
                    try
                    {
                        Player = new HumanPlayer("filler", "filler", new ToyKnife(), new Bandage());
                        Load();
                    }
                    catch (InvalidSaveFileException invalidSaveFileException)
                    {
                        Console.WriteLine(invalidSaveFileException.Message);
                        StartCharacter();
                    }
                    catch (EmptySaveFileException emptySaveFileException)
                    {
                        Console.WriteLine(emptySaveFileException.Message);
                        StartCharacter();
                    }
                    break;
                default:
                    StartCharacter();
                    break;
            }
            PlayGame();
        }

        private int PlayLevel()
        {
            var currentLevelType = Levels[Level - 1];
            var currentLevel = (Level) Activator.CreateInstance(currentLevelType, Player);
            return currentLevel.PlayOut();
        }

        private void PlayGame()
        {
            var playedOut = 0;
            while (playedOut != -1 && Level != 8)
            {
                playedOut = PlayLevel();
                Level++;
                if (Level == 7 && Player.IsCheater())
                {
                    Console.WriteLine("Last Level cannot be played because you cheated!\n");
                    Exit = true;
                    break;
                }
            }
        }

        private void PrintAllSaveFiles()
        {
            foreach (var saveFile in ListOfSaveFiles)
            {
                Console.WriteLine(saveFile);
            }
        }

        private void Load()
        {
            PrintAllSaveFiles();
            Console.WriteLine("Choose the number of the Save File to load:");
            var readLine = Console.ReadLine();;
            int choice;
            try
            {
                choice = Convert.ToInt32(readLine);
            }
            catch (FormatException)
            {
                throw new InvalidInputTypeException(typeof(int), readLine.GetType());
            }
            
            if (choice < 0 || choice >= 10)
                throw new InvalidSaveFileException();
            var loadType = ListOfSaveFiles[choice].LoadInfo();
            if (loadType.Item1 == null)
                throw new EmptySaveFileException();
            Player = loadType.Item1;
            Level = loadType.Item2;
        }
        
        
    }
}