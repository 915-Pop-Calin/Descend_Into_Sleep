using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class WorshipperOfTheSun: FireSideEnemy
    {
        public WorshipperOfTheSun() : base("Worshipper Of The Sun", 60, 60, AllItems.NoWeapon, AllItems.NoArmour, 150)
        {
            Level = 5;
        }
    }
}