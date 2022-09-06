using System;

namespace ConsoleApp12.Exceptions
{
    public class MaximumLevelException : Exception
    {
        public MaximumLevelException() : base("You cannot level past level 34\n")
        {
        }
    }
}