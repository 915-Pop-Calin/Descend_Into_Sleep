using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedInvalidValuesException: Exception
    {
        public CorruptedInvalidValuesException(string filename, int lineNumber) : base($"{filename} is corrupted due to" +
            $"invalid values; {lineNumber} has an invalid value")
        {
            
        }
    }
}