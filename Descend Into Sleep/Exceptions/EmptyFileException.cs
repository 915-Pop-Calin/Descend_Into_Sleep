using System;

namespace ConsoleApp12.Exceptions
{
    public class EmptyFileException: Exception
    {
        public EmptyFileException(string filename) : base($"{filename} is empty")
        {
            
        }
    }
}