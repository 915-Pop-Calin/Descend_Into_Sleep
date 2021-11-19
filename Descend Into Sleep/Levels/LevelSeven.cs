using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelSeven;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Game.keysWork;

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
                    "You have achieved your goal.\n", "You have neutralised the evil in this world.\n",
                    "However, while doing so.\n", "You have succumbed to the darkness you swore to oppose.\n",
                    "And you have became the thing you swore to destroy.\n",
                    "Now, you have to make your final choice.\n",
                    "You can choose to destroy whatever is left of your humanity.\n", "Or you can spare it.\n",
                    "The decision is yours.\n"
                }
            );
        }

        private void DeleteSaveFiles()
        {
            
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
                Environment.Exit(0);
            }
            StartUp();
            var finalDecision = TheDecision();
            if (finalDecision == "spare")
                SpareEnding();
            else
                DestroyEnding();
        }
        
        private void StartUp()
        {
            while (DialogueLines.Count != 1)
            {
                var dialogueLine = DialogueLines.Dequeue();
                Console.WriteLine(dialogueLine);
                ConsoleHelper.MultipleChoice(20, "proceed");
            }
        }

        private string TheDecision()
        {
            var lastDialogueLine = DialogueLines.Dequeue();
            Console.WriteLine(lastDialogueLine);

            var options = new String[2] {"spare", "destroy"};
            
            Console.WriteLine("Your choice is:\n");
            var choice = ConsoleHelper.MultipleChoice(20, "spare", "destroy");
            return options[choice];
        }

        private void SpareEnding()
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
            DeleteSaveFiles();
                // Environment.Exit(0);
            }

        private void DestroyEnding()
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
 
            DeleteSaveFiles();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("BAD ENDING");
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}