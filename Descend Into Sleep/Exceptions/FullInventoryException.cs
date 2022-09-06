using System;

namespace ConsoleApp12.Exceptions
{
    public class FullInventoryException : Exception
    {
        public FullInventoryException() : base("Action cannot be done because current inventory is full\n")
        {
        }
    }
}