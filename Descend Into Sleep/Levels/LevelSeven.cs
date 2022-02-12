using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelSeven;
using ConsoleApp12.CombatSystem;

namespace ConsoleApp12.Levels
{
    public class LevelSeven: Level
    {
        private readonly List<Character> PastSelves;
        private Queue<string> DialogueLines;
        
        public LevelSeven(HumanPlayer humanPlayer) : base(7, humanPlayer)
        {
            Shop = new Shop.Shop(Player, Number);
            if (humanPlayer != null)
                PastSelves = humanPlayer.GetPastSelves();
            
            SideEnemies.Add(typeof(RemnantOfIcarus));
            SideEnemies.Add(typeof(RemnantOfSauron));
            SideEnemies.Add(typeof(RemnantOfYogg));

            DialogueLines = new Queue<string>(    new[]
                {
                    "So this is it.\n", "You have reached the end of your journey.\n",
                    "In your relentless adventure, you have sought to neutralise the evil of this world.\n",
                    "Depending on your path, you will now face your final challenge.\n",
                    "Good luck.\n"
                }
            );
        }
        
        private bool PastSelfFight(Character pastSelf)
        {
            var genocideCombat = new GenocideCombat(Player, pastSelf);
            return genocideCombat.Combat();
        }

        protected override void BossFight()
        {
            if (Player.IsCheater())
            {
                Console.WriteLine("Level cannot be played because you cheated!\n");
                throw new ExitGameException();
            }
            StartUp();
            if (Player.GetKillCount() == 0)
            {
                PacifistEnding();
                return;
            }

            if (Player.GetKillCount() >= 100)
            {
                GenocideEnding();
                return;
            }
            NeutralEnding();
            Console.WriteLine("THIS ENDING IS NOT DONE YET");
        }
        
        private void StartUp()
        {
            while (DialogueLines.Count != 1)
            {
                var question = DialogueLines.Dequeue();
                Utils.keysWork.Utils.MultipleChoice(20, question, "proceed");
            }
        }
        
        private void PacifistEnding()
        {
            Console.WriteLine("A strange figure appears from the shadows.\n");
            Console.WriteLine("This is the end.\n");
            MainEnemies.Enqueue(FinalBoss.MainBoss);

            var mainEnemy = MainEnemies.Dequeue();
            var combat = new Fight(Player, mainEnemy);
            combat.Brawl();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("GOOD ENDING");
            Console.ResetColor();
        }

        private void NeutralEnding()
        {
            
        }
        
        private void GenocideEnding()
        {
            Console.WriteLine("Very Well.\n");
            for (int i = 0; i < 3; i++)
                MainEnemies.Enqueue(PastSelves[i]);

            var goFurther = true;
            while (goFurther && MainEnemies.Count != 0)
            {
                var currentPastSelf = MainEnemies.Dequeue();
                goFurther = PastSelfFight(currentPastSelf);
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("BAD ENDING");
            Console.ResetColor();
            throw new ExitGameException();
        }
    }
}