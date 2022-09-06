using System;

namespace ConsoleApp12.Exceptions
{
    public class NonexistentDecastException : Exception
    {
        public NonexistentDecastException(string abilityName) : base(
            $"This ability has cast an inexistent decast: {abilityName}\n")
        {
        }

        public NonexistentDecastException()
        {
        }
    }
}