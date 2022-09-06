using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidItemDropException : Exception
    {
        public InvalidItemDropException(string itemType) : base($"You have no {itemType} to drop\n")
        {
        }

        public InvalidItemDropException()
        {
        }
    }
}