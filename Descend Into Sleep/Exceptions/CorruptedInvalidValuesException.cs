using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedInvalidValuesException : Exception
    {
        public CorruptedInvalidValuesException(int number, int lineNumber) : base(
            $"Save File #{number} is corrupted due to" +
            $"invalid values; {lineNumber} has an invalid value")
        {
        }
    }
}