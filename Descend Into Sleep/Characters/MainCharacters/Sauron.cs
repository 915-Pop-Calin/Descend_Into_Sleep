using System.Collections.Generic;
using ConsoleApp12.Ability.SauronAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Armours.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Sauron: Character
    {
        private Sauron() : base("Sauron", 2, 200, AllItems.TheRing, AllItems.EyeOfSauron, 200, 
            new List<string>{"praise", "worship", "pray", "stare into the eye"}, 
            0.3, 6, "The creator of the Ring")
        {
            AddAbility(new PowerOfTheRing());
            AddAbility(new PowerOfTheEye());
        }

        public static readonly Sauron MainBoss = new Sauron();
    }
}