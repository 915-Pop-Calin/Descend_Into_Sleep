using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidInputTypeException: Exception
    {
        public InvalidInputTypeException(Type requestedType, Type gotType) : base($"Invalid input type, {requestedType}" +
            $" was requested but got instead {gotType}\n")
        {
            
        }
    }
}