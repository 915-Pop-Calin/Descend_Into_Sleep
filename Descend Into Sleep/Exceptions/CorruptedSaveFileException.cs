using System;

namespace ConsoleApp12.Exceptions
{
    public class CorruptedSaveFileException: Exception
    {
        public CorruptedSaveFileException(int number) : base("bin\\Debug\\net5.0\\SaveFiles\\savefile" + number +
                                                             ".json cannot be opened; please erase its contents\n")
        {
            
        }

        public CorruptedSaveFileException(String filename) : base(filename +
                                                                  " cannot be opened; please erase its contents\n")
        {
            
        }
    }
}