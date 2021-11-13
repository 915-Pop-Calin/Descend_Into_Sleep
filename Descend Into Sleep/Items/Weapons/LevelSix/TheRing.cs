using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelSix
{
    public class TheRing: Weapon
    {
        public TheRing() : base(1, 0)
        {
            SetEffect();
            Name = "The Ring";
            Description = "Scaling weapon. Starts off very weak but gains one attack on each attack";
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var attackValue = GetAttackValue();
            var randomObject = new Random();
            var randomChoice = randomObject.Next(1, 5);
            var toStr = "";
            if (randomChoice == 1)
            {
                opponent.ReduceSanity(attackValue);
                var opponentSanity = opponent.GetSanity();
                toStr = opponent.GetName() + "'s sanity was reduced by " + attackValue.ToString() + "!\n";
                toStr += opponent.GetName() + " is left with " + opponentSanity.ToString() + " sanity!\n";
            }
            IncrementAttackValue();
            toStr += caster.GetName() + "'s weapon attack was increased by 1!\n";
            caster.IncreaseAttackValue(1);
            return toStr;
        }
    }
}