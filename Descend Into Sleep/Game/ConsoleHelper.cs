using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp12.Game
{
    using System;

namespace keysWork
{
    public class ConsoleHelper
    {
        public static int MultipleChoice(int spacingPerLine, string question, params string[] options)
        {
            int currentSelection = 0;
            var consoleBufferSize = Console.CursorTop;
            
            if (consoleBufferSize >= 3000)
                Console.Clear();
            Console.WriteLine(question);
            ConsoleKey key = ConsoleKey.A;
            Console.CursorVisible = false;
            int topConsole = Console.CursorTop;
            var tableLine = GetTableLine(spacingPerLine, options);
            
            while (key != ConsoleKey.Enter){
                Console.SetCursorPosition(0, topConsole);
                Console.WriteLine(tableLine);
                var currentPosition = 0;
                for (int i = 0; i < options.Length; i++)
                {
                    
                    Console.Write("|");

                    var emptySpaces = spacingPerLine + 2 - options[i].Length;
                    if (emptySpaces % 2 == 1)
                        emptySpaces += 1;
                    
                    currentPosition += emptySpaces / 2;
                    Console.SetCursorPosition(currentPosition, topConsole + 1);
                    if(i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;
    
                    Console.Write(options[i]);
    
                    Console.ResetColor();
                    currentPosition += emptySpaces / 2 + options[i].Length + 1;
                    Console.SetCursorPosition(currentPosition, topConsole + 1);
                }
                Console.Write("|\n");
                Console.WriteLine(tableLine);
                Console.WriteLine();
                
                key = Console.ReadKey(true).Key;
    
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    {
                        if (currentSelection >= 1)
                            currentSelection--;
                        else
                            currentSelection = options.Length - 1;
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        if (currentSelection < options.Length - 1)
                            currentSelection++;
                        else
                            currentSelection = 0;
                        break;
                    }
                }

                if (key != ConsoleKey.Enter)
                {
                    int currentCursorPosition = Console.GetCursorPosition().Top;
                    int newPosition = currentCursorPosition - 2;
                    Console.SetCursorPosition(0, newPosition);
                }
            }
            
            Console.CursorVisible = true;
            Console.WriteLine();
            
            return currentSelection;
        }

          public static string GetTableLine(int spacingPerLine, string[] options)
          {
              var tableLine = new StringBuilder();
              for (int i = 0; i < options.Length; i++)
              {
                  tableLine.Append('+');
                  int totalDashes;
                  if (options[i].Length % 2 == spacingPerLine % 2)
                      totalDashes = spacingPerLine + 2;
                  else
                      totalDashes = spacingPerLine + 3;
                  for (int j = 0; j < totalDashes; j++)
                      tableLine.Append('-');
              }

              tableLine.Append('+');
              return tableLine.ToString();
          }
    }
}
}