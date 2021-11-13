using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using ConsoleApp12.Items.Weapons.Unobtainable;

namespace ConsoleApp12.CombatSystem
{
    public class LastBossCombat: Combat
    {
        public LastBossCombat(FinalBoss finalBoss): base(finalBoss)
        {
            Player = finalBoss;
        }

        public override void CombatTurn(Character secondCharacter)
        {
            var formChanged = ((FinalBoss) Player).CheckIfFormChange();
            var phaseNumber = ((FinalBoss) Player).GetPhaseNumber();
            if (formChanged)
            {
                switch (phaseNumber)
                {
                    case 2:
                        SecondPhase((HumanPlayer)secondCharacter);
                        break;
                    case 3:
                        ThirdPhase((HumanPlayer)secondCharacter);
                        var toStr = Intervention((HumanPlayer)secondCharacter);
                        Console.WriteLine(toStr);
                        break;
                }
            }
            else
            {
                var toStr = ((FinalBoss) Player).Hit(secondCharacter, ListOfTurns, TurnCounter);
                Console.WriteLine(toStr);
            }

        }

        private void SecondPhase(HumanPlayer humanPlayer)
        {
            Console.WriteLine("ENOUGH of this!\nYou have NO chances of defeating me!\n");
            humanPlayer.DeleteOptions();
            Console.WriteLine("All your abilities have been deleted!\n");
            ((FinalBoss)Player).SetAttackType("physical");
        }

        private void ThirdPhase(HumanPlayer humanPlayer)
        {
            Console.WriteLine("You really can't get enough, can you?");
            Console.WriteLine("Behold then, my ultimate form!");
            ((FinalBoss)Player).SetUltimateForm();
            ((FinalBoss)Player).SetAttackType("both");
        }

        private string Intervention(HumanPlayer humanPlayer)
        {
            var humanPlayerName = humanPlayer.GetName();
            var toStr = humanPlayerName +
                        " look, I don't have much time but I have been studying this for some time.\n";
            toStr += "And I have come to the conclusion that there is only ONE way to beat him.\n";
            toStr += "You have to use this titan construct to strike him right in the heart.\n";
            var titanConstructItem = new OrbOfTheTitans();
            humanPlayer.DirectEquipWeapon(titanConstructItem);
            toStr += "You have equipped " + titanConstructItem.GetName() + "!\n";
            return toStr;
        }
    }
}