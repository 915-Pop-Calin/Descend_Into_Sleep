using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons.LevelSix;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class FinalBoss : Character
    {
        private int PhaseNumber;
        private string AttackType;
        private Queue<string> DialogueLines;

        public FinalBoss() : base("???????", 75, 10000, new Dreams(), new Bandage(), 10000, "Mysterious Presence.\n")
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
            if (PhaseNumber == 1 && Weapon.IsBroken())
            {
                PhaseNumber = 2;
                return true;
            }

            if (PhaseNumber == 2 && Armour.IsBroken())
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

        private string InsanityHit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var randomObject = new Random();
            var minimumSanityReduced = 1;
            var maximumSanityReduced = 31;
            var sanityReduced = randomObject.Next(minimumSanityReduced, maximumSanityReduced);
            var opponentName = opponent.GetName();
            var toStr = opponentName + "'s sanity was reduced by " + sanityReduced + "!\n";
            toStr += opponentName + " is left with " + opponent.GetSanity() + " sanity!\n";
            if (DialogueLines.Count == 0)
                toStr += "...\n";
            else
                toStr += DialogueLines.Dequeue();
            return toStr;
        }

        private string PhysicalHit(Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = base.Hit(opponent, listOfTurns, turnCounter);
            return toStr;
        }

        private string PhysicalInsanityHit(Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var toStr = InsanityHit(opponent, listOfTurns, turnCounter);
            toStr += PhysicalHit(opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public override string Hit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
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
    }
}