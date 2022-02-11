
using System;
using System.IO;
using System.Threading;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Items;
using ConsoleApp12.SaveFile;
using ConsoleApp12.Utils;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex(false, "Descend Into Sleep 65537");
            if (!mutex.WaitOne(0, false))
            {
                    Console.WriteLine("Descend Into Sleep is already running!");
                    mutex.Close();
                    return;
            }

            try
            {
                var game = new Game.Game();
                game.StartGame();
            }
            catch (ExitGameException)
            {
                Console.WriteLine("GAME OVER");
            }
            finally
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}