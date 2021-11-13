using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidSaveFileException: Exception
    {
        public InvalidSaveFileException() : base("Invalid Save File chosen\n")
        {
            
        }
    }
}