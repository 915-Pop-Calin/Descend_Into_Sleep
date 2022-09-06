using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.Characters.SideCharacters.LevelThree
{
    public class VoidPossessedAmalgamation : VoidSideEnemy
    {
        public VoidPossessedAmalgamation() : base("Void Possessed Amalgamation", 15, 50, NoWeapon.NO_WEAPON,
            BootsOfDodge.BOOTS_OF_DODGE, 50, new List<string> {"feed", "caress", "purify"},
            0.75, 3)
        {
        }
    }
}