﻿using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfYogg: VoidSideEnemy
    {
        public RemnantOfYogg() : base("Remnant of Yogg", 15, 200, AllItems.GiantSlayer, AllItems.NoArmour, 100)
        {
            Level = 7;
        }
    }
}