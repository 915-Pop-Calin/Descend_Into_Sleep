using System.Collections.Generic;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Items.Weapons.Unobtainable;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class FinalBoss : Character
    {
        private int PhaseNumber;
        private string AttackType;
        private readonly Queue<string> DialogueLines;

        private FinalBoss() : base("???????", 75, 10000, SaroniteTentacles.SARONITE_TENTACLES,
            SaroniteScales.SARONITE_SCALES, 10000000, new List<string>(), 0, 7,
            "Mysterious Presence")
        {
            Level = 7;
            PhaseNumber = 1;
            AttackType = "sanity";
            DialogueLines = new Queue<string>();
            DialogueLines.Enqueue("I am the lucid dream");
            DialogueLines.Enqueue("The monster in your nightmares");
            DialogueLines.Enqueue("The fiend of a thousand faces");
            DialogueLines.Enqueue("Bow down before me!");
        }

        public bool CheckIfFormChange()
        {
            if (PhaseNumber == 1 && ((IReflector) Weapon).IsBroken())
            {
                PhaseNumber = 2;
                Health = 10000;
                return true;
            }

            if (PhaseNumber == 2 && ((IReflector) Armour).IsBroken())
            {
                PhaseNumber = 3;
                return true;
            }

            return false;
        }

        public void SetAttackType(string newAttackType)
        {
            AttackType = newAttackType;
        }

        public void SetUltimateForm()
        {
            SetInnateDefense(10000000);
        }

        private string InsanityHit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            int minimumSanityReduced = 1;
            int maximumSanityReduced = 31;
            int sanityReduced = RandomHelper.GenerateRandomInInterval(minimumSanityReduced, maximumSanityReduced);
            string opponentName = opponent.GetName();
            string toStr = $"{opponentName}'s sanity was reduced by {sanityReduced}!\n";
            opponent.ReduceSanity(sanityReduced);
            toStr += $"{opponentName} is left with {opponent.GetSanity()} sanity!\n";
            if (DialogueLines.Count == 0)
                toStr += "...\n";
            else
                toStr += DialogueLines.Dequeue();
            return toStr;
        }

        private string PhysicalHit(Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = base.Hit(opponent, listOfTurns, turnCounter);
            return toStr;
        }

        private string PhysicalInsanityHit(Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            string toStr = InsanityHit(opponent, listOfTurns, turnCounter);
            toStr += PhysicalHit(opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Hit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            switch (AttackType)
            {
                case "physical":
                    return PhysicalHit(opponent, listOfTurns, turnCounter);
                case "sanity":
                    return InsanityHit(opponent, listOfTurns, turnCounter);
                case "both":
                    return PhysicalInsanityHit(opponent, listOfTurns, turnCounter);
                default:
                    return "";
            }
        }

        public int GetPhaseNumber()
        {
            return PhaseNumber;
        }

        public static readonly FinalBoss FINAL_BOSS = new FinalBoss();
    }
}