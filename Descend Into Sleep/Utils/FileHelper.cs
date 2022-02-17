using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ConsoleApp12.Utils
{
    public class FileHelper
    {
        private static string GetPathToFile()
        {
            var operatingSystem = FindOperatingSystem();
            var username = Environment.UserName;
            switch (operatingSystem)
            {
                case "Windows":
                    return $"C:\\Users\\{username}\\AppData\\Local\\";
                case "Linux":
                    return $"/home/{username}/.local/share/";
                case "MacOS":
                    return $"/Users/{username}/Library/Application Support/";
                default:
                    Console.WriteLine($"{operatingSystem} is not currently supported");
                    throw new GameOverException();
            }
        }

        private static bool IsUnix()
        {
            var operatingSystem = FindOperatingSystem();
            if (operatingSystem == "Windows")
                return false;
            return true;
        }

        private static string FindOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "MacOS";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "Linux";
            return "None";
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
                var stream = File.Create(saveFilePath);
                stream.Close();
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
            var saveFilePath = $"{appDataPath}{gameName}{delimiter}savefile{saveFileNumber}.json";
            return saveFilePath;
        }
    }
}