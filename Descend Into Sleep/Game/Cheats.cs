using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Levels;

namespace ConsoleApp12.Game
{
    public class Cheats
    {
        public Dictionary<string, Func<string>> ListOfCheats;
        private HumanPlayer Player;

        public Cheats(HumanPlayer player)
        {
            Player = player;

            ListOfCheats = new Dictionary<string, Func<string>>();
            Func<string> godModeCheat = delegate()
            {
                return GodMode();
            };
            ListOfCheats["mpcezarrus"] = godModeCheat;
            
            Func<string> infiniteGoldCheat = delegate()
            {
                return InfiniteGold();
            };
            ListOfCheats["mpgreedisgood"] = infiniteGoldCheat;
        }

        private string GodMode()
        {
            Player.SetInnateAttack(int.MaxValue);
            Player.SetInnateDefense(int.MaxValue);
            Player.SetInnateMaximumHealth(int.MaxValue);
            var toStr = "God mode has been activated!\n";
            return toStr;
        }

        private string InfiniteGold()
        {
            Player.GainGold(int.MaxValue);
            var toStr = "You have gained maximum gold!\n";
            return toStr;
        }
        
        
    }
}