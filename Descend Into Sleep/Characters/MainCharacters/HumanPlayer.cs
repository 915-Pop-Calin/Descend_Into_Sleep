using System;
using System.Collections.Generic;
using ConsoleApp12.Ability.HumanAbilities.FireAbilities;
using ConsoleApp12.Ability.HumanAbilities.NatureAbilities;
using ConsoleApp12.Ability.HumanAbilities.NeutralAbilities;
using ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class HumanPlayer: Character
    {
        private double ExperiencePoints;
        private readonly List<IItem> Inventory;
        private Dictionary<int, KeyValuePair<Ability.Ability, int>> AbilitiesToLearn;
        private double Gold;
        private bool Cheater;
        private readonly List<PastSelf> PastSelves;
        private string School;
        private string Difficulty;
        private double AttackGrowth;
        private double DefenseGrowth;
        private double HealthGrowth;
        private double ManaGrowth;
        private double KillCount;

        public HumanPlayer(string name, string difficulty, IWeapon weapon, IArmour armour) : 
            base(name, 10, 0, weapon, armour, 20)
        {
            Difficulty = difficulty;
            SetDifficulty();
            SetInitialAbilities();
            ExperiencePoints = 0;
            Inventory = new List<IItem>()
            {
                null, null, null, null, null, null, null, null
            };

            // 30 Gold => game should be beatable on pacifist
            Gold = 30;
            Cheater = false;
            PastSelves = new List<PastSelf>();
            School = null;
            KillCount = 0;
        }

        public HumanPlayer(String name, int humanLevel, double experiencePoints, double innateAttack,
            double innateDefense, double innateCriticalChance, double innateArmourPenetration,
            double manaRegenerationRate, double mana, double health, double maximumHealth,
            double totalMana, List<IItem> inventory, double gold, string school, double sanity,
            double maxSanity, double killCount, string difficulty, IWeapon weapon, IArmour armour, 
            List<PastSelf> pastSelves): base(name, innateAttack, innateDefense, weapon, armour, maximumHealth)
        {
            School = school;
            Difficulty = difficulty;
            SetDifficulty();
            SetInitialAbilities();
            ExperiencePoints = experiencePoints;
            InnateCriticalChance = innateCriticalChance;
            InnateArmourPenetration = innateArmourPenetration;
            ManaRegenerationRate = manaRegenerationRate;
            Mana = mana;
            Health = health;
            TotalMana = totalMana;
            Inventory = inventory;
            Gold = gold;
            Sanity = sanity;
            MaxSanity = maxSanity;
            KillCount = killCount;
            PastSelves = pastSelves;
            Cheater = false;
            JumpToGivenLevel(humanLevel, School);
            Armour = AllItems.NoArmour;
            Weapon = AllItems.NoWeapon;
            UseArmour(armour, 0);
            UseWeapon(weapon, 0);
        }

        private void SetDifficulty()
        {
            switch (Difficulty)
            {
                case "impossible":
                    AttackGrowth = 0;
                    DefenseGrowth = 0;
                    HealthGrowth = 0;
                    ManaGrowth = 0;
                    break;
                case "hard":
                    AttackGrowth = 0.2;
                    DefenseGrowth = 2;
                    HealthGrowth = 1;
                    ManaGrowth = 1;
                    break;
                case "medium":
                    AttackGrowth = 0.6;
                    DefenseGrowth = 2;
                    HealthGrowth = 3;
                    ManaGrowth = 2;
                    break;
                default:
                    AttackGrowth = 1;
                    DefenseGrowth = 3;
                    HealthGrowth = 5;
                    ManaGrowth = 4;
                    Difficulty = "easy";
                    break;
            }
        }

        private void SetInitialAbilities()
        {
            AbilitiesToLearn = new Dictionary<int, KeyValuePair<Ability.Ability, int>>();
            AbilitiesToLearn[1] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 1);
            AbilitiesToLearn[2] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 1);
            AbilitiesToLearn[3] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 1);
            AbilitiesToLearn[5] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 2);
            AbilitiesToLearn[6] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 2);
            AbilitiesToLearn[7] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 2);
            AbilitiesToLearn[9] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 3);
            AbilitiesToLearn[11] = new KeyValuePair<Ability.Ability, int>(new Focus(), 1);
            AbilitiesToLearn[12] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 3);
            AbilitiesToLearn[14] = new KeyValuePair<Ability.Ability, int>(new Discourage(), 1);
            AbilitiesToLearn[19] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 3);
            AbilitiesToLearn[21] = new KeyValuePair<Ability.Ability, int>(new Focus(), 2);
            AbilitiesToLearn[24] = new KeyValuePair<Ability.Ability, int>(new CleanseDOT(), 1);
            AbilitiesToLearn[29] = new KeyValuePair<Ability.Ability, int>(new TrueDamage(), 1);
            AbilitiesToLearn[34] = new KeyValuePair<Ability.Ability, int>(new CCImmunity(), 1);
        }
        
        private int FindEmptyPositionInInventory()
        {
            for (int index = 0; index < Inventory.Count; index++)
                if (Inventory[index] == null)
                    return index;
            return -1;
        }

        // one use, might as well just return the IDs for safety purposes
        public List<IItem> GetInventory()
        {
            return Inventory;
        }

        public void PickUp(IItem item)
        {
            int firstNonEmpty = FindEmptyPositionInInventory();
            if (firstNonEmpty == -1)
                throw new FullInventoryException();
            Inventory[firstNonEmpty] = item;
        }

        private int FindItemByName(string itemName)
        {
            for (int index = 0; index < Inventory.Count; index++)
                if (Inventory[index] != null && Inventory[index].GetName().ToLower() == itemName.ToLower().Trim())
                    return index;
            return -1;
        }

        public string DropItem(int position)
        {
            if (position < 0 || position >= 8)
                throw new InventoryOutOfBoundsException();
            if (Inventory[position] == null)
                throw new NullItemException();
            var toStr = $"{Inventory[position].GetName()} has been dropped!\n";
            Inventory[position] = null;
            return toStr;
        }

        public void MoveWeaponToInventory()
        {
            int firstEmpty = FindEmptyPositionInInventory();
            if (firstEmpty == -1)
                throw new FullInventoryException();
            if (Weapon.GetName() == "No Weapon")
                throw new InvalidItemDropException("weapon");
            Inventory[firstEmpty] = Weapon;
            UseWeapon(AllItems.NoWeapon, firstEmpty);
        }

        public void MoveArmourToInventory()
        {
            int firstEmpty = FindEmptyPositionInInventory();
            if (firstEmpty == -1)
                throw new FullInventoryException();
            if (Armour.GetName() == "No Armour")
                throw new InvalidItemDropException("armour");
            UseArmour(AllItems.NoArmour, firstEmpty);
        }

        private string UseWeapon(IWeapon weapon, int itemIndex)
        {
            var toStr = $"You have equipped {weapon.GetName()}!\n";
            var oldWeapon = ChangeWeapon(weapon);
            if (oldWeapon != AllItems.NoWeapon)
                Inventory[itemIndex] = oldWeapon;
            else
                Inventory[itemIndex] = null;
            var oldStats = ItemHelper.GetItemStats(oldWeapon);
            var newStats = ItemHelper.GetItemStats(weapon);
            var attackDifference = newStats.Attack - oldStats.Attack;
            var defenseDifference = newStats.Defense - oldStats.Defense;
            var armourPenetrationDifference = newStats.ArmourPenetration - oldStats.ArmourPenetration;
            var criticalChanceDifference = newStats.ArmourPenetration - oldStats.ArmourPenetration;
            var healthDifference = newStats.Health - oldStats.Health;
            var sanityDifference = newStats.Sanity - oldStats.Sanity;
            
            ChangeStats(attackDifference, defenseDifference, healthDifference, armourPenetrationDifference,
                    criticalChanceDifference, sanityDifference);
            return toStr;
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
        
        
        private string UseArmour(IArmour armour, int itemIndex)
        {
            var toStr = $"You have equipped {armour.GetName()}!\n";
            var oldArmour = ChangeArmour(armour);
            if (oldArmour != AllItems.NoArmour)
                Inventory[itemIndex] = oldArmour;
            else
                Inventory[itemIndex] = null;

            var oldStats = ItemHelper.GetItemStats(oldArmour);
            var newStats = ItemHelper.GetItemStats(armour);
            var attackDifference = newStats.Attack - oldStats.Attack;
            var defenseDifference = newStats.Defense - oldStats.Defense;
            var armourPenetrationDifference = newStats.ArmourPenetration - oldStats.ArmourPenetration;
            var criticalChanceDifference = newStats.ArmourPenetration - oldStats.ArmourPenetration;
            var healthDifference = newStats.Health - oldStats.Health;
            var sanityDifference = newStats.Sanity - oldStats.Sanity;
            
            ChangeStats(attackDifference, defenseDifference, healthDifference, armourPenetrationDifference,
                criticalChanceDifference, sanityDifference);
            return toStr;
        }

        private string UsePotion(IPotion potion, int itemIndex)
        {
            var toStr = $"You have consumed a {potion.GetName()}!\n";
            Inventory[itemIndex] = null;
            toStr += potion.UseItem(this);
            return toStr;

        }

        public string EquipItem(int position)
        {
            if (position < 0 || position >= 8)
                throw new InventoryOutOfBoundsException();
            var item = Inventory[position];
            if (item == null)
                throw new NullItemException();
            if (item is IWeapon weapon)
                return UseWeapon(weapon, position);
            if (item is IArmour armour)
                return UseArmour(armour, position);
            if (item is IPotion potion)
                return UsePotion(potion, position);
            throw new InvalidItemTypeException();
        }

        public string[] GetInventoryItems()
        {
            var items = new string[9];
            var currentPosition = 0;
            foreach (var item in Inventory)
            {
                if (item == null)
                    items[currentPosition] = "Empty Position";
                else
                    items[currentPosition] = item.GetName();
                currentPosition++;
            }
            items[8] = "back";
            return items;
        }
        
        public bool GainExperience(double experiencePoints)
        {
            ExperiencePoints += experiencePoints;
            if (Level == 34)
                return false;
            var experienceNeeded = 25 * (Level + 1) * (Level + 2);
            bool leveledUp = false;
            while (ExperiencePoints >= experienceNeeded)
            {
                LevelUp();
                leveledUp = true;
                experienceNeeded = 25 * (Level + 1) * (Level + 2);
            }
            return leveledUp;
        }

        private void LevelUp(string alreadyChosenSchool = null)
        {
            Level += 1;
            if (alreadyChosenSchool is null){
                SetInnateDefense(InnateDefense + DefenseGrowth);
                SetInnateAttack(InnateAttack + AttackGrowth);
                SetInnateMaximumHealth(MaximumHealth + HealthGrowth);
                Mana += ManaGrowth;
                TotalMana += ManaGrowth;
                switch (Level)
                {
                    case 2:
                        PastSelves.Add(CreateCharacterCopy($"Young {Name}", "Does not want to hurt you", 1));
                        break;
                    case 15:
                        PastSelves.Add(CreateCharacterCopy($"Teen {Name}", "Might want to hurt you", 2));
                        break;
                    case 30:
                        PastSelves.Add(CreateCharacterCopy($"Adult {Name}", "Wants to MURDER you", 3));
                        break;
                }
            }
            
            if (Level == 4)
                ChooseSchool(alreadyChosenSchool);
            
            var isVerbose = alreadyChosenSchool is null;
            NewAbility(isVerbose);
            
        }

        public List<PastSelf> GetPastSelves()
        {
            return PastSelves;
        }

        private void NewAbility(bool verbose = true)
        {
            var abilityInfo = AbilitiesToLearn[Level];
            var abilityToLearn = abilityInfo.Key;
            var abilityLevel = abilityInfo.Value;
            var toStr = abilityLevel == 1 ? LearnAbility(abilityToLearn) : LevelUpAbility(abilityToLearn);
            if (verbose)
                Console.WriteLine(toStr);
        }

        // public void AddLifeStealToWeapon(double lifeStealAdded)
        // {
        //     var currentLifeSteal = Weapon.GetLifeSteal();
        //     currentLifeSteal += lifeStealAdded;
        //     Weapon.SetLifeSteal(currentLifeSteal);
        // }
        
        public string ShowInventory()
        {
            if (IsInventoryEmpty())
                return "Inventory is Empty!\n";
            var toStr = "";
            foreach (var item in Inventory)
            {
                if (item != null)
                    toStr += ItemHelper.ItemToString(item);
            }
            return toStr;
        }

        private string LearnAbility(Ability.Ability abilityToLearn)
        {
            var abilityName = abilityToLearn.GetName();
            RespectiveAbilities[abilityName] = abilityToLearn;
            var toStr = $"{abilityName} has been learnt!";
            return toStr;
        }

        public string GetAbilitiesDescription()
        {
            var toStr = "";
            foreach (var ability in RespectiveAbilities)
            {
                toStr += $"{ability.Value.GetName()}:{ability.Value.GetDescription()}\n";
            }
            return toStr;
        }

        public void GainGold(double amountGained)
        {
            Gold += amountGained;
        }

        public double GetGold()
        {
            return Gold;
        }

        
        public void BuyItem(IObtainable item)
        {
            var cost = item.GetPrice();
            if (Gold < cost)
                throw new InsufficientGoldException(item.GetName());
            PickUp(item);
            Gold -= cost;
        }
        
        public string SellItem(int position)
        {
            if (Inventory[position] == null)
                throw new NullItemException();
            if (Inventory[position] is IObtainable obtainable)
                Gold += obtainable.GetPrice();
            else
                Gold += 0;
            var itemName = Inventory[position].GetName();
            Inventory[position] = null;
            return $"You have sold {itemName}!\n";
        }
        
        public void DropInventory()
        {
            for (int i = 0; i < 8; i++)
                Inventory[i] = null;
        }

        public bool IsInventoryEmpty()
        {
            foreach (var item in Inventory)
                if (item != null)
                    return false;
            return true;
        }
        
        public override string Cast(string abilityName, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var abilityManaCost = RespectiveAbilities[abilityName].GetManaCost();
            if (Mana < abilityManaCost)
                throw new InsufficientManaException(Mana, abilityManaCost, Name, abilityName);
            Mana -= abilityManaCost;
            var toStr = RespectiveAbilities[abilityName].Cast(this, opponent, listOfTurns, turnCounter);
            toStr += ItemEffects(opponent, listOfTurns, turnCounter);
            return toStr;
        }

        private PastSelf CreateCharacterCopy(string copyName, string copyDescription, int level)
        {
            var chara = new PastSelf(copyName, InnateAttack, InnateDefense, Weapon, Armour, MaximumHealth, copyDescription, level);
            return chara;
        }

        public bool IsCheater()
        {
            return Cheater;
        }

        public void SetCheater()
        {
            Cheater = true;
        }

        private string LevelUpAbility(Ability.Ability ability)
        {
            var abilityName = ability.GetName();
            RespectiveAbilities[abilityName].LevelUp();
            var toStr = $"{abilityName} has been leveled up to level {RespectiveAbilities[abilityName].GetLevel()}!\n";
            return toStr;
        }

        private void ChooseSchool(string alreadyChosenSchool = null)
        {
            String schoolChoice;
            if (alreadyChosenSchool is not null)
                schoolChoice = alreadyChosenSchool;
            
            else
            {
                var schools = new String[3] {"fire", "self-harm", "nature"};
                const string question = "Choose a school to be a part of";
                    var choice = Utils.keysWork.ConsoleHelper.MultipleChoice(20, question, "fire", "self-harm", "nature");
                    schoolChoice = schools[choice];
            }

            switch (schoolChoice)
                {
                    case "fire":
                        SetUpFire();
                        School = "fire";
                        break;
                    case "self-harm":
                        SetUpSelfHarm();
                        School = "self-harm";
                        break;
                    case "nature":
                        SetUpNature();
                        School = "nature";
                        break;
                }
                
            }

        private void SetUpSchool(List<Ability.Ability> schoolAbilities)
        {
            var firstAbility = schoolAbilities[0];
            var secondAbility = schoolAbilities[1];
            var thirdAbility = schoolAbilities[2];
            var fourthAbility = schoolAbilities[3];
            var ultimate = schoolAbilities[4];

            AbilitiesToLearn[4] = new KeyValuePair<Ability.Ability, int>(firstAbility, 1);
            AbilitiesToLearn[8] = new KeyValuePair<Ability.Ability, int>(firstAbility, 2);
            AbilitiesToLearn[10] = new KeyValuePair<Ability.Ability, int>(secondAbility, 1);
            AbilitiesToLearn[13] = new KeyValuePair<Ability.Ability, int>(firstAbility, 3);
            AbilitiesToLearn[15] = new KeyValuePair<Ability.Ability, int>(thirdAbility, 1);
            AbilitiesToLearn[16] = new KeyValuePair<Ability.Ability, int>(firstAbility, 4);
            AbilitiesToLearn[17] = new KeyValuePair<Ability.Ability, int>(secondAbility, 2);
            AbilitiesToLearn[18] = new KeyValuePair<Ability.Ability, int>(thirdAbility, 2);
            AbilitiesToLearn[20] = new KeyValuePair<Ability.Ability, int>(fourthAbility, 1);
            AbilitiesToLearn[22] = new KeyValuePair<Ability.Ability, int>(secondAbility, 3);
            AbilitiesToLearn[23] = new KeyValuePair<Ability.Ability, int>(secondAbility, 4);
            AbilitiesToLearn[25] = new KeyValuePair<Ability.Ability, int>(thirdAbility, 3);
            AbilitiesToLearn[26] = new KeyValuePair<Ability.Ability, int>(ultimate, 1);
            AbilitiesToLearn[27] = new KeyValuePair<Ability.Ability, int>(fourthAbility, 2);
            AbilitiesToLearn[28] = new KeyValuePair<Ability.Ability, int>(ultimate, 2);
            AbilitiesToLearn[30]= new KeyValuePair<Ability.Ability, int>(fourthAbility, 3);
            AbilitiesToLearn[31] = new KeyValuePair<Ability.Ability, int>(ultimate, 3);
            AbilitiesToLearn[32] = new KeyValuePair<Ability.Ability, int>(fourthAbility, 4);
            AbilitiesToLearn[33]= new KeyValuePair<Ability.Ability, int>(ultimate, 4);
        }

        private void SetUpFire()
        {
            var fireAbilities = new List<Ability.Ability>();
            fireAbilities.Add(new Ignite());
            fireAbilities.Add(new TickingBomb());
            fireAbilities.Add(new BurningLava());
            fireAbilities.Add(new LastChance());
            fireAbilities.Add(new Pyroblast());
            SetUpSchool(fireAbilities);
        }

        private void SetUpSelfHarm()
        {
            var selfHarmAbilities = new List<Ability.Ability>();
            selfHarmAbilities.Add(new BlindingRage());
            selfHarmAbilities.Add(new UndyingWill());
            selfHarmAbilities.Add(new Hysteria());
            selfHarmAbilities.Add(new Reflection());
            selfHarmAbilities.Add(new ChaosEnsues());
            SetUpSchool(selfHarmAbilities);
        }

        private void SetUpNature()
        {
            var natureAbilities = new List<Ability.Ability>();
            natureAbilities.Add(new LivingRoots());
            natureAbilities.Add(new FeralRage());
            natureAbilities.Add(new Empower());
            natureAbilities.Add(new Leech());
            natureAbilities.Add(new Shapeshift());
            SetUpSchool(natureAbilities);
        }
        
        public void JumpToGivenLevel(int level, string school = null)
        {
            while (Level < level)
                LevelUp(school);

        }

        public string GetSchool()
        {
            return School;
        }

        public string GetDifficulty()
        {
            return Difficulty;
        }

        public double GetExperiencePoints()
        {
            return ExperiencePoints;
        }

        public double GetKillCount()
        {
            return KillCount;
        }

        public void IncrementKillCount()
        {
            KillCount += 1;
        }
        
        public double GetInnateArmourPenetration()
        {
            return InnateArmourPenetration;
        }
        
        public double GetInnateCriticalChance()
        {
            return InnateCriticalChance;
        }

        public Tuple<double, double, double> Weaken()
        {
            double lostHealth = MaximumHealth / 5;
            MaximumHealth -= lostHealth;
            Health = Math.Max(1, Health - lostHealth);

            double lostAttack = InnateAttack / 5;
            InnateAttack -= lostAttack;
            Attack -= lostAttack;

            double lostDefense = Defense / 5;
            InnateDefense -= lostDefense;
            Defense -= lostDefense;

            return new Tuple<double, double, double>(lostHealth, lostAttack, lostDefense);
        }
        
        public override string ToString()
        {
            var toStr =
                $"{Name}: {Math.Round(Health, 2)}/{Math.Round(MaximumHealth, 2)} HEALTH, {Math.Round(Mana, 2)}/" +
                $"{Math.Round(TotalMana, 2)} MANA, {Math.Round(Defense, 2)} DEFENSE, " +
                $"{Math.Round(Attack, 2)} ATTACK, {Math.Round(Sanity, 2)}/{Math.Round(MaxSanity, 2)} SANITY, " +
                $"{CriticalChance * 100}% CRITICAL CHANCE, {ArmourPenetration * 100}% ARMOUR PENETRATION\n" +
                $"{Gold} GOLD, {Level} LEVEL\n{ItemHelper.ItemToString(Weapon)}{ItemHelper.ItemToString(Armour)}school:{School}\nattack growth rate: {AttackGrowth}, " +
                $"defense growth rate: {DefenseGrowth}\nhealth growth rate: {HealthGrowth}, mana growth rate: {ManaGrowth}\n";
            return toStr;
        }
    }
}