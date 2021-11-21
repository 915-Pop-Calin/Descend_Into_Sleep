using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Game.keysWork;

namespace ConsoleApp12.CombatSystem
{
    public class GenocideCombat
    {

        private HumanPlayer HumanPlayer;
        private Character SecondPlayer;
        public GenocideCombat(HumanPlayer humanPlayer, Character secondPlayer)
        {
            HumanPlayer = humanPlayer;
            SecondPlayer = secondPlayer;
        }

        public bool Combat()
        {
            var computerPlayerName = SecondPlayer.GetName();
            var humanPlayerName = HumanPlayer.GetName();
            Console.WriteLine(computerPlayerName + " arrives into the fray!\n");

            while (true)
            {
                const string question = "";
                var choice = ConsoleHelper.MultipleChoice(20, question, "MURDER", "SPARE", "STATS");
                Console.WriteLine("Your choice is:\n");
                switch (choice)
                {
                    case 0:
                        HumanPlayer.DealDirectDamage(SecondPlayer, int.MaxValue);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(computerPlayerName + " has been MURDERED!\n");
                        Console.ResetColor();
                        return true;
                    case 1:
                        Console.WriteLine("And I thought highly of you.\n");
                        HumanPlayer.DealDirectDamage(HumanPlayer, int.MaxValue);
                        Console.WriteLine(humanPlayerName + " was lost to his insanity!\n");
                        return false;
                    case 2:
                        Console.WriteLine(SecondPlayer);
                        break;
                }
            }
        }
    }
}