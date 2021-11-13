using System;

namespace ConsoleApp12.Exceptions
{
    public class InexistentDecastException: Exception
    {
        public InexistentDecastException(string abilityName) : base("This ability has cast an inexistent decast: " + abilityName + "\n")
        {
            
        }

        public InexistentDecastException()
        {
            
        }
    }
}