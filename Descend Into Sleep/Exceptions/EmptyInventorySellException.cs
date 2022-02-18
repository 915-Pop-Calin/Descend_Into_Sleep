using System;

namespace ConsoleApp12.Exceptions
{
    public class EmptyInventorySellException: Exception
    {
        public EmptyInventorySellException() : base("You cannot sell items because your inventory is empty")
        {
            
        }
    }
}