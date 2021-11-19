using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game.keysWork;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Levels;
using ConsoleApp12.SaveFile;

namespace ConsoleApp12.Game
{
    public class Game
    {
        protected HumanPlayer Player;
        protected List<Level> Levels;
        protected List<SaveFile.SaveFile> ListOfSaveFiles;
        protected int Level;

        public Game()
        {
            Player = null;
            Levels = null;

            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            ListOfSaveFiles = SaveFile.SaveFile.saveFiles;
            Level = 1;
        }

        private void StartCharacter()
        {
            Console.WriteLine("The name you want to use from now on is:\n");
            var name = Console.ReadLine();
            
            var difficulties = new String[] {"easy", "medium", "hard", "impossible"};
            Console.WriteLine("Choose the difficulty you want to play on");
            
            var choice = ConsoleHelper.MultipleChoice(20,difficulties);
            var difficulty = difficulties[choice];
            
            var humanPlayer = new HumanPlayer(name, difficulty, AllItems.ToyKnife, AllItems.Bandage);
            
            Player = humanPlayer;
            SetLevels();
        }

        private void SetLevels()
        {
            Levels = new List<Level>()
            {
                new LevelOne(Player), new LevelTwo(Player), new LevelThree(Player), new LevelFour(Player),
                new LevelFive(Player), new LevelSix(Player), new LevelSeven(Player)
            };
        }
        public void StartGame()
        {
            Console.WriteLine("Do you want to load your save file?");
            var decision = ConsoleHelper.MultipleChoice(20,"yes", "no");

            switch (decision)
            {
                case 0:
                    try
                    {
                        Player = new HumanPlayer("filler", "filler", AllItems.ToyKnife, AllItems.Bandage);
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
                    catch (InvalidInputTypeException invalidInputTypeException)
                    {
                        Console.WriteLine(invalidInputTypeException.Message);
                        StartCharacter();
                    }
                    break;
                case 1:
                    StartCharacter();
                    break;
            }
            PlayGame();
        }

        private void PlayLevel()
        {
            var currentLevel = Levels[Level - 1];
            currentLevel.PlayOut();
        }

        private void PlayGame()
        {
            while (Level != 8)
            {
                PlayLevel();
                Level++;
                if (Level == 7 && Player.IsCheater())
                {
                    Console.WriteLine("Last Level cannot be played because you cheated!\n");
                    Environment.Exit(0);
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
            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            
            PrintAllSaveFiles();
            Console.WriteLine("Choose the number of the Save File to load:");
            var readLine = Console.ReadLine();;

            int choice;
            var isParseable = int.TryParse(readLine.Trim(), out choice);
            if (!isParseable)
                throw new InvalidInputTypeException(typeof(int), readLine.GetType());
            
            if (choice < 0 || choice >= 10)
                throw new InvalidSaveFileException();
            var loadType = ListOfSaveFiles[choice].LoadInfo();
            if (loadType.Item1 == null)
                throw new EmptySaveFileException();
            
            Player = loadType.Item1;
            Level = loadType.Item2;
            SetLevels();
        }
        
        
    }
}