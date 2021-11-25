using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class HumanJSONSave
    {
        public string Name { get; set; }
        public int HumanLevel { get; set; }
        public double ExperiencePoints { get; set; }

        public double InnateAttack { get; set; }
        
        public double InnateDefense { get; set; }
        
        public double InnateCriticalChance { get; set; }
        
        public double InnateArmourPenetration { get; set; }
        
        public double ManaRegenerationRate { get; set; }

        public double Mana { get; set; }
        
        public double Health { get; set; }
        
        public double MaximumHealth { get; set; }
        
        public double TotalMana { get; set; }
        
        public List<int> Inventory { get; set; }

        public double Gold { get; set; }
        public string School { get; set; }
        
        public double Sanity { get; set; }
        
        public double MaximumSanity { get; set; }
        public string Difficulty { get; set; }
        public int Weapon { get; set; }
        
        public int Armour { get; set; }

        public string[] PastSelvesNames { get; set; }
        public double[] PastSelvesInnateAttack { get; set; }
        public double[] PastSelvesInnateDefense { get; set; }
        public int[] PastSelvesWeapons { get; set; }
        public int[] PastSelvesArmours { get; set; }
        public double[] PastSelvesMaxHealth { get; set; }
        public string[] PastSelvesDescription { get; set; }
        
        public DateTime CurrentDate { get; set; }
        public int GameLevel { get; set; }

        public HumanJSONSave()
        {
            
        }
        
        public HumanJSONSave(HumanPlayer humanPlayer, int gameLevel)
        {
            HandlePastSelves(humanPlayer);
            HandlePlayerStats(humanPlayer);
            CurrentDate = DateTime.Now;
            GameLevel = gameLevel;
        }

        private void HandlePastSelves(HumanPlayer humanPlayer)
        {
            PastSelvesNames = new string[3];
            PastSelvesInnateAttack = new double[3];
            PastSelvesInnateDefense = new double[3];
            PastSelvesWeapons = new int[3];
            PastSelvesArmours = new int[3];
            PastSelvesMaxHealth = new double[3];
            PastSelvesDescription = new string[3];
            
            var pastSelves = humanPlayer.GetPastSelves();
            switch (pastSelves.Count)
            {
                case 0:
                    LoadPastSelf(0, pastSelves, true);
                    LoadPastSelf(1, pastSelves, true);
                    LoadPastSelf(2, pastSelves, true);
                    break;
                case 1:
                    LoadPastSelf(0, pastSelves);
                    LoadPastSelf(1, pastSelves, true);
                    LoadPastSelf(2, pastSelves, true);
                    break;
                case 2:
                    LoadPastSelf(0, pastSelves);
                    LoadPastSelf(1, pastSelves);
                    LoadPastSelf(2, pastSelves, true);
                    break;
                case 3:
                    LoadPastSelf(0, pastSelves);
                    LoadPastSelf(1, pastSelves);
                    LoadPastSelf(2, pastSelves);
                    break;
            }
        }

        private void HandlePlayerStats(HumanPlayer humanPlayer)
        {
            Name = humanPlayer.GetName();
            HumanLevel = humanPlayer.GetLevel();
            ExperiencePoints = humanPlayer.GetExperiencePoints();

            var inventory = humanPlayer.GetInventory();
            Inventory = new List<int>();
            foreach (var item in inventory)
            {
                if (item != null)
                    Inventory.Add(AllItems.FindIdForItem(item));
            }

            Gold = humanPlayer.GetGold();
            Sanity = humanPlayer.GetSanity();
            MaximumSanity = humanPlayer.GetMaximumSanity();
            School = humanPlayer.GetSchool();
            Difficulty = humanPlayer.GetDifficulty();
            Weapon = AllItems.FindIdForItem(humanPlayer.GetWeapon());
            Armour = AllItems.FindIdForItem(humanPlayer.GetArmour());

            InnateAttack = humanPlayer.GetInnateAttack();
            InnateDefense = humanPlayer.GetInnateDefense();
            InnateArmourPenetration = humanPlayer.GetInnateArmourPenetration();
            InnateCriticalChance = humanPlayer.GetInnateCriticalChance();
            ManaRegenerationRate = humanPlayer.GetManaRegenerationRate();
            Mana = humanPlayer.GetMana();
            Health = humanPlayer.GetHealthPoints();
            MaximumHealth = humanPlayer.GetMaximumHealthPoints();
            TotalMana = humanPlayer.GetTotalMana();
        }
        
        private void LoadPastSelf(int index, List<Character> pastSelves, bool isNull = false)
        {
            if (isNull)
            {
                PastSelvesNames[index] = null;
                PastSelvesInnateAttack[index] = -1;
                PastSelvesInnateDefense[index] = -1;
                PastSelvesWeapons[index] = -1;
                PastSelvesArmours[index] = -1;
                PastSelvesMaxHealth[index] = -1;
                PastSelvesDescription[index] = null;
            }
            else
            {
                var pastSelf = pastSelves[index];
                PastSelvesNames[index] = pastSelf.GetName();
                PastSelvesInnateAttack[index] = pastSelf.GetInnateAttack();
                PastSelvesInnateDefense[index] = pastSelf.GetInnateDefense();
                PastSelvesWeapons[index] = AllItems.FindIdForItem(pastSelf.GetWeapon());
                PastSelvesArmours[index] = AllItems.FindIdForItem(pastSelf.GetArmour());
                PastSelvesMaxHealth[index] = pastSelf.GetMaximumHealthPoints();
                PastSelvesDescription[index] = pastSelf.GetDescription();
            }
        }
        public void Save(string filename)
        {
            var jsonString = GetJSONString();
            File.WriteAllText(filename, jsonString);
        }

        public static Tuple<HumanPlayer, int, DateTime> Load(string filename)
        {
            String jsonStringEncoded;
            try
            {
                jsonStringEncoded = File.ReadAllText(filename);
            }
            catch (FileNotFoundException)
            {
                throw new UnopenableSaveFileException(filename);
            }

            var jsonStringDecoded = DecodeString(jsonStringEncoded);
            if (jsonStringDecoded.Length == 0)
                return new Tuple<HumanPlayer, int, DateTime>(null, -1, DateTime.Now);

            HumanJSONSave saveDetails;
            try
            {
                saveDetails = JsonSerializer.Deserialize<HumanJSONSave>(jsonStringDecoded);
            }
            catch (JsonException)
            {
                throw new CorruptedSaveFileException(filename);
            }

            var weaponId = saveDetails.Weapon;
            var currentWeapon = (Weapon) AllItems.Items[weaponId];

            var armourId = saveDetails.Armour;
            var currentArmour = (Armour) AllItems.Items[armourId];

            var currentPlayer = new HumanPlayer(saveDetails.Name, saveDetails.Difficulty, currentWeapon, currentArmour);
            if (saveDetails.School is null)
                currentPlayer.JumpToGivenLevel(saveDetails.HumanLevel, "");
            else
                currentPlayer.JumpToGivenLevel(saveDetails.HumanLevel, saveDetails.School);    
            
            currentPlayer.SetInnateAttack(saveDetails.InnateAttack);
            currentPlayer.SetInnateDefense(saveDetails.InnateDefense);
            currentPlayer.SetInnateArmourPenetration(saveDetails.InnateArmourPenetration);
            currentPlayer.SetInnateCriticalChance(saveDetails.InnateCriticalChance);
            currentPlayer.SetManaRegenerationRate(saveDetails.ManaRegenerationRate);
            currentPlayer.SetMana(saveDetails.Mana);
            currentPlayer.SetTotalMana(saveDetails.TotalMana);
            currentPlayer.SetInnateMaximumHealth(saveDetails.MaximumHealth);
            currentPlayer.SetHealthPoints(saveDetails.Health);
            currentPlayer.SetSanity(saveDetails.Sanity);
            currentPlayer.SetMaximumSanity(saveDetails.MaximumSanity);
            
            foreach (var itemId in saveDetails.Inventory)
            {
                var item = AllItems.Items[itemId];
                currentPlayer.PickUp(item);
            }
            
            
            currentPlayer.SetExperiencePoints(saveDetails.ExperiencePoints);
            currentPlayer.SetGold(saveDetails.Gold);

            int currentLevel = saveDetails.GameLevel;
            DateTime dateTime = saveDetails.CurrentDate;

            var pastSelves = new List<Character>();

            int index = 0;
            
            while (index < 3 && CreatePastSelf(saveDetails, index) is not null)
            {
                var pastSelf = CreatePastSelf(saveDetails, index);
                pastSelves.Add(pastSelf);
                index += 1;
            }

            currentPlayer.SetPastSelves(pastSelves);
            var returnTuple = new Tuple<HumanPlayer, int, DateTime>(currentPlayer, currentLevel, dateTime);
            return returnTuple;
        }

        private static Character CreatePastSelf(HumanJSONSave saveDetails, int index)
        {
            if (saveDetails.PastSelvesNames[index] is null)
                return null;
            var weapon = (Weapon) AllItems.Items[saveDetails.PastSelvesWeapons[index]];
            var armour = (Armour) AllItems.Items[saveDetails.PastSelvesArmours[index]];

            var name = saveDetails.PastSelvesNames[index];
            var innateAttack = saveDetails.PastSelvesInnateAttack[index];
            var innateDefense = saveDetails.PastSelvesInnateDefense[index];
            var maxHealth = saveDetails.PastSelvesMaxHealth[index];
            var description = saveDetails.PastSelvesDescription[index];
            var pastSelf = new Character(name, innateAttack, innateDefense, weapon, armour, maxHealth, description);
            return pastSelf;
        }
        
        private string GetJSONString()
        {
            var serializedString = JsonSerializer.Serialize(this);
            var encodedString = EncodeString(serializedString);
            return encodedString;
        }

        public static String EncodeString(String stringToEncode)
        {
            var encodedString = new StringBuilder();
            var stringLength = stringToEncode.Length;
            for (var i = 0; i < stringLength; i++)
            {
                var currentCharacter = stringToEncode[i];
                int previousCharacterValue;
                if (i > 0)
                    previousCharacterValue = (int) stringToEncode[i - 1];
                else
                    previousCharacterValue = 0;
                var encodedCharacter = (char) (currentCharacter + previousCharacterValue % 16 + 1);
                encodedString.Append(encodedCharacter);
            }
            return encodedString.ToString();
        }

        public static String DecodeString(String stringToDecode)
        {
            var decodedString = new StringBuilder();
            var stringLength = stringToDecode.Length;
            for (var i = 0; i < stringLength; i++)
            {
                var currentCharacter = stringToDecode[i];
                int previousDecodedCharacterValue;
                if (i > 0)
                    previousDecodedCharacterValue = (int) decodedString[i - 1];
                else
                    previousDecodedCharacterValue = 0;
                var decodedCharacter = (char) (currentCharacter - previousDecodedCharacterValue % 16 - 1);
                decodedString.Append(decodedCharacter);
            }
            
            return decodedString.ToString();
        }
        
    }
}