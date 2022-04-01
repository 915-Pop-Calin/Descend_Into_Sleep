using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public interface IActive
    {
        public string Active(double damageDealt, Character caster, Character opponent);
    }
}