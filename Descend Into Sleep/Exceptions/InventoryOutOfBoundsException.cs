using System;

namespace ConsoleApp12.Exceptions
{
    public class InventoryOutOfBoundsException: Exception
    {
        public InventoryOutOfBoundsException() : base("Inventory is out of bounds")
        {
            
        }
    }
}