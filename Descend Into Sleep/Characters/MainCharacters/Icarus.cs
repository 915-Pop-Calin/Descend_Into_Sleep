using System.Collections.Generic;
using ConsoleApp12.Ability.IcarusAbilities;
using ConsoleApp12.Items.Armours.LevelTwo;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Icarus : Character
    {
        private Icarus() : base("Icarus", 0, 100, IcarusesTouch.ICARUSES_TOUCH, SteelPlateau.STEEL_PLATEAU, 200,
            new List<string> {"extinguish", "heal", "explain yourself", "enlighten"}, 0.35, 5,
            "The corrupted mythological figure by flying too close to the sun")
        {
            AddAbility(new Burn());
            AddAbility(new BurningWill());
        }

        public static readonly Icarus ICARUS = new Icarus();
    }
}