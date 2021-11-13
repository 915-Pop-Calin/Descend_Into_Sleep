using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidInputTypeException: Exception
    {
        public InvalidInputTypeException(Type requested_type, Type got_type) : base("Invalid input type, " +
            requested_type +
            " was requested but got instead " + got_type + "\n")
        {
            
        }
    }
}