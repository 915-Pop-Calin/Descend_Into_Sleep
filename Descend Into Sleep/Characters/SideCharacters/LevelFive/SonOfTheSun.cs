using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.SideCharacters.LevelFive
{
    public class SonOfTheSun: FireSideEnemy
    {
        public SonOfTheSun() : base("Son Of The Sun", 30, 30, new NoWeapon(), new NoArmour(), 200)
        {
            Level = 5;
        }
    }
}