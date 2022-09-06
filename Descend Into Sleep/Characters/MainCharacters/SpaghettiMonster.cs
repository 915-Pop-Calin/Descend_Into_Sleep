using System.Collections.Generic;
using ConsoleApp12.Ability.SpaghettiMonsterAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class SpaghettiMonster: Character
    {
        private SpaghettiMonster(): base("Spaghetti Monster", 10, 300, Words.WORDS, TemArmour.TEM_ARMOUR, 100, 
            new List<string>{"taste", "decode", "rewrite"},0.5, 2, "Represents the Developer")
        {
            AddAbility(new Entangle());
            AddAbility(new DefensiveStance());
        }

        public static readonly SpaghettiMonster SPAGHETTI_MONSTER = new SpaghettiMonster();
    }
}