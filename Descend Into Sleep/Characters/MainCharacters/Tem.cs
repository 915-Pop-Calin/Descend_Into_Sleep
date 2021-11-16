using ConsoleApp12.Ability.TemAbilities;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Tem: Character
    {
        public Tem() : base("Tem", 1, 75, new Eclipse(), new Cloth(), 100, "Comes from Temmie Village\n")
        {
            Level = 1;
            var doNothingAbility = new DoNothing();
            AddAbility(doNothingAbility);
            var healTemAbility = new HealTem();
            AddAbility(healTemAbility);
        }
    }
}