using System.Collections.Generic;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;

namespace ConsoleApp12.Characters.SideCharacters.LevelOne
{
    public class DogOfWar: SideEnemy
    {
        public DogOfWar() : base("Dog Of War", 7, 1, AllItems.NoWeapon, AllItems.Cloth, 25)
        {
            Level = 1;
            Actions = new List<string> { "pet", "run at", "love"};
            OrderOfActions = new Queue<string>(new [] {"run at", "pet", "love"});
            ChanceOfSuccessfulAct = 0.99;
        }        
    }
}