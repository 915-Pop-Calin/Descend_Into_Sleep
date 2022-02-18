using System.Collections.Generic;
using ConsoleApp12.Ability.CthulhuAbilities;
using ConsoleApp12.Ability.IcarusAbilities;
using ConsoleApp12.Ability.SauronAbilities;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class FinalAmalgamation: Character
    {

        private FinalAmalgamation() : base("Final Amalgamation", 40, 300, AllItems.InfinityEdge, AllItems.LastStand,
            1000, new List<string>{"feed", "caress", "tame", "take care of", "reassure", "enlighten"},
            0.2, 7,"We actually don't know where this came from")
        {
            Level = 7;
            AddAbility(new Burn());
            AddAbility(new BurningWill());
            AddAbility(new PowerOfTheEye());
            AddAbility(new PowerOfTheRing());
            AddAbility(new MadnessHit());
            AddAbility(new TripleHit());
        }

        public override double GetOddsOfAttacking()
        {
            return 0;
        }

        public static readonly FinalAmalgamation MainBoss = new FinalAmalgamation();
    }
}