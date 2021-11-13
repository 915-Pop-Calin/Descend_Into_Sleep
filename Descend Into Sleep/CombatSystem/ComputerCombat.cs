using System;
using System.Linq;
using ConsoleApp12.Characters;

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
            var randomObject = new Random();
            var willAttack = randomObject.Next(1, 5);
            var numberOfAbilities = Player.GetRespectiveAbilities().Count;
            var abilityNumber = randomObject.Next(0, numberOfAbilities);
            if (!Player.AutoAttacker())
                willAttack = 4;

            if (willAttack == 4 && numberOfAbilities > 0)
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