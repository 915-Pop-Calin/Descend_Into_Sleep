using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;

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
            Console.WriteLine("MURDER\nSPARE\nSTATS\n");
            var invalidInput = true;
            while (invalidInput)
            {
                Console.WriteLine("Your choice is:\n");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "STATS":
                        Console.WriteLine(SecondPlayer);
                        break;
                    case "MURDER":
                        HumanPlayer.DealDirectDamage(SecondPlayer, int.MaxValue);
                        Console.WriteLine(computerPlayerName + " has been defeated!\n");
                        return true;
                    case "SPARE":
                        Console.WriteLine("And I thought highly of you.\n");
                        HumanPlayer.DealDirectDamage(HumanPlayer, int.MaxValue);
                        Console.WriteLine(humanPlayerName + " has been murdered!\n");
                        return false;
                }
            }
            return true;
        }
    }
}