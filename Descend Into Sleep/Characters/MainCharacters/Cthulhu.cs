using System.Collections.Generic;
using ConsoleApp12.Ability.CthulhuAbilities;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Cthulhu : Character
    {
        private Cthulhu() : base("Cthulhu", 7.5, 100, Dreams.DREAMS, Scales.SCALES, 200,
            new List<string> {"beg for mercy", "worship", "go insane"}, 0.4, 4, "The God which preys on your sanity")
        {
            AddAbility(new TripleHit());
            AddAbility(new MadnessHit());
        }

        public static readonly Cthulhu CTHULHU = new Cthulhu();
    }
}