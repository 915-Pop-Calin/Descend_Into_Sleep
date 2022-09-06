using System;

namespace ConsoleApp12.Exceptions
{
    public class StunException : Exception
    {
        public StunException(string characterName) : base(
            $"{characterName} cannot be stunned because they are CC Immune\n")
        {
        }

        public StunException()
        {
        }
    }
}