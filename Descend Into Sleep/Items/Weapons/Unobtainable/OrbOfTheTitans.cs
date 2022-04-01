using System;
using ConsoleApp12.Characters;
using ConsoleApp12.Characters.MainCharacters;
using Microsoft.VisualBasic;

namespace ConsoleApp12.Items.Weapons.Unobtainable
{
    public class OrbOfTheTitans: IWeapon, IActive, IDefense
    {
        private readonly double PercentageIncreased;
        private readonly double IncreasedArmourPenetration;
        
        public OrbOfTheTitans()
        {
            PercentageIncreased = 0.15;
            IncreasedArmourPenetration = 0.3;
        }
        
        public double GetAttackValue()
        {
            return 1000;
        }

        public double GetDefenseValue()
        {
            return 1000;
        }

        public string GetName()
        {
            return "Orb of the Titans";
        }

        public string GetDescription()
        {
            return "Orb created by the Ancient Titans to defeat all evil";
        }

        public string Active(double damageDealt, Character caster, Character opponent)
        {
            var toStr = "";
            if (opponent is FinalBoss finalBoss)
            {
                var maximumHealthRatio = opponent.GetMaximumHealthPoints() / caster.GetMaximumHealthPoints();
                var attackValue = caster.GetAttackValue();
                var increasedAttackValue = PercentageIncreased * maximumHealthRatio * attackValue;
                caster.IncreaseAttackValue(increasedAttackValue);
                toStr += $"{caster.GetName()}'s attack was increased by {increasedAttackValue}!\n";
                caster.IncreaseArmourPenetration(IncreasedArmourPenetration);
                toStr += "The power of the titans activates!\n";
                toStr += $"{caster.GetName()}'s armour penetration was increased by {IncreasedArmourPenetration}!\n";
                toStr += $"{caster.GetName()} now has {Math.Round(caster.GetAttackValue(), 2)} attack and " +
                         $"{caster.GetArmourPenetration() * 100}% armour penetration!\n";
            }
            return toStr;
        }
    }
}