using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFive;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class BurningCitizen: FireSideEnemy
    {
        public BurningCitizen() : base("Burning Citizen", 40, 20, AllItems.RadusBiceps, AllItems.EyeOfSauron, 
            100, new List<string>{"extinguish", "patch up", "calm down", "reassure"}, 0.6, 5)
        {
        }
    }
}