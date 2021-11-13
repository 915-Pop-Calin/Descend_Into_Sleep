using System;

namespace ConsoleApp12.Exceptions
{
    public class InsufficientManaException: Exception
    {
        public InsufficientManaException(double currentMana, double requiredMana, string characterName) : base(
            "Ability cannot be cast because " + characterName + " has only " + currentMana + " while " + requiredMana +
            " is required\n")
        {
            
        }

        public InsufficientManaException()
        {
            
        }
    }
}