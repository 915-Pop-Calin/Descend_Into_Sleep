using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedSaveFileException: Exception
    {
        public CorruptedSaveFileException(string message) : base(message)
        {
            
        }
    }
}