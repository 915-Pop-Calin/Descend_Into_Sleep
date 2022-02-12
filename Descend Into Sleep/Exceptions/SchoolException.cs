using System;

namespace ConsoleApp12.Exceptions
{
    public class SchoolException: Exception
    {
        public SchoolException(string schoolName) : base($"{schoolName} ability cast by someone who is not of that school\n")
        {
            
        }

        public SchoolException()
        {
            
        }
    }
}