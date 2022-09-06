using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedFormatSaveFileException : Exception
    {
        public CorruptedFormatSaveFileException(int number, int lineNumber, Type type) : base(
            $"Save File #{number} is" +
            $" corrupted due to format type; message: line {lineNumber} must be {type}")
        {
        }
    }
}