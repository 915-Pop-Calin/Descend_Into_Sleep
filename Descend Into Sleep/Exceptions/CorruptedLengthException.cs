using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedLengthException : Exception
    {
        public CorruptedLengthException(int number, int length) : base($"Save File #{number} is corrupted due to" +
                                                                       $" its length being invalid; length of the file is {length}")
        {
        }
    }
}