using System;

namespace ConsoleApp12.Exceptions
{
    public class NullItemException : Exception
    {
        public NullItemException() : base("Selected Item does not exist!")
        {
        }
    }
}