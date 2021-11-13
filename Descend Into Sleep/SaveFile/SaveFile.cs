using System;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.SaveFile
{
    public class SaveFile
    {
        private int Number;
        private string Name;
        private bool Corrupted;
        private bool Unopenable;
        
        public SaveFile(int number)
        {
            Number = number;
            Name = "SaveFiles" + "\\" + "savefile" + Number + ".json";
            Unopenable = false;
            Corrupted = false;
            try
            {
                HumanJSONSave.Load(Name);
                
            }
            catch (UnopenableSaveFileException)
            {
                Unopenable = true;
            }
            catch (CorruptedSaveFileException)
            {
                Corrupted = true;
            }
        }

        public Tuple<HumanPlayer, int, DateTime> LoadInfo()
        {
            if (Unopenable)
                throw new UnopenableSaveFileException(Number);
            if (Corrupted)
                throw new CorruptedSaveFileException(Number);
            var loadedInformation = HumanJSONSave.Load(Name);
            return loadedInformation;
        }
        
        public void SaveInfo(HumanPlayer humanPlayer, int GameLevel)
        {
            if (Unopenable)
                throw new UnopenableSaveFileException(Number);
            var humanJSON = new HumanJSONSave(humanPlayer, GameLevel);
            humanJSON.Save(Name);
        }

        public override string ToString()
        {
            var header = "Save File " + Number;
            if (Unopenable)
                return header + ": Unopenable. Check for its existence and for access to it\n";
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
    }
}