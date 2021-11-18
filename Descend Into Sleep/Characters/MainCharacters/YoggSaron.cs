using System;
using ConsoleApp12.Ability.YoggSaronAbilities;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class YoggSaron: Character
    {
        private int DiscourageCounter;
        
        public YoggSaron() : base("YoggSaron", int.MaxValue, int.MaxValue, AllItems.BoilingBlood, AllItems.BootsOfDodge,
            int.MaxValue, "The God Of Death.\n")
        {
            Level = 3;
            DiscourageCounter = 3;
        }

        public override void DecreaseAttackValue(double attackValue)
        {
            if (attackValue - Attack < 0.0001)
            {
                Attack = 0;
                if (DiscourageCounter > 0)
                    Console.WriteLine("Yogg Saron's touch on this world slips a bit!\n");
                DiscourageCounter -= 1;
                if (DiscourageCounter == 0)
                {
                    Console.WriteLine("Yogg Saron's touch on this world fully slipped!\n");
                    FormChange();
                }
            }
            else
            {
                Attack -= attackValue;
            }
        }

        private void FormChange()
        {
            Console.WriteLine("Avatar of Yogg Saron appears!\n");
            Name = "Avatar of Yogg Saron";
            InnateAttack = 10;
            InnateDefense = 100;
            Health = 100;
            MaximumHealth = 100;
            Description = "A manifestation of the God of Death.\n";
            Attack = InnateAttack + Weapon.GetAttackValue() + Armour.GetAttackValue();
            Defense = InnateDefense + Weapon.GetDefenseValue() + Armour.GetDefenseValue();
            CriticalChance = InnateCriticalChance + Weapon.GetCriticalChance();
            ArmourPenetration = Weapon.GetArmorPenetration();
            var madnessAbility = new Madness();
            AddAbility(madnessAbility);
            IsAutoAttacker = false;
        }
    }
}