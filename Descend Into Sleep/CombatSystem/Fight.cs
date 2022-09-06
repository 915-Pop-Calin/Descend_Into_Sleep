using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Characters.SideCharacters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.CombatSystem
{
    public class Fight
    {
        private readonly HumanPlayer HumanPlayer;
        private readonly Character ComputerPlayer;
        private int Turn;
        private bool CombatDone;
        private int TurnCounter;
        private readonly HumanCombat HumanCombat;
        private readonly Combat ComputerCombat;
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
                GoldDivider = 1;
                ExperienceDivider = 1;
                if (ComputerPlayer is FinalBoss finalBoss)
                    ComputerCombat = new LastBossCombat(finalBoss);
                else
                    ComputerCombat = new ComputerCombat(ComputerPlayer);
            }
        }

        private KeyValuePair<int, int> PostCombatGains()
        {
            int gameLevel = ComputerPlayer.GetLevel();
            int minimumGoldToGain = 10 * (TurnCounter + 1) * gameLevel + 100;
            int maximumGoldToGain = 10 * (TurnCounter + 1) * gameLevel + 200;
            int goldToGain = RandomHelper.GenerateRandomInInterval(minimumGoldToGain, maximumGoldToGain);
            goldToGain /= GoldDivider;

            int minimumExperienceToGain = (TurnCounter + 1) * gameLevel + 200 * gameLevel;
            int maximumExperienceToGain = 2 * (TurnCounter + 1) * gameLevel + 200 * gameLevel;
            int experienceToGain =
                RandomHelper.GenerateRandomInInterval(minimumExperienceToGain, maximumExperienceToGain);
            experienceToGain /= ExperienceDivider;

            var keyValuePair = new KeyValuePair<int, int>(goldToGain, experienceToGain);
            return keyValuePair;
        }

        private bool IsCombatDone()
        {
            return ComputerPlayer.GetHealthPoints() <= 0 || ComputerPlayer.IsSpared();
        }

        private void PlayerTurn()
        {
            if (!HumanCombat.DotCheck(ComputerPlayer) || !HumanCombat.CheckUndos(ComputerPlayer))
            {
                ComputerWin();
                return;
            }

            if (!HumanCombat.CheckStun())
            {
                if (!CombatDone)
                {
                    HumanCombat.CombatTurn(ComputerPlayer);
                    if (IsCombatDone())
                    {
                        HumanWin();
                    }
                }
            }

            Turn = 1;
            TurnCounter++;
        }

        private void HumanWin()
        {
            CombatDone = true;
            Console.WriteLine($"{HumanPlayer.GetName()} has won!\n");
            if (ComputerPlayer.GetHealthPoints() <= 0)
                HumanPlayer.IncrementKillCount();
            ComputerCombat.FightEnd(HumanPlayer);
            HumanCombat.FightEnd(ComputerPlayer);
            var goldAndExperience = PostCombatGains();
            HumanCombat.PostCombat(goldAndExperience.Value, goldAndExperience.Key);
        }

        private void ComputerWin()
        {
            Console.WriteLine($"{ComputerPlayer.GetName()} has won!\n");
            ComputerCombat.FightEnd(HumanPlayer);
            HumanCombat.FightEnd(ComputerPlayer);
            throw new GameOverException();
        }

        private void ComputerTurn()
        {
            if (HumanPlayer.GetHealthPoints() <= 0)
            {
                ComputerWin();
                return;
            }

            if (!ComputerCombat.DotCheck(HumanPlayer) || !ComputerCombat.CheckUndos(HumanPlayer))
            {
                HumanWin();
                return;
            }

            if (!ComputerCombat.CheckStun() && !CombatDone)
            {
                ComputerCombat.CombatTurn(HumanPlayer);
                if (HumanPlayer.GetHealthPoints() <= 0 || HumanPlayer.GetSanity() <= 0)
                {
                    ComputerWin();
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