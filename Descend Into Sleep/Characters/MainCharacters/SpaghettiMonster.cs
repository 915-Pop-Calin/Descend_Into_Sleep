using ConsoleApp12.Ability.SpaghettiMonsterAbilities;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class SpaghettiMonster: Character
    {
        public SpaghettiMonster(): base("Spaghetti Monster", 1, 300, new Words(), new TemArmour(), 100, "Represents the Developer\n")
        {
            var entangleAbility = new Entangle();
            var defensiveStanceAbility = new DefensiveStance();
            AddAbility(entangleAbility);
            AddAbility(defensiveStanceAbility);
        }
    }
}