using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidBuyException: Exception
    {
        public InvalidBuyException(string itemName) : base($"{itemName} does not exist!")
        {
            
        }
    }
}