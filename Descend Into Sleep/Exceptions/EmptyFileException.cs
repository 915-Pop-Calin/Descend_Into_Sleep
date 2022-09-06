using System;

namespace ConsoleApp12.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException(int number) : base($"Save File #{number} is empty")
        {
        }
    }
}