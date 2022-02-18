using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Weapons.LevelFour;

namespace ConsoleApp12.Characters.SideCharacters.LevelSeven
{
    public class RemnantOfYogg: VoidSideEnemy
    {
        public RemnantOfYogg() : base("Remnant of Yogg", 15, 200, AllItems.GiantSlayer, AllItems.NoArmour, 
            100, new List<string>{"beg for mercy", "pray", "worship", "follow"}, 0.5, 7)
        {
        }
    }
}