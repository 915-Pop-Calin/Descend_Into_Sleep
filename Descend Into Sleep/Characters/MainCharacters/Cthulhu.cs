using ConsoleApp12.Ability.CthulhuAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Cthulhu: Character
    {
        public Cthulhu() : base("Cthulhu", 7.5, 100, AllItems.Dreams, AllItems.Scales, 200,
            "The God which preys on your sanity.\n")
        {
            Level = 4;
            var tripleHitAbility = new TripleHit();
            var madnessHitAbility = new MadnessHit();
            AddAbility(tripleHitAbility);
            AddAbility(madnessHitAbility);
        }
    }
}