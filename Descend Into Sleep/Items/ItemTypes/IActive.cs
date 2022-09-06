using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.ItemTypes
{
    public interface IActive
    {
        public string Active(double damageDealt, Character caster, Character opponent);
    }
}