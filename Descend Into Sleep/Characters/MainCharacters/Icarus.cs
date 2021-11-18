using ConsoleApp12.Ability.IcarusAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Icarus: Character
    {
        public Icarus() : base("Icarus", 0, 100, AllItems.IcarusesTouch, AllItems.SteelPlateau, 200,
            "The corrupted mythological figure by flying too close to the sun.\n")
        {
            Level = 5;
            var burnAbility = new Burn();
            var burningWillAbility = new BurningWill();
            AddAbility(burnAbility);
            AddAbility(burningWillAbility);
        }
    }
}