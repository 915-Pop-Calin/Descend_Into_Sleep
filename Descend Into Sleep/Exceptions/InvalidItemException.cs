using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidItemException : Exception
    {
        public InvalidItemException() : base("The chosen item is invalid\n")
        {
        }
    }
}