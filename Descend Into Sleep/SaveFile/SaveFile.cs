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

        private SaveFile(int number)
        {
            Number = number;
            Name = FileHelper.GetSaveFilePath(number);
            CheckCorruptionMessage();
        }

        private void CheckCorruptionMessage()
        {
            CorruptionMessage = null;
            try
            {
                HumanTxtSave.Load(Name);
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
        
        public Tuple<HumanPlayer, int, DateTime> LoadInfo()
        {
            CheckCorruptionMessage();
            if (CorruptionMessage != null)
                throw new CorruptedSaveFileException(CorruptionMessage);
            var loadedInformation = HumanTxtSave.Load(Name);
            return loadedInformation;
        }
        
        public void SaveInfo(HumanPlayer humanPlayer, int GameLevel)
        {
            HumanTxtSave.Save(humanPlayer, GameLevel, Name);
        }

        public override string ToString()
        {
            CheckCorruptionMessage();
            var header = $"Save File {Number}";
            if (CorruptionMessage != null)
                return $"{header}: {CorruptionMessage}\n";
            var information = LoadInfo();
            var character = information.Item1;
            var gameLevel = information.Item2;
            var saveDate = information.Item3;
            return $"{header}:\n{character}Game Level: {gameLevel}\nSave Date: {saveDate}\n"; 
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