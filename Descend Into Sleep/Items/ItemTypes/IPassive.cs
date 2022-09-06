using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Items.ItemTypes
{
    public interface IPassive
    {
        public string Passive(Character caster, Character opponent,
            ListOfTurns listOfTurns, int turnCounter);
    }
}