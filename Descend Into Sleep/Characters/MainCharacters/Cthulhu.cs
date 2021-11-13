﻿using ConsoleApp12.Ability.CthulhuAbilities;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class Cthulhu: Character
    {
        public Cthulhu() : base("Cthulhu", 7.5, 100, new Dreams(), new Scales(), 200,
            "The God which preys on your sanity.\n")
        {
            var tripleHitAbility = new TripleHit();
            var madnessHitAbility = new MadnessHit();
            AddAbility(tripleHitAbility);
            AddAbility(madnessHitAbility);
        }
    }
}