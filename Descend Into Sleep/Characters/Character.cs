using System;
using System.Collections.Generic;
using System.ComponentModel;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters
{
    public class Character
    {
        protected string Name;
        protected double InnateAttack;
        protected double InnateDefense;
        protected double InnateCriticalChance;
        protected double InnateArmourPenetration;
        protected Weapon Weapon;
        protected Armour Armour;
        protected double ArmourPenetration;
        protected double Attack;
        protected double Defense;
        protected double CriticalChance;
        protected double Health;
        protected double MaximumHealth;
        protected string Description;
        protected int Level;
        protected Dictionary<string, Ability.Ability> RespectiveAbilities;
        protected int Stunned;
        protected List<DotEffect> DotEffects;
        protected double MaxSanity;
        protected double Sanity;
        protected bool IsAutoAttacker;
        protected bool StunResistant;
        protected double TotalMana;
        protected double Mana;
        protected double ManaRegenerationRate;

        public Character(string name, double innateAttack, double innateDefense, Weapon weapon, Armour armour,
            double health, string description = null)
        {
            Name = name;
            InnateAttack = innateAttack;
            InnateDefense = innateDefense;
            InnateCriticalChance = 0.15;
            Weapon = weapon;
            Armour = armour;
            InnateArmourPenetration = 0;
            ArmourPenetration = Weapon.GetArmorPenetration();
            Attack = InnateAttack + Weapon.GetAttackValue() + Armour.GetAttackValue();
            Defense = InnateDefense + Weapon.GetDefenseValue() + Armour.GetDefenseValue();
            CriticalChance = InnateCriticalChance + Weapon.GetCriticalChance();
            Health = health;
            MaximumHealth = Health;
            Description = description;
            RespectiveAbilities = new Dictionary<string, Ability.Ability>();
            Stunned = 0;
            DotEffects = new List<DotEffect>();
            Sanity = 100;
            MaxSanity = Sanity;
            IsAutoAttacker = true;
            StunResistant = false;
            TotalMana = 100;
            Mana = 100;
            ManaRegenerationRate = 0.03125;
        }

        public int GetLevel()
        {
            return Level;
        }
        public string GetName()
        {
            return Name;
        }
        
        public void SetAttackValue(double newAttackValue)
        {
            Attack = newAttackValue;
        }

        public double GetInnateAttack()
        {
            return InnateAttack;
        }

        public double GetInnateDefense()
        {
            return InnateDefense;
        }

        public void SetInnateAttack(double newInnateAttackValue)
        {
            var attackDifference = newInnateAttackValue - InnateAttack;
            InnateAttack += attackDifference;
            Attack += attackDifference;
        }

        public void SetInnateDefense(double newInnateDefenseValue)
        {
            var defenseDifference = newInnateDefenseValue - InnateDefense;
            InnateDefense += defenseDifference;
            Defense += defenseDifference;
        }

        public void SetInnateMaximumHealth(double newMaximumHealthValue)
        {
            var healthDifference = newMaximumHealthValue - MaximumHealth;
            Health = Math.Max(1, Health + healthDifference);
            MaximumHealth = newMaximumHealthValue;
            Health = Math.Min(Health, MaximumHealth);
        }

        public void GainHealthPoints(double healthPoints)
        {
            Health += healthPoints;
            MaximumHealth += healthPoints;
        }

        public void LoseHealthPoints(double healthPoints)
        {
            Health -= healthPoints;
            MaximumHealth -= healthPoints;
        }

        public void SetHealthPoints(double healthPoints)
        {
            Health = healthPoints;
        }

        public void IncreaseAttackValue(double attackValue)
        {
            Attack += attackValue;
        }

        public virtual void DecreaseAttackValue(double attackValue)
        {
            Attack -= attackValue;
        }

        public void IncreaseDefenseValue(double defenseValue)
        {
            Defense += defenseValue;
        }

        public void DecreaseDefenseValue(double defenseValue)
        {
            Defense -= defenseValue;
        }

        public void IncreaseCriticalChance(double criticalChanceValue)
        {
            CriticalChance += criticalChanceValue;
        }

        public void IncreaseArmourPenetration(double armourPenetrationValue)
        {
            ArmourPenetration += armourPenetrationValue;
        }

        public void DecreaseArmourPenetration(double armourPenetrationValue)
        {
            ArmourPenetration -= armourPenetrationValue;
        }

        public double GetArmourPenetration()
        {
            return ArmourPenetration;
        }

        public void SetArmourPenetration(double newArmourPenetrationValue)
        {
            ArmourPenetration = newArmourPenetrationValue;
        }

        public double GetHealthPoints()
        {
            return Health;
        }

        public Armour GetArmour()
        {
            return Armour;
        }

        public Weapon GetWeapon()
        {
            return Weapon;
        }

        public double GetAttackValue()
        {
            return Attack;
        }
        
        public double GetDefenseValue()
        {
            return Defense;
        }

        public double GetMaximumHealthPoints()
        {
            return MaximumHealth;
        }
        
        public void ReduceHealthPoints(double damageTaken)
        {
            Health -= damageTaken;
        }

        public void GainMana(double manaGained)
        {
            Mana = Math.Min(Mana + manaGained, TotalMana);
        }

        public void PermanentlyReduceHealthPoints(double reducedHealthPoints)
        {
            MaximumHealth -= reducedHealthPoints;
            Health -= reducedHealthPoints;
        }

        public double GetMultiplier(Character opponent)
        {
            double defensePoints = opponent.GetDefenseValue();
            double multiplier;
            if (defensePoints >= 0)
            {
                var countedArmour = defensePoints * (1 - ArmourPenetration);
                multiplier = 100 / (100 + countedArmour);
            }
            else
            {
                multiplier = 2 - 100 / (100 - defensePoints);
            }
            return multiplier;
        }

        private double NormalHit(Character opponent)
        {
            var multiplier = GetMultiplier(opponent);
            double damageDealt;
            if (Attack >= 0)
                damageDealt = Attack;
            else
                damageDealt = 0;
            damageDealt *= multiplier;
            opponent.ReduceHealthPoints(damageDealt);
            return damageDealt;
        }

        public double CriticalHit(Character opponent)
        {
            var multiplier = GetMultiplier(opponent);
            double damageDealt;
            if (Attack >= 0)
                damageDealt = Attack;
            else
                damageDealt = 0;
            damageDealt *= multiplier;
            damageDealt *= 2;
            opponent.ReduceHealthPoints(damageDealt);
            return damageDealt;
        }

        public void DealDirectDamage(Character opponent, double damage)
        {
            opponent.ReduceHealthPoints(damage);
        }

        public string LifeSteal(double damageDealt)
        {
            var lifeStealValue = Weapon.GetLifeSteal();
            var lifeStolen = lifeStealValue * damageDealt;
            Heal(lifeStolen);
            var toStr = Name + " has healed for " + Math.Round(lifeStolen, 2) + "!\n";
            toStr += Name + " has " + Math.Round(Health, 2) + " health now!\n";
            return toStr;
        }

        public virtual string Hit(Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var randomObject = new Random();
            var opponentArmour = opponent.GetArmour();
            var opponentWeapon = opponent.GetWeapon();
            var dodgeChance = opponentArmour.GetDodge();
            var dodgeOdds = Convert.ToInt32(dodgeChance * 100);
            var randomChoice = randomObject.Next(0, 100);
            var criticalChoice = randomObject.Next(0, 100);
            var toStr = "";
            double dealtDamage = 0;
            if (dodgeChance != 0 && randomChoice <= dodgeOdds)
            {
                toStr = opponent.GetName() + " has dodged your attack!\n";
                return toStr;
            }

            if (opponentWeapon.IsReflector() && !opponentWeapon.IsBroken())
                toStr = opponentWeapon.TakeHit(Attack);
            else
            {
                if (opponentArmour.IsReflector() && !opponentArmour.IsReflector())
                    toStr = opponentArmour.TakeHit(Attack);
                else
                {
                    if (criticalChoice <= CriticalChance * 100)
                    {
                        dealtDamage = CriticalHit(opponent);
                        var enemyHealthPoints = opponent.GetHealthPoints();
                        toStr = "CRITICAL HIT! " + Math.Round(dealtDamage, 2) + " damage done to " + opponent.GetName() +
                                "!\n";
                        toStr += opponent.GetName() + " is left with " + Math.Round(enemyHealthPoints, 2) + " health!\n";
                    }
                    else
                    {
                        dealtDamage = NormalHit(opponent);
                        var enemyHealthPoints = opponent.GetHealthPoints();
                        toStr = Math.Round(dealtDamage, 2) + " damage done to " + opponent.GetName() + "!\n";
                        toStr += opponent.GetName() + " is left with " + Math.Round(enemyHealthPoints, 2) +
                                " health!\n";
                    }
                }
            }
            var regeneratedMana = ManaRegenerationRate * TotalMana;
            GainMana(regeneratedMana);
            toStr += Name + " has regenerated " + regeneratedMana + " of his mana!\n";
            toStr += Name + " now has " + Math.Round(Mana, 2) + " mana!\n";
            if (Weapon.HasEffect())
                toStr += Weapon.Effect(dealtDamage, this, opponent);
            if (Weapon.GetLifeSteal() != 0)
                toStr += LifeSteal(dealtDamage);
            if (Armour.HasEffect())
                toStr += Armour.Effect(dealtDamage, this, opponent);
            if (Weapon.HasPassive())
                toStr += Weapon.Passive(this, opponent, listOfTurns, turnCounter);
            if (Armour.HasPassive())
                toStr += Armour.Passive(this, opponent, listOfTurns, turnCounter);
            return toStr;
        }

        public double TakeMitigatedDamage(double mitigatedDamage)
        {
            var defensePoints = Defense;
            double multiplier;
            if (defensePoints >= 0)
                multiplier = 100 / (100 + defensePoints);
            else
                multiplier = 2 - 100 / (100 - defensePoints);
            var takenDamage = multiplier * mitigatedDamage;
            Health -= takenDamage;
            return takenDamage;
        }

        public Weapon ChangeWeapon(Weapon newWeapon)
        {
            var oldWeapon = Weapon;
            Weapon = newWeapon;
            return oldWeapon;
        }

        public Armour ChangeArmour(Armour newArmour)
        {
            var oldArmour = Armour;
            Armour = newArmour;
            return oldArmour;
        }

        public void Heal(double amountHealed)
        {
            Health = Math.Min(Health + amountHealed, MaximumHealth);
        }
        
        protected void AddAbility(Ability.Ability ability)
        {
            // var abilityName = ability.GetType().ToString().ToLower();
            var abilityName = ability.GetName();
            RespectiveAbilities[abilityName] = ability;
        }

        public virtual string Cast(string abilityName, Character opponent,
            Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            return RespectiveAbilities[abilityName].Cast(this, opponent, listOfTurns, turnCounter);
        }

        public void Stun()
        {
            if (StunResistant)
                throw new StunException(Name);
            Stunned++;
        }

        public bool IsStunned()
        {
            return Stunned != 0;
        }
        
        public void Unstun()
        {
            Stunned--;
        }

        public void AddDotEffect(DotEffect dotEffect)
        {
            DotEffects.Add(dotEffect);
        }

        public void DecreaseDotEffect(int index)
        {
            DotEffects[0].NumberOfTurns--;
            if (DotEffects[index].NumberOfTurns == 0)
                DotEffects.Remove(DotEffects[index]);
        }

        public List<DotEffect> GetDotEffects()
        {
            return DotEffects;
        }

        public void DecreaseDotEffects(int turnsDecreased)
        {
            foreach (var dotEffect in DotEffects)
            {
                dotEffect.NumberOfTurns = Math.Max(1, dotEffect.NumberOfTurns - turnsDecreased);
            }
        }

        public void ReduceSanity(double sanityValue)
        {
            Sanity -= sanityValue;
        }

        public double GetSanity()
        {
            return Sanity;
        }

        public void SetSanity(double newSanity)
        {
            Sanity = newSanity; 
        }

        public void SetMaximumSanity(double newMaximumSanity)
        {
            var sanityDifference = newMaximumSanity - MaxSanity;
            MaxSanity = newMaximumSanity;
            Sanity = Math.Max(1, Sanity + sanityDifference);
            Sanity = Math.Min(Sanity, MaxSanity);
        }

        public double GetMaximumSanity()
        {
            return MaxSanity;
        }
        
        public void RestoreSanity(double sanityValue)
        {
            Sanity = Math.Min(Sanity + sanityValue, MaxSanity);
        }

        public bool AutoAttacker()
        {
            return IsAutoAttacker;
        }

        public void ClearDotEffects()
        {
            DotEffects.Clear();
        }

        public void SetStunResistant(bool truthValue)
        {
            StunResistant = truthValue;
        }

        public void DeleteOptions()
        {
            RespectiveAbilities.Clear();
        }

        public void DirectEquipWeapon(Weapon newWeapon)
        {
            Weapon = newWeapon;
        }

        public Dictionary<string, Ability.Ability> GetRespectiveAbilities()
        {
            return RespectiveAbilities;
        }

        public double GetMana()
        {
            return Mana;
        }

        public void SetMana(double newMana)
        {
            Mana = newMana;
        }
        
        public string GetDescription()
        {
            return Description;
        }

        public double GetTotalMana()
        {
            return TotalMana;
        }

        public void SetTotalMana(double newTotalMana)
        {
            TotalMana = newTotalMana;
        }

        public double GetManaRegenerationRate()
        {
            return ManaRegenerationRate;
        }

        public void SetManaRegenerationRate(double newManaRegenerationRate)
        {
            ManaRegenerationRate = newManaRegenerationRate;
        }

        public override string ToString()
        {
            var toStr = Name + ": " + Health + " HEALTH, ";
            toStr += Defense + " DEFENSE, " + Attack + " ATTACK";
            if (Description != null)
                toStr += "\n" + Description;
            toStr += "\n" + Weapon;
            toStr += Armour;
            return toStr;
        }
    }
}