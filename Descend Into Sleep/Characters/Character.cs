using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.Unobtainable;
using ConsoleApp12.Items.ItemTypes;
using ConsoleApp12.Items.Weapons.Unobtainable;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters
{
    public class Character
    {
        protected string Name;
        protected double InnateAttack;
        protected double InnateDefense;
        protected double InnateCriticalChance;
        protected double InnateArmourPenetration;
        protected IWeapon Weapon;
        protected IArmour Armour;
        protected double ArmourPenetration;
        protected double Attack;
        protected double Defense;
        protected double CriticalChance;
        protected double Health;
        protected double MaximumHealth;
        protected string Description;
        protected int Level;
        protected readonly Dictionary<string, Ability.Ability> RespectiveAbilities;
        private int Stunned;
        private readonly List<DotEffect> DotEffects;
        protected double MaxSanity;
        protected double Sanity;
        private bool StunResistant;
        protected double TotalMana;
        protected double Mana;
        protected double ManaRegenerationRate;
        private bool Spared;
        private readonly List<string> Actions;
        private readonly Queue<string> OrderOfActions;
        protected double ChanceOfSuccessfulAct;
        private bool CanLifeSteal;

        protected Character(string name, double innateAttack, double innateDefense, IWeapon weapon, IArmour armour,
            double health, List<string> actions = null, double chanceOfSuccessfulAct = -1, int level = 0,
            string description = null)
        {
            Name = name;
            InnateAttack = innateAttack;
            InnateDefense = innateDefense;
            InnateCriticalChance = 0.15;
            InnateArmourPenetration = 0;
            Health = health;
            MaximumHealth = Health;
            Description = description;
            RespectiveAbilities = new Dictionary<string, Ability.Ability>();
            Stunned = 0;
            DotEffects = new List<DotEffect>();
            Sanity = 100;
            MaxSanity = Sanity;
            StunResistant = false;
            TotalMana = 100;
            Mana = 100;
            ManaRegenerationRate = 0.03125;
            Spared = false;
            Actions = actions;
            OrderOfActions = actions != null ? new Queue<string>(actions) : null;
            var random = new Random();
            if (Actions != null)
                Actions = Actions.OrderBy(element => random.Next()).ToList();
            ChanceOfSuccessfulAct = chanceOfSuccessfulAct;
            CanLifeSteal = true;
            Level = level;
            Weapon = NoWeapon.NO_WEAPON;
            Armour = NoArmour.NO_ARMOUR;
            UseStatStick(weapon);
            UseStatStick(armour);
        }

        private void ChangeStats(double attack, double defense, double health, double armourPenetration,
            double criticalChance, double sanity)
        {
            IncreaseAttackValue(attack);
            IncreaseDefenseValue(defense);
            var supposedHealth = Math.Max(1, MaximumHealth + health);
            SetInnateMaximumHealth(supposedHealth);
            IncreaseArmourPenetration(armourPenetration);
            IncreaseCriticalChance(criticalChance);
            var supposedSanity = Math.Max(1, MaxSanity + sanity);
            SetMaximumSanity(supposedSanity);
        }

        private void UseStatStick(IItem weapon)
        {
            var newStats = ItemHelper.GetItemStats(weapon);
            var attackDifference = newStats.Attack;
            var defenseDifference = newStats.Defense;
            var armourPenetrationDifference = newStats.ArmourPenetration;
            var criticalChanceDifference = newStats.ArmourPenetration;
            var healthDifference = newStats.Health;
            var sanityDifference = newStats.Sanity;
            if (weapon is IWeapon iWeapon)
                Weapon = iWeapon;
            if (weapon is IArmour iArmour)
                Armour = iArmour;
            ChangeStats(attackDifference, defenseDifference, healthDifference, armourPenetrationDifference,
                criticalChanceDifference, sanityDifference);
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
            InnateAttack = newInnateAttackValue;
            Attack += attackDifference;
        }

        public void SetInnateDefense(double newInnateDefenseValue)
        {
            var defenseDifference = newInnateDefenseValue - InnateDefense;
            InnateDefense = newInnateDefenseValue;
            Defense += defenseDifference;
        }

        public void SetInnateMaximumHealth(double newMaximumHealthValue)
        {
            var healthDifference = newMaximumHealthValue - MaximumHealth;
            Health = Math.Max(1, Health + healthDifference);
            // newMaximumHealthValue < 0 => exception
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
            Health = Math.Max(1, Health - healthPoints);
            MaximumHealth = Math.Max(1, MaximumHealth - healthPoints);
        }

        public void SetHealthPoints(double healthPoints)
        {
            Health = healthPoints;
        }

        public virtual void IncreaseAttackValue(double attackValue)
        {
            Attack += attackValue;
        }

        public void IncreaseDefenseValue(double defenseValue)
        {
            Defense += defenseValue;
        }

        protected void IncreaseCriticalChance(double criticalChanceValue)
        {
            CriticalChance += criticalChanceValue;
        }

        public void IncreaseArmourPenetration(double armourPenetrationValue)
        {
            ArmourPenetration += armourPenetrationValue;
        }

        public double GetArmourPenetration()
        {
            return ArmourPenetration;
        }

        public double GetHealthPoints()
        {
            return Health;
        }

        public IArmour GetArmour()
        {
            return Armour;
        }

        public IWeapon GetWeapon()
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

        private double GetMultiplier(Character opponent)
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

        private double CriticalHit(Character opponent)
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

        private string LifeSteal(double damageDealt)
        {
            var lifeStealValue = ItemHelper.GetItemStats(Weapon).LifeSteal;
            var lifeStolen = lifeStealValue * damageDealt;
            Heal(lifeStolen);
            var toStr = $"{Name} has healed for {Math.Round(lifeStolen, 2)}!\n";
            toStr += $"{Name} has {Math.Round(Health, 2)} health now!\n";
            return toStr;
        }

        public virtual string Hit(Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            var opponentArmour = opponent.GetArmour();
            var opponentWeapon = opponent.GetWeapon();
            double dodgeChance = 0;
            if (opponentArmour is IDodge dodge)
                dodgeChance = dodge.GetDodge();
            var dodged = RandomHelper.IsSuccessfulTry(dodgeChance);
            if (dodged)
            {
                return $"{opponent.GetName()} has dodged your attack!\n";
            }

            var criticalStruck = RandomHelper.IsSuccessfulTry(CriticalChance);
            double dealtDamage;

            if (opponentWeapon is IReflector reflectorWeapon && !reflectorWeapon.IsBroken())
                return reflectorWeapon.TakeHit(Attack);
            if (opponentArmour is IReflector reflectorArmour && !reflectorArmour.IsBroken())
                return reflectorArmour.TakeHit(Attack);

            string toStr;
            if (criticalStruck)
            {
                dealtDamage = CriticalHit(opponent);
                var enemyHealthPoints = opponent.GetHealthPoints();
                toStr = $"CRITICAL HIT! {Math.Round(dealtDamage, 2)} damage done to {opponent.GetName()}!\n";
                toStr += $"{opponent.GetName()} is left with {Math.Round(enemyHealthPoints, 2)} health!\n";
            }
            else
            {
                dealtDamage = NormalHit(opponent);
                var enemyHealthPoints = opponent.GetHealthPoints();
                toStr = $"{Math.Round(dealtDamage, 2)} damage done to {opponent.GetName()}!\n";
                toStr += $"{opponent.GetName()} is left with {Math.Round(enemyHealthPoints, 2)} health!\n";
            }

            var regeneratedMana = ManaRegenerationRate * TotalMana;
            GainMana(regeneratedMana);
            toStr += $"{Name} has regenerated {regeneratedMana} of its mana!\n";
            toStr += $"{Name} now has {Math.Round(Mana, 2)} mana!\n";
            toStr += ItemEffects(opponent, listOfTurns, turnCounter, dealtDamage);
            return toStr;
        }

        public string ItemEffects(Character opponent, ListOfTurns listOfTurns,
            int turnCounter, double dealtDamage = 0)
        {
            var toStr = "";
            if (Weapon is IActive activeWeapon && dealtDamage != 0)
                toStr += activeWeapon.Active(dealtDamage, this, opponent);
            if (Weapon is IPassive passiveWeapon && dealtDamage != 0)
                toStr += passiveWeapon.Passive(this, opponent, listOfTurns, turnCounter);
            if (CanLifeSteal && ItemHelper.GetItemStats(Weapon).LifeSteal != 0 && dealtDamage != 0)
                toStr += LifeSteal(dealtDamage);
            if (Armour is IActive activeArmour && dealtDamage != 0)
                toStr += activeArmour.Active(dealtDamage, this, opponent);
            if (Armour is IPassive passiveArmour && dealtDamage != 0)
                toStr += passiveArmour.Passive(this, opponent, listOfTurns, turnCounter);
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

        protected IWeapon ChangeWeapon(IWeapon newWeapon)
        {
            var oldWeapon = Weapon;
            Weapon = newWeapon;
            return oldWeapon;
        }

        protected IArmour ChangeArmour(IArmour newArmour)
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
            var abilityName = ability.GetName();
            RespectiveAbilities[abilityName] = ability;
        }

        public string GetAbilityDescriptionByName(string name)
        {
            return RespectiveAbilities[name].GetDescription();
        }

        public virtual string Cast(string abilityName, Character opponent,
            ListOfTurns listOfTurns, int turnCounter)
        {
            // return RespectiveAbilities[abilityName].Cast(this, opponent, listOfTurns, turnCounter);
            var toStr = RespectiveAbilities[abilityName].Cast(this, opponent, listOfTurns, turnCounter);
            toStr += ItemEffects(opponent, listOfTurns, turnCounter);
            return toStr;
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

        protected void SetMaximumSanity(double newMaximumSanity)
        {
            // new maximum sanity < 0 => exception
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

        public void ClearDotEffects()
        {
            DotEffects.Clear();
        }

        public void SetLifeStealStatus(bool status)
        {
            CanLifeSteal = status;
        }

        public void SetStunResistant(bool truthValue)
        {
            StunResistant = truthValue;
        }

        public void DeleteOptions()
        {
            RespectiveAbilities.Clear();
        }

        public void DirectEquipWeapon(IWeapon newWeapon)
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

        public string GetDescription()
        {
            return Description;
        }

        public double GetTotalMana()
        {
            return TotalMana;
        }

        public double GetManaRegenerationRate()
        {
            return ManaRegenerationRate;
        }

        public void Spare()
        {
            if (!IsSpareable())
            {
                throw new ImpossibleSpareException(Name);
            }

            Spared = true;
        }

        public bool IsSpared()
        {
            return Spared;
        }

        public bool IsSpareable()
        {
            return OrderOfActions.Count == 0 && Actions.Count != 0;
        }

        public List<string> GetActions()
        {
            return Actions;
        }

        public string Act(string action)
        {
            var toStr = $"You {action} {Name}!\n";
            var successfulAct = RandomHelper.IsSuccessfulTry(ChanceOfSuccessfulAct);
            if (!successfulAct)
                return "Action failed!\n";
            if (OrderOfActions.Count == 0 || action == OrderOfActions.Peek())
            {
                toStr += "It is very effective!\n";
                if (OrderOfActions.Count != 0)
                {
                    OrderOfActions.Dequeue();

                    int actsLeft = OrderOfActions.Count, totalActs = Actions.Count;
                    var unbuffedAttack =
                        InnateAttack + Weapon.GetAttackValue() + ItemHelper.GetItemStats(Armour).Attack;
                    var lostAttack = FormulaHelper.GetAttackValueDifference(actsLeft, totalActs, unbuffedAttack);
                    toStr += $"{Name} has lost {Math.Round(lostAttack, 2)} of its Attack!\n";
                    InnateAttack -= lostAttack;
                    Attack -= lostAttack;
                }
            }
            else
            {
                toStr += "It does not seem to be effective!\n";
            }

            return toStr;
        }

        private string GetStatus()
        {
            if (Actions.Count == 0)
                return "Not Spareable\n";
            if (OrderOfActions.Count == 0)
                return "Spareable\n";
            var desiredAction = OrderOfActions.Peek();
            return $"You should {desiredAction} them\n";
        }

        public virtual double GetOddsOfAttacking()
        {
            return 0.8;
        }

        public override string ToString()
        {
            return $"{Name}: {Health} HEALTH, {Defense} DEFENSE, {Attack} ATTACK" +
                   $"\n{Description}\n{ItemHelper.ItemToString(Weapon)}{ItemHelper.ItemToString(Armour)}";
        }
    }
}