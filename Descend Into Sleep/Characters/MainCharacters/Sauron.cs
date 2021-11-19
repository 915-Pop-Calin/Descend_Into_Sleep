using ConsoleApp12.Ability.SauronAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Armours.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Sauron: Character
    {
        private Sauron() : base("Sauron", 2, 200, AllItems.TheRing, AllItems.EyeOfSauron, 200, "The creator of the Ring.\n")
        {
            Level = 6;
            var powerOfTheRingAbility = new PowerOfTheRing();
            var powerOfTheEyeAbility = new PowerOfTheEye();
            AddAbility(powerOfTheRingAbility);
            AddAbility(powerOfTheEyeAbility);
        }

        public static Sauron MainBoss = new Sauron();
    }
}