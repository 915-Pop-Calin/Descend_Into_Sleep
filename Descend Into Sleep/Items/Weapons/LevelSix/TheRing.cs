using System;
using ConsoleApp12.Characters;

namespace ConsoleApp12.Items.Armours.LevelSix
{
    public class TheRing: Weapon
    {
        private readonly int DamageIncrease;
        
        public TheRing() : base(1, 0, 0)
        {
            SetEffect();
            Name = "The Ring";
            DamageIncrease = 1;
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
                toStr = opponent.GetName() + "'s sanity was reduced by " + attackValue + "!\n";
                toStr += opponent.GetName() + " is left with " + opponentSanity + " sanity!\n";
            }

            var currentInnateAttack = caster.GetInnateAttack();
            caster.SetInnateAttack(currentInnateAttack + DamageIncrease);
            toStr += caster.GetName() + "'s attack was increased by " + DamageIncrease + "!\n";
            return toStr;
        }
    }
}