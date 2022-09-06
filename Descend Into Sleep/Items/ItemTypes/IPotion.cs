using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.ItemTypes
{
    public interface IPotion : IItem
    {
        public string UseItem(Character character);

        public string ToString()
        {
            return GetName() + (GetDescription() != null ? $", {GetDescription()}" : "");
        }
    }
}