using System.Collections.Generic;
using ConsoleApp12.Ability.TemAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Tem: Character
    {
        private Tem() : base("Tem", 1, 75, AllItems.Eclipse, AllItems.Cloth, 
            100, new List<string> {"pet", "hug", "fund college"}, 0.6, 1, "Comes from Temmie Village")
        {
            AddAbility(new DoNothing());
            AddAbility(new HealTem());
        }

        public static readonly Tem MainBoss = new Tem();
    }
}