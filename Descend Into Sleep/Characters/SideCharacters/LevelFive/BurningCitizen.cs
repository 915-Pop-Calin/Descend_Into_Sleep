using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelFive;
using ConsoleApp12.Items.Weapons.LevelFive;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class BurningCitizen: FireSideEnemy
    {
        public BurningCitizen() : base("Burning Citizen", 40, 20, AllItems.RadusBiceps, AllItems.EyeOfSauron, 100)
        {
            Level = 5;
        }
    }
}