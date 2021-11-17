using System;
using System.IO;

namespace ConsoleApp12.SaveFile
{
    public class FileHelper
    {
        private static string GetPathToFile()
        {
            var operatingSystem = Environment.OSVersion.Platform;
            switch (operatingSystem)
            {
                case PlatformID.Win32NT:
                    var username = Environment.UserName;
                    return "C:\\Users\\" + username + "\\AppData\\Local\\";
                case PlatformID.Unix:
                    return "~/Library/Application Support/";
                case PlatformID.MacOSX:
                    return "~/.local/share/";
                default:
                    Console.WriteLine(operatingSystem + " is not currently supported");
                    Environment.Exit(0);
                    break;
            }
            return "";
        }

        private static bool IsUnix()
        {
            var operatingSystem = Environment.OSVersion.Platform;
            if (operatingSystem == PlatformID.Win32NT)
                return false;
            return true;
        }
        
        public static void CheckSaveDirectory()
        {
            
            var appDataPath = GetPathToFile();
            var gameName = "Descend Into Sleep";
            var gameFilePath = appDataPath + gameName;
            if (!Directory.Exists(gameFilePath))
            {
                Directory.CreateDirectory(gameFilePath);
            }
        }

        public static void CheckSaveFile(int saveFileNumber)
        {
            var saveFilePath = GetSaveFilePath(saveFileNumber);
            if (!File.Exists(saveFilePath))
            {
                File.Create(saveFilePath);
            }
        }

        public static string GetSaveFilePath(int saveFileNumber)
        {
            var appDataPath = GetPathToFile();
            var gameName = "Descend Into Sleep";
            string delimiter;
            if (IsUnix())
                delimiter = "/";
            else
                delimiter = "\\";
            var saveFilePath = appDataPath + gameName + delimiter + "savefile" + saveFileNumber + ".json";
            return saveFilePath;
        }
    }
}