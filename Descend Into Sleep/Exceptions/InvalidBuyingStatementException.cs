using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidBuyingStatementException: Exception
    {
        public InvalidBuyingStatementException(string statement) : base($"{statement} is not a valid buying statement")
        {
            
        }
    }
}