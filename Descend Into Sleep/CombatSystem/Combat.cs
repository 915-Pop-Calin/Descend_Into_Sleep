using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;

namespace ConsoleApp12.CombatSystem
{
    public abstract class Combat
    {
        protected Dictionary<int, List<Func<Character, Character, string>>> ListOfTurns;
        protected int TurnCounter;
        protected Character Player;

        protected Combat(Character player)
        {
            Player = player;
            ListOfTurns = new Dictionary<int, List<Func<Character, Character, string>>>();
            TurnCounter = 0;
        }

        public bool CheckStun()
        {
            if (Player.IsStunned())
            {
                var toStr = $"{Player.GetName()}'s turn was skipped because he was stunned!\n";
                Console.WriteLine(toStr);
                TurnCounter++;
                return true;
            }
            return false;
        }

        public bool CheckUndos(Character secondCharacter)
        {
            if (ListOfTurns.ContainsKey(TurnCounter))
            {
                var listOfActions = ListOfTurns[TurnCounter];
                foreach (var action in listOfActions)
                {
                    var toStr = action(Player, secondCharacter);
                    Console.WriteLine(toStr);
                }

                ListOfTurns.Remove(TurnCounter);
            }
            if (Player.GetHealthPoints() <= 0)
                return false;
            return true;
        }

        public void FightEnd(Character secondCharacter)
        {
            var dotEffects = Player.GetDotEffects();
            if (dotEffects.Count != 0)
                Player.ClearDotEffects();
            if (ListOfTurns.Count != 0)
            {
                foreach (var key in ListOfTurns.Keys)
                {
                    foreach (var action in ListOfTurns[key])
                    {
                        var toStr = action(Player, secondCharacter);
                        Console.WriteLine(toStr);
                    }
                }
                ListOfTurns.Clear();
            }
        }

        public bool DotCheck(Character secondCharacter)
        {
            var dotEffects = Player.GetDotEffects();
            var toStr = "";
            if (dotEffects.Count != 0)
            {
                int index = 0;
                while (index < dotEffects.Count)
                {
                    Player.ReduceHealthPoints(dotEffects[index].DamagePerTurn);
                    toStr += $"{Player.GetName()} has taken {dotEffects[index].DamagePerTurn} damage over time!\n";
                    var leftTurns = dotEffects[index].NumberOfTurns;
                    Player.DecreaseDotEffect(index);
                    if (leftTurns != 1)
                        index++;
                }
            }
            Console.WriteLine(toStr);
            if (Player.GetHealthPoints() <= 0)
                return false;
            return true;
        }

        public abstract void CombatTurn(Character secondCharacter);

    }
}