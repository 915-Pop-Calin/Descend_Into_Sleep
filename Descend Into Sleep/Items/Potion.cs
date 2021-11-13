using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items
{
    public abstract class Potion: Item
    {
        protected Potion()
        {
            
        }

        public abstract string UseItem(HumanPlayer humanPlayer);
        
        public override string ToString()
        {
            var toStr = "";
            toStr += Name;
            if (Description != null)
                toStr += ", " + Description;
            return toStr;
        }
        
    }
}