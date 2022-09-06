using System;

namespace ConsoleApp12.Exceptions
{
    public class EmptySaveFileException : Exception
    {
        public EmptySaveFileException() : base("The Save File you have chosen is empty\n")
        {
        }
    }
}