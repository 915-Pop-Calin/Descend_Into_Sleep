using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedLengthException: Exception
    {
        public CorruptedLengthException(string filename, int length): base($"{filename} is corrupted due to" +
                                                                           $" its length being invalid; length of the file is {length}")
        {
            
        }
    }
}