using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelTwo;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class ParanoiaInducer: VoidSideEnemy
    {
        public ParanoiaInducer() : base("Paranoia Inducer", 30, 30, new TacosWhisper(), new Scales(),
            75)
        {
            
        }
    }
}