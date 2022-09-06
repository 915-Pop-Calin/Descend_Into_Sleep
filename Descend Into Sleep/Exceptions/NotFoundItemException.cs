using System;

namespace ConsoleApp12.Exceptions
{
    public class NotFoundItemException : Exception
    {
        public NotFoundItemException() : base("Item cannot be found in inventory\n")
        {
        }
    }
}