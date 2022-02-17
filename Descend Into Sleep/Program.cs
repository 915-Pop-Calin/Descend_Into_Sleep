
using System;
using System.IO;
using System.Threading;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;
using ConsoleApp12.SaveFile;
using ConsoleApp12.Utils;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex(false, "Descend Into Sleep 65536");
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
            catch (GameOverException)
            {
                Console.WriteLine("GAME OVER");
            }
            catch (PacifistEndingException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GOOD ENDING");
                Console.ResetColor();
            }
            catch (NeutralEndingException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("NEUTRAL ENDING");
                Console.ResetColor();
            }
            catch (GenocideEndingException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BAD ENDING");
                Console.ResetColor();
            }
            finally
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}