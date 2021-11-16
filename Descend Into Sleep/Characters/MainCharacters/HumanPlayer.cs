using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Numerics;
using ConsoleApp12.Ability;
using ConsoleApp12.Ability.HumanAbilities;
using ConsoleApp12.Ability.HumanAbilities.FireAbilities;
using ConsoleApp12.Ability.HumanAbilities.NatureAbilities;
using ConsoleApp12.Ability.HumanAbilities.NeutralAbilities;
using ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Game.keysWork;
using ConsoleApp12.Items;
using ConsoleApp12.Items.Armours.LevelOne;
using ConsoleApp12.Items.Weapons;
using ConsoleApp12.Items.Weapons.LevelOne;
using ConsoleApp12.Items.Weapons.LevelTwo;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class HumanPlayer: Character
    {
        protected int Level;
        protected double ExperiencePoints;
        protected List<Item> Inventory;
        protected Dictionary<int, KeyValuePair<Ability.Ability, int>> AbilitiesToLearn;
        protected double Gold;
        protected bool Cheater;
        protected List<Character> PastSelves;
        protected string School;
        protected string Difficulty;
        protected double AttackGrowth;
        protected double DefenseGrowth;
        protected double HealthGrowth;
        protected double ManaGrowth;

        public HumanPlayer(string name, string difficulty, Weapon weapon, Armour armour) : 
            base(name, 10, 0, weapon, armour, 20)
        {
            Level = 0;
            ExperiencePoints = 0;
            Inventory = new List<Item>();
            for (var index = 0; index < 8; index++)
                Inventory.Add(null);
            Gold = 0;
            Cheater = false;
            PastSelves = new List<Character>();
            School = null;
            Difficulty = difficulty;
            AbilitiesToLearn = new Dictionary<int, KeyValuePair<Ability.Ability, int>>();
            AbilitiesToLearn[1] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 1);
            AbilitiesToLearn[2] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 1);
            AbilitiesToLearn[3] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 1);
            AbilitiesToLearn[5] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 2);
            AbilitiesToLearn[6] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 2);
            AbilitiesToLearn[7] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 2);
            AbilitiesToLearn[9] = new KeyValuePair<Ability.Ability, int>(new Taunt(), 3);
            AbilitiesToLearn[11] = new KeyValuePair<Ability.Ability, int>(new Strengthen(), 3);
            AbilitiesToLearn[12] = new KeyValuePair<Ability.Ability, int>(new Bolster(), 3);
            AbilitiesToLearn[14] = new KeyValuePair<Ability.Ability, int>(new Discourage(), 1);
            AbilitiesToLearn[19] = new KeyValuePair<Ability.Ability, int>(new Focus(), 1);
            AbilitiesToLearn[21] = new KeyValuePair<Ability.Ability, int>(new Focus(), 2);
            AbilitiesToLearn[24] = new KeyValuePair<Ability.Ability, int>(new CleanseDOT(), 1);
            AbilitiesToLearn[29] = new KeyValuePair<Ability.Ability, int>(new TrueDamage(), 1);
            AbilitiesToLearn[34] = new KeyValuePair<Ability.Ability, int>(new CCImmunity(), 1);
            
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
        
        private int FindEmptyPositionInInventory()
        {
            for (int index = 0; index < Inventory.Count; index++)
                if (Inventory[index] == null)
                    return index;
            return -1;
        }

        public List<Item> GetInventory()
        {
            return Inventory;
        }

        public void PickUp(Item item)
        {
            int firstNonEmpty = FindEmptyPositionInInventory();
            if (firstNonEmpty == -1)
                throw new FullInventoryException();
            Inventory[firstNonEmpty] = item;
        }

        private int FindItemByName(string itemName)
        {
            for (int index = 0; index < Inventory.Count; index++)
                if (Inventory[index] != null && Inventory[index].GetName() == itemName)
                    return index;
            return -1;
        }

        public string DropItem(string itemName)
        {
            int itemIndex = FindItemByName(itemName);
            if (itemIndex == -1)
                throw new InvalidItemException();
            string toStr = itemName + " has been dropped!\n";
            Inventory[itemIndex] = null;
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
            
            var attackDamage = Weapon.GetAttackValue();
            var defenseValue = Weapon.GetDefenseValue();
            var armourPenetration = Weapon.GetArmorPenetration();
            var criticalChance = Weapon.GetCriticalChance();
            IncreaseStatsAtWeaponChange(-attackDamage, -defenseValue, -armourPenetration, -criticalChance);
            
            Weapon = new NoWeapon();
        }

        public void MoveArmourToInventory()
        {
            int firstEmpty = FindEmptyPositionInInventory();
            if (firstEmpty == -1)
                throw new FullInventoryException();
            if (Armour.GetName() == "No Armour")
                throw new InvalidItemDropException("armour");
            Inventory[firstEmpty] = Armour;

            var attackDamage = Armour.GetAttackValue();
            var defenseValue = Armour.GetDefenseValue();
            IncreaseStatsAtArmourChange(attackDamage, defenseValue);
            
            Armour = new NoArmour();
        }

        private string UseWeapon(Weapon weapon, int itemIndex)
        {
            var toStr = "You have equipped " + weapon.GetName() + "!\n";
            var oldWeapon = ChangeWeapon(weapon);
            if (oldWeapon.GetName() != "No Weapon")
                Inventory[itemIndex] = oldWeapon;
            else
                Inventory[itemIndex] = null;
            var oldAttack = oldWeapon.GetAttackValue();
            var oldDefense = oldWeapon.GetDefenseValue();
            var oldArmourPenetration = oldWeapon.GetArmorPenetration();
            var oldCriticalChance = oldWeapon.GetCriticalChance();

            var newAttack = weapon.GetAttackValue();
            var newDefense = weapon.GetDefenseValue();
            var newArmourPenetration = weapon.GetArmorPenetration();
            var newCriticalChance = weapon.GetCriticalChance();

            var attackDifference = newAttack - oldAttack;
            var defenseDifference = newDefense - oldDefense;
            var armourPenetrationDifference = newArmourPenetration - oldArmourPenetration;
            var criticalChanceDifference = newCriticalChance - oldCriticalChance;
            IncreaseStatsAtWeaponChange(attackDifference, defenseDifference, armourPenetrationDifference, criticalChanceDifference);
            return toStr;
        }

        private void IncreaseStatsAtWeaponChange(double attackValue, double defenseValue, double armourPenetration,
            double criticalChance)
        {
            IncreaseAttackValue(attackValue);
            IncreaseDefenseValue(defenseValue);
            IncreaseArmourPenetration(armourPenetration);
            IncreaseCriticalChance(criticalChance);
        }

        private void IncreaseStatsAtArmourChange(double attackValue, double defenseValue)
        {
            IncreaseAttackValue(attackValue);
            IncreaseDefenseValue(defenseValue);
        }
        
        private string UseArmour(Armour armour, int itemIndex)
        {
            var toStr = "You have equipped " + armour.GetName() + "!\n";
            var oldArmour = ChangeArmour(armour);
            if (oldArmour.GetName() != "No Armour")
                Inventory[itemIndex] = oldArmour;
            else
                Inventory[itemIndex] = null;

            var oldAttack = oldArmour.GetAttackValue();
            var oldDefense = oldArmour.GetDefenseValue();

            var newAttack = armour.GetAttackValue();
            var newDefense = armour.GetDefenseValue();

            var attackDifference = newAttack - oldAttack;
            var defenseDifference = newDefense - oldDefense;
                
            IncreaseStatsAtArmourChange(attackDifference, defenseDifference);
            return toStr;
        }

        private string UsePotion(Potion potion, int itemIndex)
        {
            var toStr = "You have consumed a " + potion.GetName() + "!\n";
            Inventory[itemIndex] = null;
            toStr += potion.UseItem(this);
            return toStr;

        }
        
        public string UseItem(string itemName)
        {
            var itemIndex = FindItemByName(itemName);
            if (itemIndex == -1)
                throw new InvalidItemException();
            var item = Inventory[itemIndex];
            if (item is Weapon weapon)
                return UseWeapon(weapon, itemIndex);
            if (item is Armour armour)
                return UseArmour(armour, itemIndex);
            if (item is Potion potion)
                return UsePotion(potion, itemIndex);
            return "";
        }
        
        public bool GainExperience(double experiencePoints)
        {
            ExperiencePoints += experiencePoints;
            var experienceNeeded = 25 * (Level + 1) * (Level + 2);
            if (ExperiencePoints >= experienceNeeded)
            {
                LevelUp();
                return true;
            }
            return false;
        }

        public void LevelUp(string alreadyChosenSchool = null)
        {
            if (Level >= 35)
                throw new MaximumLevelException();
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
                        PastSelves.Add(CreateCharacterCopy("Young " + Name, "Does not want to hurt you\n"));
                        break;
                    case 15:
                        PastSelves.Add(CreateCharacterCopy("Teen " + Name, "Might want to hurt you\n"));
                        break;
                    case 30:
                        PastSelves.Add(CreateCharacterCopy("Adult " + Name, "Wants to MURDER you\n"));
                        break;
                }
            }
            
            if (Level == 4)
                ChooseSchool(alreadyChosenSchool);
            
            var isVerbose = alreadyChosenSchool is null;
            NewAbility(isVerbose);
            
        }

        public List<Character> GetPastSelves()
        {
            return PastSelves;
        }

        private void NewAbility(bool verbose = true)
        {
            var abilityInfo = AbilitiesToLearn[Level];
            var abilityToLearn = abilityInfo.Key;
            var abilityLevel = abilityInfo.Value;
            var toStr = "";
            if (abilityLevel == 1)
                toStr = LearnAbility(abilityToLearn);
            else
                toStr = LevelUpAbility(abilityToLearn);
            if (verbose)
                Console.WriteLine(toStr);
        }

        public void AddLifeStealToWeapon(double lifeStealAdded)
        {
            var currentLifeSteal = Weapon.GetLifeSteal();
            currentLifeSteal += lifeStealAdded;
            Weapon.SetLifeSteal(currentLifeSteal);
        }

        public void RemoveLifeStealFromWeapon(double lifeStealRemoved)
        {
            var currentLifeSteal = Weapon.GetLifeSteal();
            currentLifeSteal -= lifeStealRemoved;
            Weapon.SetLifeSteal(currentLifeSteal);
        }

        public int GetLevel()
        {
            return Level;
        }

        public string ShowInventory()
        {
            var toStr = "";
            foreach (var item in Inventory)
            {
                if (item is Weapon weapon)
                {
                    toStr += weapon.ToString();
                }
                
                if (item is Armour armour)
                {
                    toStr += armour.ToString();
                }
                
                if (item is Potion potion)
                {
                    toStr += potion.ToString();
                }
            }

            return toStr;
        }

        private string LearnAbility(Ability.Ability abilityToLearn)
        {
            var abilityName = abilityToLearn.GetName();
            RespectiveAbilities[abilityName] = abilityToLearn;
            var toStr = abilityName + " has been learnt!";
            return toStr;
        }

        public string GetAbilitiesDescription()
        {
            var toStr = "";
            foreach (var ability in RespectiveAbilities)
            {
                toStr += ability.Value.GetName() + ":" + ability.Value.GetDescription() + "\n";
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

        public void BuyItem(double cost, Item item)
        {
            if (Gold < cost)
                throw new InsufficientGoldException(item.GetName());
            PickUp(item);
            Gold -= cost;
        }

        public void SellItem(double cost, Item item)
        {
            int itemIndex = FindItemByName(item.GetName());
            if (itemIndex != -1)
                throw new NotFoundItemException();
            Inventory[itemIndex] = null;
            Gold += cost;
            
        }

        public override string Cast(string abilityName, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var abilityManaCost = RespectiveAbilities[abilityName].GetManaCost();
            if (Mana < abilityManaCost)
                throw new InsufficientManaException(Mana, abilityManaCost, Name, abilityName);
            Mana -= abilityManaCost;
            return RespectiveAbilities[abilityName].Cast(this, opponent, listOfTurns, turnCounter);
        }

        private Character CreateCharacterCopy(string copyName, string copyDescription)
        {
            var chara = new Character(copyName, InnateAttack, InnateDefense, Weapon, Armour, MaximumHealth, copyDescription);
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

        private string LevelUpAbility(Ability.Ability ability, bool verbose = true)
        {
            var abilityName = ability.GetName();
            RespectiveAbilities[abilityName].LevelUp();
            var toStr = abilityName + " has been leveled up to level " + RespectiveAbilities[abilityName].GetLevel() +
                    "!\n";
            return toStr;
        }

        public void ChooseSchool(string alreadyChosenSchool = null)
        {
            String schoolChoice;
            if (alreadyChosenSchool is not null)
                schoolChoice = alreadyChosenSchool;
            
            else
            {
                var schools = new String[3] {"fire", "self-harm", "nature"};
                Console.WriteLine("Choose a school to be a part of");
                    var choice = ConsoleHelper.MultipleChoice(20, "fire", "self-harm", "nature");
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
            selfHarmAbilities.Add(new UltimateMadness());
            selfHarmAbilities.Add(new ChaoticReflection());
            selfHarmAbilities.Add(new ChaosEnsues());
            SetUpSchool(selfHarmAbilities);
        }

        private void SetUpNature()
        {
            var natureAbilities = new List<Ability.Ability>();
            natureAbilities.Add(new LivingRoots());
            natureAbilities.Add(new FeralRage());
            natureAbilities.Add(new NatureCleansing());
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

        public void SetExperiencePoints(double experiencePoints)
        {
            ExperiencePoints = experiencePoints;
        }
        
        public double GetExperiencePoints()
        {
            return ExperiencePoints;
        }

        public void SetGold(double gold)
        {
            Gold = gold;
        }
        
        public void SetPastSelves(List<Character> pastSelves)
        {
            PastSelves = pastSelves;
        }

        public double GetInnateArmourPenetration()
        {
            return InnateArmourPenetration;
        }

        public void SetInnateArmourPenetration(double newInnateArmourPenetration)
        {
            InnateArmourPenetration = newInnateArmourPenetration;
        }

        public double GetInnateCriticalChance()
        {
            return InnateCriticalChance;
        }

        public void SetInnateCriticalChance(double newInnateCriticalChance)
        {
            InnateCriticalChance = newInnateCriticalChance;
        }

        public override string ToString()
        {
            var toStr = Name + ": " + Math.Round(Health, 2) + "/" + Math.Round(MaximumHealth, 2) +
                        " HEALTH, " + Math.Round(Mana, 2) + " MANA, " + Defense + " DEFENSE, ";
            toStr += Attack + " ATTACK, " + Gold + " GOLD, " + Level + " LEVEL\n";
            toStr += Weapon.ToString();
            toStr += Armour.ToString();
            toStr += "school: ";
            if (School is null)
                toStr += "none\n";
            else
                toStr += School + "\n";
            toStr += "attack growth rate: " + AttackGrowth + ", defense growth rate: " +
                     DefenseGrowth + "\n";
            toStr += "health growth rate: " + HealthGrowth + ", mana growth rate: " + ManaGrowth +
                     "\n";
            return toStr;
        }
    }
}