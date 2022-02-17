using ConsoleApp12.Ability.IcarusAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Icarus: Character
    {
        private Icarus() : base("Icarus", 0, 100, AllItems.IcarusesTouch, AllItems.SteelPlateau, 200,
            "The corrupted mythological figure by flying too close to the sun")
        {
            Level = 5;
            AddAbility(new Burn());
            AddAbility(new BurningWill());
        }

        public static readonly Icarus MainBoss = new Icarus();
    }
}