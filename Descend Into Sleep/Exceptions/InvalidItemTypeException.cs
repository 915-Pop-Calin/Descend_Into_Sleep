using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidItemTypeException : Exception
    {
        public InvalidItemTypeException() : base("Item must be armour, weapon or potion")
        {
        }
    }
}