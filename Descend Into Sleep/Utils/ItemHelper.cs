using ConsoleApp12.Items;
using Microsoft.VisualBasic.CompilerServices;

namespace ConsoleApp12.Utils
{
    public static class ItemHelper
    {
        public static StatHelper GetItemStats(IItem item)
        {
            double 
                attack = 0,
                health = 0,
                armourPenetration = 0,
                criticalChance = 0,
                defense = 0,
                sanity = 0;
            if (item is IAttack iAttack)
                attack = iAttack.GetAttackValue();
            if (item is IArmourPenetration iArmourPenetration)
                armourPenetration = iArmourPenetration.GetArmorPenetration();
            if (item is ICriticalChance iCriticalChance)
                criticalChance = iCriticalChance.GetCriticalChance();
            if (item is IDefense iDefense)
                defense = iDefense.GetDefenseValue();
            if (item is ISanity iSanity)
                sanity = iSanity.GetSanity();
            if (item is IHealth iHealth)
                health = iHealth.GetHealth();
            return new StatHelper(attack, defense, health, armourPenetration, criticalChance, sanity);
        }

        public static string ItemToString(IItem item)
        {
            string toStr = item.GetName() + "\n";
            if (item is IArmour)
                toStr += "ARMOUR:";
            if (item is IWeapon)
                toStr += "WEAPON:";
            if (item is IPotion)
                toStr += "POTION:";
            StatHelper stats = GetItemStats(item);
            if (stats.Defense != 0)
                toStr += stats.Defense + " DEFENSE;";
            if (stats.Attack != 0)
                toStr += stats.Attack + " ATTACK;";
            if (stats.Health != 0)
                toStr += stats.Health + " HEALTH;";
            if (stats.ArmourPenetration != 0)
                toStr += (stats.ArmourPenetration * 100) + " ARMOUR PENETRATION;";
            if (stats.CriticalChance != 0)
                toStr += (stats.CriticalChance * 100) + " CRITICAL CHANCE;";
            if (stats.LifeSteal != 0)
                toStr += (stats.LifeSteal * 100) + " LIFE STEAL;";
            if (stats.Sanity != 0)
                toStr += stats.Sanity + " SANITY;";
            if (item is IDodge dodge)
                toStr += (dodge.GetDodge() * 100) + " DODGE;";
            toStr += item.GetDescription() + "\n";
            return toStr;
        }
    }
}