using System.Text.RegularExpressions;

namespace ConsoleApp12.Game
{
    using System;

namespace keysWork
{
    public class ConsoleHelper
    {
        public static int MultipleChoice(int spacingPerLine, params string[] options)
        {
            const int startX = 0;
            int currentSelection = 0;
    
            ConsoleKey key = ConsoleKey.A;
            Console.CursorVisible = false;
            int topConsole = Console.CursorTop;
            while (key != ConsoleKey.Enter){
                for (int i = 0; i < options.Length; i++)
                {
                        
                    Console.SetCursorPosition(startX + i  * spacingPerLine, topConsole + 1);
                    if(i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;
    
                    Console.Write(options[i]);
    
                    Console.ResetColor();
                }
                
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
                    int newPosition = currentCursorPosition - 1;
                    Console.SetCursorPosition(0, newPosition);
                }
            } 
    
            Console.CursorVisible = true;
            Console.WriteLine();
            
            return currentSelection;
        }
    }
}
}