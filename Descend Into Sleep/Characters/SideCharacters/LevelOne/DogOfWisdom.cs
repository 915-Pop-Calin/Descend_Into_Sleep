using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWisdom: SideEnemy
    {
        public DogOfWisdom() : base("Dog of Wisdom", 3, 3, AllItems.ToyKnife, AllItems.Bandage, 25)
        {
            Level = 1;
            Actions = new List<string> { "pet", "run at", "love"};
            OrderOfActions = new Queue<string>(new [] {"run at", "pet", "love"});
            ChanceOfSuccessfulAct = 0.99;
        }
    }
}