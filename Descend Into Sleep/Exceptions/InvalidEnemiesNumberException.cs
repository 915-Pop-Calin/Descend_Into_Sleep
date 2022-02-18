using System;

namespace ConsoleApp12.Exceptions
{
    public class InvalidEnemiesNumberException: Exception
    {
        public InvalidEnemiesNumberException(int expected, int got) : base($"Expected {expected} enemies," +
                                                                           $"got {got} enemies")
        {
            
        }
    }
}