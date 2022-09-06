using System;
using System.Collections.Generic;
using ConsoleApp12.Ability.PastSelfAbilities;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Levels;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Game
{
    public class Game
    {
        private HumanPlayer Player;
        private List<Level> Levels;
        private readonly List<SaveFile.SaveFile> ListOfSaveFiles;
        private int Level;

        public Game()
        {
            Player = null;
            Levels = null;

            FileHelper.CheckSaveDirectory();
            for (int i = 0; i <= 9; i++)
                FileHelper.CheckSaveFile(i);
            ListOfSaveFiles = SaveFile.SaveFile.SAVE_FILES;
            Level = 1;
        }

        private void StartCharacter()
        {
            Console.WriteLine("The name you want to use from now on is:\n");
            var name = Console.ReadLine();

            var difficulties = new String[] {"easy", "medium", "hard", "impossible"};
            const string question = "Choose the difficulty you want to play on";

            var choice = Utils.keysWork.ConsoleHelper.MultipleChoice(20, question, difficulties);
            var difficulty = difficulties[choice];

            var humanPlayer = new HumanPlayer(name, difficulty, ToyKnife.TOY_KNIFE, Bandage.BANDAGE);
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
            const string question = "Do you want to load your save file?";
            var decision = Utils.keysWork.ConsoleHelper.MultipleChoice(20, question, "yes", "no");

            switch (decision)
            {
                case 0:
                    try
                    {
                        Player = new HumanPlayer("filler", "filler", ToyKnife.TOY_KNIFE, Bandage.BANDAGE);
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
                    catch (CorruptedSaveFileException corruptedSaveFileException)
                    {
                        Console.WriteLine(corruptedSaveFileException.Message);
                        StartCharacter();
                    }
                    catch (InvalidEnemiesNumberException invalidEnemiesNumberException)
                    {
                        Console.WriteLine(invalidEnemiesNumberException.Message);
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
                    throw new GameOverException();
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
            var readLine = Console.ReadLine();

            var isParseable = int.TryParse(readLine.Trim(), out var choice);
            if (!isParseable)
                throw new InvalidInputTypeException(typeof(int), readLine.GetType());

            if (choice < 0 || choice >= 10)
                throw new InvalidSaveFileException();
            var loadType = ListOfSaveFiles[choice].LoadInfo();
            // if (loadType.Item1 == null)
            //     throw new EmptySaveFileException();

            Player = loadType.Item1;
            Level = loadType.Item2;
            List<int> enemies = loadType.Item3;
            SetLevels();
            Levels[Level - 1].SetEnemies(enemies);
            Console.WriteLine("File successfully loaded!");
        }
    }
}