using System;

namespace ConsoleApp12.Exceptions
{
    public class InsufficientGoldException: Exception
    {
        public InsufficientGoldException(string itemName): base($"You do not have sufficient gold to buy {itemName}\n")
        {
        }

        public InsufficientGoldException()
        {
            
        }
    }
}