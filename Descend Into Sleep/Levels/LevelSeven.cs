using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters.LevelSeven;
using ConsoleApp12.CombatSystem;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Levels
{
    public class LevelSeven: Level
    {
        private readonly List<PastSelf> PastSelves;
        private Queue<string> DialogueLines;
        
        
        public LevelSeven(HumanPlayer humanPlayer) : base(7, humanPlayer, new Dictionary<Type, int>()
        {
            {typeof(RemnantOfIcarus), 5}, {typeof(RemnantOfSauron), 5}, {typeof(RemnantOfYogg), 6}
        }, new Queue<Character>(), new Shop.Shop(humanPlayer, 7))
        {
            Shop = new Shop.Shop(Player, Number);
            if (humanPlayer != null)
                PastSelves = humanPlayer.GetPastSelves();
            

            DialogueLines = new Queue<string>(    new[]
                {
                    "So this is it.\n", "You have reached the end of your journey.\n",
                    "In your relentless adventure, you have sought to neutralise the evil of this world.\n",
                    "Depending on your path, you will now face your final challenge.\n",
                    "Good luck.\n"
                }
            );
        }
        
        protected override void BossFight()
        {
            if (Player.IsCheater())
            {
                Console.WriteLine("Level cannot be played because you cheated!\n");
                throw new GameOverException();
            }
            StartUp();
            if (Player.GetKillCount() == 0)
            {
                PacifistEnding();
                return;
            }

            if (Player.GetKillCount() == 109)
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
            throw new PacifistEndingException();
        }

        private void NeutralEnding()
        {
            Console.WriteLine("Something appears in the battlefield...?\n");
            Console.WriteLine("This is the final challenge.\n");
            MainEnemies.Enqueue(FinalAmalgamation.MainBoss);

            var mainEnemy = MainEnemies.Dequeue();
            var combat = new Fight(Player, mainEnemy);
            combat.Brawl();
            throw new NeutralEndingException();
        }
        
        private void GenocideEnding()
        {
            for (int i = 0; i < 3; i++)
                MainEnemies.Enqueue(PastSelves[i]);
            GenocideFights();
            throw new GenocideEndingException();
        }

        private void GenocideFights()
        {
            var statuses = new Queue<string>(new[]
            {
                "You have a bad feeling about this",
                "You think you're about to have a bad time",
                "You're going to have a bad time."
            });
        while (MainEnemies.Count != 0)
            {
                
                var currentPastSelf = MainEnemies.Dequeue();
                var currentStatus = statuses.Dequeue();
                Console.WriteLine($"{currentPastSelf.GetName()} appears into the fray.\n{currentStatus}\n");
                var combat = new Fight(Player, currentPastSelf);
                combat.Brawl();
                var result  = Player.Weaken();
                Console.WriteLine($"You feel sickened.\n{Math.Round(result.Item1, 2)} Health Points, {Math.Round(result.Item2, 2)} Attack" +
                                  $" and {Math.Round(result.Item3,2)} Defense are lost.\n \n");
            }
        }
    }
}