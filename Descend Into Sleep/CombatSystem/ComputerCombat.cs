using System;
using System.Linq;
using ConsoleApp12.Characters;
using ConsoleApp12.Utils;

namespace ConsoleApp12.CombatSystem
{
    public class ComputerCombat: Combat
    {
        public ComputerCombat(Character computerPlayer) : base(computerPlayer)
        {
            ;
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var willAttack = RandomHelper.IsSuccessfulTry(0.8);
            var numberOfAbilities = Player.GetRespectiveAbilities().Count;
            var abilityNumber = RandomHelper.GenerateRandomInInterval(0, numberOfAbilities);
            if (!Player.AutoAttacker())
                willAttack = false;
            if (numberOfAbilities == 0)
                willAttack = true;
            if (!willAttack)
            {
                var abilityKeys = Player.GetRespectiveAbilities().Keys.ToList();
                var chosenAbilityKey = abilityKeys[abilityNumber];
                var chosenAbility = Player.GetRespectiveAbilities()[chosenAbilityKey].GetName();
                var toStr = Player.Cast(chosenAbility, secondCharacter, ListOfTurns, TurnCounter);
                Console.WriteLine(toStr);
                TurnCounter++;
                return;
            }

            var toStrHit = Player.Hit(secondCharacter, ListOfTurns, TurnCounter);
            Console.WriteLine(toStrHit);
            TurnCounter++;

        }
    }
}