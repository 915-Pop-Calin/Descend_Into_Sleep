using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using Microsoft.VisualBasic;

namespace ConsoleApp12.Items.Weapons.Unobtainable
{
    public class OrbOfTheTitans: Weapon
    {
        private readonly double PercentageIncreased;
        private readonly double IncreasedArmourPenetration;
        
        public OrbOfTheTitans() : base(1000, 1000, 0)
        {
            Name = "Orb of the Titans";
            SetEffect();
            PercentageIncreased = 0.15;
            IncreasedArmourPenetration = 0.3;
        }

        public override string Effect(double damageDealt, Character caster, Character opponent)
        {
            var toStr = "";
            if (opponent is FinalBoss finalBoss)
            {
                var casterName = caster.GetName();
                var maximumHealthRatio = opponent.GetMaximumHealthPoints() / caster.GetMaximumHealthPoints();
                var attackValue = caster.GetAttackValue();
                var increasedAttackValue = PercentageIncreased * maximumHealthRatio * attackValue;
                caster.IncreaseAttackValue(increasedAttackValue);
                toStr += casterName + "'s attack was increased by " + increasedAttackValue + "!\n";
                caster.IncreaseArmourPenetration(IncreasedArmourPenetration);
                toStr += "The power of the titans activates!\n";
                toStr += casterName + "'s armour penetration was increased by " + IncreasedArmourPenetration + "!\n";
            }
            return toStr;
        }
        
    }
}