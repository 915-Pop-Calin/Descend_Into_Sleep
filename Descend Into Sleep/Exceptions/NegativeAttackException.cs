using System;

namespace ConsoleApp12.Exceptions
{
    public class NegativeAttackException: Exception
    {
        public NegativeAttackException(string opponentName) : base(opponentName +
                                                                   "'s attack cannot be reduced to 0 or less\n")
        {
            
        }

        public NegativeAttackException()
        {
            
        }
    }
}