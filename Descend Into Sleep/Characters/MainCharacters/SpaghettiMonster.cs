using ConsoleApp12.Ability.SpaghettiMonsterAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class SpaghettiMonster: Character
    {
        private SpaghettiMonster(): base("Spaghetti Monster", 10, 300, AllItems.Words, AllItems.TemArmour, 100, "Represents the Developer")
        {
            Level = 2;
            AddAbility(new Entangle());
            AddAbility(new DefensiveStance());
        }

        public static readonly SpaghettiMonster MainBoss = new SpaghettiMonster();
    }
}