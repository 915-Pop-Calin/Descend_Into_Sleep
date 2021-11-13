using System;

namespace ConsoleApp12.Exceptions
{
    public class UnopenableSaveFileException: Exception
    {
        public UnopenableSaveFileException(int number) : base("bin\\Debug\\net5.0\\SaveFiles\\savefile" + number +
                                                              ".json cannot be opened; please" +
                                                              " make sure it exists and you have access to it\n")
        {
            
        }

        public UnopenableSaveFileException(String filename) : base(filename + " cannot be opened; please" +
                                                                   " make sure it exists and you have access to it\n")
        {
            
        }
    }
}