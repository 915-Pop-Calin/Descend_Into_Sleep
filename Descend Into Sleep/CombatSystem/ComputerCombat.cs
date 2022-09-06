using System;
using System.Linq;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.CombatSystem
{
    public class ComputerCombat : Combat
    {
        public ComputerCombat(Character computerPlayer) : base(computerPlayer)
        {
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var oddsOfAttacking = Player.GetOddsOfAttacking();
            var willAttack = RandomHelper.IsSuccessfulTry(oddsOfAttacking);
            var numberOfAbilities = Player.GetRespectiveAbilities().Count;
            var abilityNumber = RandomHelper.GenerateRandomInInterval(0, numberOfAbilities);
            if (numberOfAbilities == 0)
                willAttack = true;

            if (willAttack)
            {
                var toStrHit = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
                Console.WriteLine(toStrHit);
                TurnCounter++;
                return;
            }

            var abilityKeys = Player.GetRespectiveAbilities().Keys.ToList();
            var chosenAbilityKey = abilityKeys[abilityNumber];
            var chosenAbility = Player.GetRespectiveAbilities()[chosenAbilityKey].GetName();
            var toStr = Player.Cast(chosenAbility, secondCharacter, ListOfTurns, TurnCounter);
            Console.WriteLine(toStr);
            TurnCounter++;
        }
    }
}