using System.Collections.Generic;
using ConsoleApp12.Items.Armours.LeverFour;
using ConsoleApp12.Items.Weapons.LevelTwo;

namespace ConsoleApp12.Characters.SideCharacters.LevelFour
{
    public class ParanoiaInducer : VoidSideEnemy
    {
        public ParanoiaInducer() : base("Paranoia Inducer", 30, 30, TacosWhisper.TACOS_WHISPER, Scales.SCALES,
            75, new List<string> {"clear mind", "grasp reality", "purify"}, 0.7, 4)
        {
        }
    }
}