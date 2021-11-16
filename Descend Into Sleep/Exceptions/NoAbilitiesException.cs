using System;

namespace ConsoleApp12.Exceptions
{
    public class NoAbilitiesException: Exception
    {
        public NoAbilitiesException() : base("there are no abilities to cast")
        {
            
        }
    }
}