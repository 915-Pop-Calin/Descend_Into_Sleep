﻿using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfRashness: SideEnemy
    {
        public DogOfRashness(): base("Dog Of Rashness", 7, 1, AllItems.Eclipse, AllItems.Bandage, 15)
        {
            Level = 1;
        }
    }
}