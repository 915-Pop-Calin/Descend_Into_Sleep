using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.SaveFile
{
    public class SaveFile
    {
        private int Number;
        private string Name;
        private bool Corrupted;

        private SaveFile(int number)
        {
            Number = number;
            
            Name = FileHelper.GetSaveFilePath(number);
            Corrupted = false;
            try
            {
                HumanJSONSave.Load(Name);
                
            }
            catch (CorruptedSaveFileException)
            {
                Corrupted = true;
            }
        }

        public Tuple<HumanPlayer, int, DateTime> LoadInfo()
        {
            if (Corrupted)
                throw new CorruptedSaveFileException(Number);
            var loadedInformation = HumanJSONSave.Load(Name);
            return loadedInformation;
        }
        
        public void SaveInfo(HumanPlayer humanPlayer, int GameLevel)
        {
            var humanJSON = new HumanJSONSave(humanPlayer, GameLevel);
            humanJSON.Save(Name);
        }

        public override string ToString()
        {
            var header = "Save File " + Number;
            if (Corrupted)
                return header + ": Corrupted. Please erase its contents\n";
            var information = LoadInfo();
            var character = information.Item1;
            var gameLevel = information.Item2;
            var saveDate = information.Item3;
            if (character == null)
                return header + ": Empty Save File\n";

            return header + ":\n" + character + "Game Level: " + gameLevel +
                   "\nSave Date: " + saveDate + "\n"; 
        }

        public bool IsEmpty()
        {
            var information = LoadInfo();
            var character = information.Item1;
            return character == null;
        }
        
        
        public static List<SaveFile> saveFiles = new List<SaveFile>()
        {
            new SaveFile(0), new SaveFile(1), new SaveFile(2), new SaveFile(3),
            new SaveFile(4), new SaveFile(5), new SaveFile(6), new SaveFile(7),
            new SaveFile(8), new SaveFile(9)
        };

    }
}