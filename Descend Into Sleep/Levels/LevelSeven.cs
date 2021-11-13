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
            MainEnemy = null;
            SideEnemies.Add(typeof(RemnantOfIcarus));
            SideEnemies.Add(typeof(RemnantOfSauron));
            SideEnemies.Add(typeof(RemnantOfYogg));
            
            DialogueLines = new Queue<string>();
            DialogueLines.Enqueue("So this is it.\n");
            DialogueLines.Enqueue("You have reached the end of your journey.\n");
            DialogueLines.Enqueue("You have achieved your goal.\n");
            DialogueLines.Enqueue("You have neutralised the evil in this world.\n");
            DialogueLines.Enqueue("However, while doing so.\n");
            DialogueLines.Enqueue("You have succumbed to the darkness you swore to oppose.\n");
            DialogueLines.Enqueue("And you have became the thing you swore to destroy.\n");
            DialogueLines.Enqueue("Now, you have to make your final choice.\n");
            DialogueLines.Enqueue("You can choose to destroy whatever is left of your humanity.\n");
            DialogueLines.Enqueue("Or you can spare it.\n");
            DialogueLines.Enqueue("The decision is yours.\n");
        }

        private void DeleteSaveFiles()
        {
            
        }

        private bool PastSelfFight(Character pastSelf)
        {
            var genocideCombat = new GenocideCombat(Player, pastSelf);
            return genocideCombat.Combat();
        }
        
        public override int PlayOut()
        {
            if (Player.IsCheater())
            {
                Console.WriteLine("Level cannot be played because you cheated!\n");
                return -2;
            }

            StartUp();
            var finalDecision = TheDecision();
            if (finalDecision == "spare")
                return SpareEnding();
            else
                return DestroyEnding();
        }

        private void StartUp()
        {
            while (DialogueLines.Count != 1)
            {
                var dialogueLine = DialogueLines.Dequeue();
                Console.WriteLine(dialogueLine);
                Console.WriteLine("proceed\n");
                var invalidInput = true;
                while (invalidInput)
                {
                    var command = Console.ReadLine();
                    if (command == "proceed")
                        invalidInput = false;
                }
            }
        }

        private string TheDecision()
        {
            var lastDialogueLine = DialogueLines.Dequeue();
            Console.WriteLine(lastDialogueLine);
            var invalidInput = true;
            while (invalidInput)
            {
                Console.WriteLine("Your choice is:\n");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "destroy":
                        return "destroy";
                    case "spare":
                        return "spare";
                }
            
            }
            return "";
        }

        private int SpareEnding()
        {
            Console.WriteLine("A strange figure appears from the shadows.\n");
            Console.WriteLine("This is the end.\n");
            MainEnemy = typeof(FinalBoss);
            var mainEnemy = (FinalBoss) Activator.CreateInstance(MainEnemy);
            var combat = new Fight(Player, mainEnemy);
            combat.Brawl();
            if (Player.GetHealthPoints() > 0 && Player.GetSanity() > 0)
            {
                Console.WriteLine("GOOD ENDING");
                DeleteSaveFiles();
            }
            else
            {
                Console.WriteLine("BAD ENDING");
                return -1;
            }
            return 1;
        }

        private int DestroyEnding()
        {
            Console.WriteLine("Very Well.\n");
            var firstPastSelf = PastSelves[0];
            var secondPastSelf = PastSelves[1];
            var thirdPastSelf = PastSelves[2];
            var choice = PastSelfFight(firstPastSelf);
            if (choice)
            {
                choice = PastSelfFight(secondPastSelf);
                if (choice)
                {
                    choice = PastSelfFight(thirdPastSelf);
                    if (choice)
                        DeleteSaveFiles();
                }
            }
            Console.WriteLine("BAD ENDING");
            return 1;
        }
    }
}