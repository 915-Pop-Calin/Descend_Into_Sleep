using System;

namespace ConsoleApp12.Exceptions
{
    public class ImpossibleSpareException: Exception
    {
        public ImpossibleSpareException(String name) : base(name + " cannot be spared yet!\n"){
            
        }
    }
}