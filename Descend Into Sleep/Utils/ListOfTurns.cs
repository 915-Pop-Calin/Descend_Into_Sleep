using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Utils
{
    public class ListOfTurns
    {
        private readonly Dictionary<int, List<Func<Character, Character, string>>> Turns;

        public ListOfTurns()
        {
            Turns = new Dictionary<int, List<Func<Character, Character, string>>>();
        }

        public void Add(int key, Func<Character, Character, string> value)
        {
            if (Turns.ContainsKey(key))
                Turns[key].Add(value);
            else
                Turns[key] = new List<Func<Character, Character, string>> {value};
        }

        public List<Func<Character, Character, string>> Get(int key)
        {
            if (Turns.ContainsKey(key))
                return Turns[key];
            return new List<Func<Character, Character, string>>();
        }

        public void Remove(int key)
        {
            if (Turns.ContainsKey(key))
                Turns.Remove(key);
        }

        public List<Func<Character, Character, string>> GetAll()
        {
            var allActions = new List<Func<Character, Character, string>>();
            foreach (var key in Turns.Keys)
                allActions = allActions.Concat(Turns[key]).ToList();
            return allActions;
        }

        public void Clear()
        {
            Turns.Clear();
        }
    }
}