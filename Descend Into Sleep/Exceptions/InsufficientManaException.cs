using System;

namespace ConsoleApp12.Exceptions
{
    public class InsufficientManaException: Exception
    {
        public InsufficientManaException(double currentMana, double requiredMana, string characterName, string abilityName) : base(
            $"{abilityName} cannot be cast because {characterName} has only {currentMana} Mana while {requiredMana} Mana is " +
            "required\n")
        {
            
        }

        public InsufficientManaException()
        {
            
        }
    }
}