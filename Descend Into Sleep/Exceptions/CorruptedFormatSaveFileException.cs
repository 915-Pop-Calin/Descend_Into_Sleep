using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedFormatSaveFileException: Exception
    {
        public CorruptedFormatSaveFileException(string filename, int lineNumber, Type type) : base($"{filename} is" +
            $" corrupted due to format type; message: line {lineNumber} must be {type}")
        {
            
        }        
    }
}