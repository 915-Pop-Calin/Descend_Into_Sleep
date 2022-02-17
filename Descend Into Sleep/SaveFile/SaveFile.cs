using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.SaveFile
{
    public class SaveFile
    {
        private int Number;
        private string Name;
        private string CorruptionMessage;
        private HumanPlayer Player;
        private int GameLevel;
        private List<int> Enemies;
        private DateTime Time;

        private SaveFile(int number)
        {
            Number = number;
            Name = FileHelper.GetSaveFilePath(number);
            CheckCorruptionMessage();
        }

        private void CheckCorruptionMessage()
        {
            CorruptionMessage = null;
            Player = null;
            GameLevel = -1;
            Time = DateTime.UnixEpoch;
            try
            {
                var loadedInfo = HumanTxtSave.Load(Name);
                Player = loadedInfo.Item1;
                GameLevel = loadedInfo.Item2;
                Enemies = loadedInfo.Item3;
                Time = loadedInfo.Item4;
            }
            catch (CorruptedFormatSaveFileException corruptedFormatSaveFileException)
            {
                CorruptionMessage = corruptedFormatSaveFileException.Message;
            }
            catch (CorruptedInvalidValuesException corruptedInvalidValuesException)
            {
                CorruptionMessage = corruptedInvalidValuesException.Message;
            }
            catch (CorruptedLengthException corruptedLengthException)
            {
                CorruptionMessage = corruptedLengthException.Message;
            }
            catch (EmptyFileException emptyFileException)
            {
                CorruptionMessage = emptyFileException.Message;
            }
        }
        
        public Tuple<HumanPlayer, int, List<int>, DateTime> LoadInfo()
        {
            CheckCorruptionMessage();
            if (CorruptionMessage != null)
                throw new CorruptedSaveFileException(CorruptionMessage);
            return new Tuple<HumanPlayer, int, List<int>, DateTime>(Player, GameLevel, Enemies, Time);
        }
        
        public void SaveInfo(HumanPlayer humanPlayer, int gameLevel, List<int> enemies)
        {
            DateTime currentTime = DateTime.Now;
            Player = humanPlayer;
            GameLevel = gameLevel;
            Time = currentTime;
            CorruptionMessage = null;
            HumanTxtSave.Save(humanPlayer, gameLevel, enemies, currentTime, Name);
        }
        
        public override string ToString()
        {
            if (CorruptionMessage != null)
                return $"Save File {Number}:\nFile is corrupted:{CorruptionMessage}\n";
            var header = $"Save File {Number}";
            return $"{header}:\n{Player}Game Level: {GameLevel}\nSave Date: {Time}\n"; 
        }

        public bool IsEmpty()
        {
            return CorruptionMessage != null;
        }
        
        
        public static List<SaveFile> saveFiles = new List<SaveFile>()
        {
            new SaveFile(0), new SaveFile(1), new SaveFile(2), new SaveFile(3),
            new SaveFile(4), new SaveFile(5), new SaveFile(6), new SaveFile(7),
            new SaveFile(8), new SaveFile(9)
        };

    }
}