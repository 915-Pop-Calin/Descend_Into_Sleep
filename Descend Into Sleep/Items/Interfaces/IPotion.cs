using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

namespace ConsoleApp12.Items
{
    public interface IPotion: IItem
    {
        public string UseItem(HumanPlayer humanPlayer);
        
        public string ToString()
        {
            var toStr = "";
            toStr += GetName();
            if (GetDescription() != null)
                toStr += $", {GetDescription()}";
            return toStr;
        }
        
    }
}