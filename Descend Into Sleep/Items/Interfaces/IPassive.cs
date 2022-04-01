using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items
{
    public interface IPassive
    {
        public string Passive(Character caster, Character opponent,
                            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter);
    }
}