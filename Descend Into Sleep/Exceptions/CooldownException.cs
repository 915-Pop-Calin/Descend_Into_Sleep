using System;

namespace ConsoleApp12.Exceptions
{
    public class CooldownException: Exception
    {
        public CooldownException(string abilityName) : base(abilityName + " is not ready yet\n")
        {
            
        }

        public CooldownException()
        {
            
        }
    }
}