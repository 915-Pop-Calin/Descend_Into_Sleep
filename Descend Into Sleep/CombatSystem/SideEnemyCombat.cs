using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.SideCharacters;

namespace ConsoleApp12.CombatSystem
{
    public class SideEnemyCombat : Combat
    {
        public SideEnemyCombat(SideEnemy sideEnemyPlayer) : base(sideEnemyPlayer)
        {
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var toStr = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
            Console.WriteLine(toStr);
            TurnCounter++;
        }
    }
}