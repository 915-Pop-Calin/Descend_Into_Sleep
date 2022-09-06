using System;
using System.Collections.Generic;
using ConsoleApp12.Ability.YoggSaronAbilities;
using ConsoleApp12.Items.Armours.LevelThree;
using ConsoleApp12.Items.Weapons.LevelThree;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class YoggSaron : Character
    {
        private int DiscourageCounter;

        private YoggSaron() : base("YoggSaron", int.MaxValue, int.MaxValue, BoilingBlood.BOILING_BLOOD,
            BootsOfDodge.BOOTS_OF_DODGE,
            int.MaxValue, new List<string> {"beg for mercy", "pray", "worship"}, 0, 3, "The God Of Death")
        {
            DiscourageCounter = 3;
        }

        public override void IncreaseAttackValue(double attackValue)
        {
            if (attackValue > 0)
            {
                base.IncreaseAttackValue(attackValue);
                return;
            }

            if (attackValue + Attack < 0.0001)
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
                Attack += attackValue;
            }
        }

        private void FormChange()
        {
            StatHelper weaponStats = ItemHelper.GetItemStats(Weapon),
                armourStats = ItemHelper.GetItemStats(Armour);
            Console.WriteLine("Avatar of Yogg Saron appears!\n");
            ChanceOfSuccessfulAct = 0.6;
            Name = "Avatar of Yogg Saron";
            InnateAttack = 10;
            InnateDefense = 100;
            Health = 100;
            MaximumHealth = 100;
            Description = "A manifestation of the God of Death.\n";
            Attack = InnateAttack + Weapon.GetAttackValue() + armourStats.Attack;
            Defense = InnateDefense + weaponStats.Defense + Armour.GetDefenseValue();
            CriticalChance = InnateCriticalChance + weaponStats.CriticalChance;
            ArmourPenetration = weaponStats.ArmourPenetration;
            AddAbility(new Madness());
        }

        public override double GetOddsOfAttacking()
        {
            if (Name == "YoggSaron")
                return 1;
            return 0;
        }

        public static readonly YoggSaron YOGG_SARON = new YoggSaron();
    }
}