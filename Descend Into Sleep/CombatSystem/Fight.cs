using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;

namespace ConsoleApp12.CombatSystem
{
    public class Fight
    {
        private HumanPlayer HumanPlayer;
        private Character ComputerPlayer;
        private int Turn;
        private bool CombatDone;
        private int TurnCounter;
        private HumanCombat HumanCombat;
        private Combat ComputerCombat;
        private readonly int GoldDivider;
        private readonly int ExperienceDivider;
        
        public Fight(HumanPlayer humanPlayer, Character computerPlayer)
        {
            HumanPlayer = humanPlayer;
            ComputerPlayer = computerPlayer;
            Turn = 0;
            CombatDone = false;
            TurnCounter = 0;
            HumanCombat = new HumanCombat(HumanPlayer);
            if (ComputerPlayer is SideEnemy sideEnemy)
            {
                ComputerCombat = new SideEnemyCombat(sideEnemy);
                GoldDivider = 5;
                ExperienceDivider = 2;
            }
            else
            {
                if (ComputerPlayer is FinalBoss finalBoss)
                {
                    ComputerCombat = new LastBossCombat(finalBoss);
                    GoldDivider = 1;
                    ExperienceDivider = 1;
                }

                else
                {
                    ComputerCombat = new ComputerCombat(ComputerPlayer);
                    GoldDivider = 1;
                    ExperienceDivider = 1;
                }
            }
        }

        private KeyValuePair<int, int> PostCombatGains()
        {
            int gameLevel = ComputerPlayer.GetLevel();
            int minimumGoldToGain = 10 * (TurnCounter + 1) * gameLevel + 100;
            int maximumGoldToGain = 10 * (TurnCounter + 1) * gameLevel + 200;
            var randomObject = new Random();
            int goldToGain = randomObject.Next(minimumGoldToGain, maximumGoldToGain);
            goldToGain /= GoldDivider;

            int minimumExperienceToGain = 4 * (TurnCounter + 1) * gameLevel + 100;
            int maximumExperienceToGain = 4 * (TurnCounter + 1) * gameLevel + 200;
            int experienceToGain = randomObject.Next(minimumExperienceToGain, maximumExperienceToGain);
            experienceToGain /= ExperienceDivider;
            
            var keyValuePair = new KeyValuePair<int, int>(goldToGain, experienceToGain);
            return keyValuePair;
        }
        
        private void PlayerTurn()
        {
            var verdict = HumanCombat.DotCheck(ComputerPlayer);
            if (verdict == -1)
            {
                CombatDone = true;
                var dotDeathStr = ComputerPlayer.GetName() + " has won!\n";
                Console.WriteLine(dotDeathStr);
                Environment.Exit(0);
                return;
            }

            if (!HumanCombat.CheckUndos(ComputerPlayer))
            {
                Environment.Exit(0);
            }

            if (!HumanCombat.CheckStun())
            {
                if (!CombatDone)
                {
                    HumanCombat.CombatTurn(ComputerPlayer);
                    if (ComputerPlayer.GetHealthPoints() <= 0)
                    {
                        var goldAndExperience = PostCombatGains();
                        
                        var winningStr = HumanPlayer.GetName() + " has won!\n";
                        Console.WriteLine(winningStr);
                        
                        HumanCombat.FightEnd(ComputerPlayer);
                        ComputerCombat.FightEnd(HumanPlayer);
                        HumanCombat.PostCombat(goldAndExperience.Value, goldAndExperience.Key);
                        CombatDone = true;
                    }
                }
            }
            Turn = 1;
            TurnCounter++;
        }

        private void ComputerTurn()
        {
            if (HumanPlayer.GetHealthPoints() <= 0)
            {
                Console.WriteLine(ComputerPlayer.GetName() + " has won!\n");
                ComputerCombat.FightEnd(HumanPlayer);
                HumanCombat.FightEnd(ComputerPlayer);
                Environment.Exit(0);
                return;
            }

            var dotVerdict = ComputerCombat.DotCheck(HumanPlayer);
            if (dotVerdict == -1)
            {
                CombatDone = true;
                Console.WriteLine(HumanPlayer.GetName() + " has won!\n");
                return;
            }

            ComputerCombat.CheckUndos(HumanPlayer);
            if (!ComputerCombat.CheckStun())
            {
                if (!CombatDone)
                {
                    ComputerCombat.CombatTurn(HumanPlayer);
                    if (HumanPlayer.GetHealthPoints() <= 0 || HumanPlayer.GetSanity() <= 0)
                    {
                        Console.WriteLine(ComputerPlayer.GetName() + " has won!\n");
                        ComputerCombat.FightEnd(HumanPlayer);
                        HumanCombat.FightEnd(ComputerPlayer);
                        CombatDone = true;
                        Environment.Exit(0);
                    }
                }
            }

            Turn = 0;

        }

        public void Brawl()
        {
            while (!CombatDone)
            {
                if (Turn == 0)
                    PlayerTurn();
                else
                    ComputerTurn();
            }
        }
    }
}