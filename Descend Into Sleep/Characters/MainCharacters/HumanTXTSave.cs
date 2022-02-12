using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.MainCharacters
{
    public static class HumanTxtSave
    {
        

        private static string GetTextString(HumanPlayer humanPlayer, int gameLevel, DateTime currentTime)
        {
            var toStr = humanPlayer.GetName() + "\n";
            toStr += humanPlayer.GetLevel() + "\n";
            toStr += humanPlayer.GetExperiencePoints() + "\n";
            toStr += humanPlayer.GetInnateAttack() + "\n";
            toStr += humanPlayer.GetInnateDefense() + "\n";
            toStr += humanPlayer.GetInnateCriticalChance() + "\n";
            toStr += humanPlayer.GetInnateArmourPenetration() + "\n";
            toStr += humanPlayer.GetManaRegenerationRate() + "\n";
            toStr += humanPlayer.GetMana() + "\n";
            toStr += humanPlayer.GetHealthPoints() + "\n";
            toStr += humanPlayer.GetMaximumHealthPoints() + "\n";
            toStr += humanPlayer.GetTotalMana() + "\n";
            foreach (var item in humanPlayer.GetInventory())
            {
                var id = -1;
                if (item != null)
                    id = AllItems.FindIdForItem(item);
                toStr += id + "\n";
            }
            toStr += humanPlayer.GetGold() + "\n";
            if (humanPlayer.GetSchool() == null)
                toStr += "\n";
            else
                toStr += humanPlayer.GetSchool() + "\n";
            toStr += humanPlayer.GetSanity() + "\n";
            toStr += humanPlayer.GetMaximumSanity() + "\n";
            toStr += humanPlayer.GetKillCount() + "\n";
            toStr += humanPlayer.GetDifficulty() + "\n";
            toStr += AllItems.FindIdForItem(humanPlayer.GetWeapon()) + "\n";
            toStr += AllItems.FindIdForItem(humanPlayer.GetArmour()) + "\n";
            toStr += currentTime + "\n";
            toStr += gameLevel + "\n";
            toStr += humanPlayer.GetPastSelves().Count + "\n";
            foreach (var pastSelf in humanPlayer.GetPastSelves())
            {
                toStr += pastSelf.GetName() + "\n";
                toStr += pastSelf.GetInnateAttack() + "\n";
                toStr += pastSelf.GetInnateDefense() + "\n";
                toStr += AllItems.FindIdForItem(pastSelf.GetWeapon()) + "\n";
                toStr += AllItems.FindIdForItem(pastSelf.GetArmour()) + "\n";
                toStr += pastSelf.GetMaximumHealthPoints() + "\n";
                toStr += pastSelf.GetDescription() + "\n";
            }
            return toStr;
        }
        
        public static void Save(HumanPlayer humanPlayer, int gameLevel, DateTime currentTime, string filename)
        {
            var textString = GetTextString(humanPlayer, gameLevel, currentTime);
            File.WriteAllText(filename, textString);
        }

        public static Tuple<HumanPlayer, int, DateTime> Load(string filename)
        {
            var lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
                throw new EmptyFileException(filename);

            if (lines.Length < 31)
                throw new CorruptedLengthException(filename, lines.Length);
            
            var name = lines[0];
            
            if (!int.TryParse(lines[1], out var humanLevel))
                throw new CorruptedFormatSaveFileException(filename, 2, typeof(int));
            if (humanLevel < 0 || humanLevel > 34)
                throw new CorruptedInvalidValuesException(filename, 2);
            
            if (!double.TryParse(lines[2], out var experiencePoints))
                throw new CorruptedFormatSaveFileException(filename, 3, typeof(double));
            // check this to work

            if (!double.TryParse(lines[3], out var innateAttack))
                throw new CorruptedFormatSaveFileException(filename, 4, typeof(double));

            if (!double.TryParse(lines[4], out var innateDefense))
                throw new CorruptedFormatSaveFileException(filename, 5, typeof(double));

            if (!double.TryParse(lines[5], out var innateCriticalChance))
                throw new CorruptedFormatSaveFileException(filename, 6, typeof(double));
            if (innateCriticalChance < 0 || innateCriticalChance > 1)
                throw new CorruptedInvalidValuesException(filename, 6);

            if (!double.TryParse(lines[6], out var innateArmourPenetration))
                throw new CorruptedFormatSaveFileException(filename, 7, typeof(double));
            if (innateArmourPenetration < 0 || innateArmourPenetration > 1)
                throw new CorruptedInvalidValuesException(filename, 7);

            if (!double.TryParse(lines[7], out var manaRegenerationRate))
                throw new CorruptedFormatSaveFileException(filename, 8, typeof(double));

            if (!double.TryParse(lines[8], out var mana))
                throw new CorruptedFormatSaveFileException(filename, 9, typeof(double));
            if (mana <= 0)
                throw new CorruptedInvalidValuesException(filename, 9);

            if (!double.TryParse(lines[9], out var health))
                throw new CorruptedFormatSaveFileException(filename, 10, typeof(double));
            if (health <= 0)
                throw new CorruptedInvalidValuesException(filename, 10);

            if (!double.TryParse(lines[10], out var maximumHealth))
                throw new CorruptedFormatSaveFileException(filename, 11, typeof(double));
            if (health > maximumHealth)
                throw new CorruptedInvalidValuesException(filename, 11);
            if (maximumHealth <= 0)
                throw new CorruptedInvalidValuesException(filename, 11);

            if (!double.TryParse(lines[11], out var totalMana))
                throw new CorruptedFormatSaveFileException(filename, 12, typeof(double));
            if (mana > totalMana)
                throw new CorruptedInvalidValuesException(filename, 12);
            if (totalMana <= 0)
                throw new CorruptedInvalidValuesException(filename, 12);
            
            List<Item> inventory = new List<Item>();
            for (int i = 0; i < 8; i++)
            {
                var currentLine = 12 + i;
                if (!int.TryParse(lines[currentLine], out var currentItem))
                    throw new CorruptedFormatSaveFileException(filename, currentLine + 1, typeof(int));
                if (currentItem == -1)
                    inventory.Add(null);
                else
                {
                    if (!AllItems.Items.ContainsKey(currentItem))
                        throw new CorruptedInvalidValuesException(filename, currentLine + 1);
                    inventory.Add(AllItems.Items[currentItem]);
                }
            }
            

            if (!double.TryParse(lines[20], out var gold))
                throw new CorruptedFormatSaveFileException(filename, 21, typeof(double));
            if (gold < 0)
                throw new CorruptedInvalidValuesException(filename, 21);

            string school = lines[21];
            if (school != "" && school != "fire" && school != "self-harm" && school != "nature")
                throw new CorruptedInvalidValuesException(filename, 22);

            if (!double.TryParse(lines[22], out var sanity))
                throw new CorruptedFormatSaveFileException(filename, 23, typeof(double));
            if (sanity <= 0)
                throw new CorruptedInvalidValuesException(filename, 23);

            if (!double.TryParse(lines[23], out var maximumSanity))
                throw new CorruptedFormatSaveFileException(filename, 24, typeof(double));
            if (sanity > maximumSanity)
                throw new CorruptedInvalidValuesException(filename, 24);
            if (maximumSanity <= 0)
                throw new CorruptedInvalidValuesException(filename, 24);

            if (!double.TryParse(lines[24], out var killCount))
                throw new CorruptedFormatSaveFileException(filename, 25, typeof(double));
            if (killCount < 0)
                throw new CorruptedInvalidValuesException(filename, 25);

            string difficulty = lines[25];
            if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard" && difficulty != "impossible")
                throw new CorruptedInvalidValuesException(filename, 26);
            
            if (!int.TryParse(lines[26], out var weaponId))
                throw new CorruptedFormatSaveFileException(filename, 27, typeof(int));
            
            if (!AllItems.Items.ContainsKey(weaponId))
                            throw new CorruptedInvalidValuesException(filename, 27);
                        
            Item weapItem = AllItems.Items[weaponId];
            if (weapItem is not Weapon weap)
                throw new CorruptedInvalidValuesException(filename, 27);

            
            if (!int.TryParse(lines[27], out var armourId))
                throw new CorruptedFormatSaveFileException(filename, 28, typeof(int));
            
            if (!AllItems.Items.ContainsKey(armourId))
                throw new CorruptedInvalidValuesException(filename, 28);

            Item armItem = AllItems.Items[armourId];
            if (armItem is not Armour arm)
                throw new CorruptedInvalidValuesException(filename, 28);

            
            if (!DateTime.TryParseExact(lines[28], "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None,
                out var dateTime))
                throw new CorruptedFormatSaveFileException(filename, 29, typeof(DateTime));

            if (!int.TryParse(lines[29], out var gameLevel))
                throw new CorruptedFormatSaveFileException(filename, 30, typeof(int));
            if (gameLevel < 0 || gameLevel > 7)
                throw new CorruptedInvalidValuesException(filename, 30);

            if (!int.TryParse(lines[30], out var pastSelvesCount))
                throw new CorruptedFormatSaveFileException(filename, 31, typeof(int));

            if (pastSelvesCount > 3 || pastSelvesCount < 0)
            {
                throw new CorruptedInvalidValuesException(filename, 31);
            }


            if (lines.Length < 30 + 6 * pastSelvesCount)
                throw new CorruptedLengthException(filename, lines.Length);
            
            var pastSelves = new List<PastSelf>();
            for (int i = 0; i < pastSelvesCount; i++)
            {
                var currentName = lines[31 + 6 * i];
                if (!double.TryParse(lines[32 + 6 * i], out var currentAttack))
                    throw new CorruptedFormatSaveFileException(filename, 33 + 6 * i, typeof(double));
                if (currentAttack < 0)
                    throw new CorruptedInvalidValuesException(filename, 34 + 6 * i);
                
                if (!double.TryParse(lines[33 + 6 * i], out var currentDefense))
                    throw new CorruptedFormatSaveFileException(filename, 34 + 6 * i, typeof(double));

                if (!int.TryParse(lines[34 + 6 * i], out var currentWeapon))
                    throw new CorruptedFormatSaveFileException(filename, 35 + 6 * i, typeof(int));
                if (!AllItems.Items.ContainsKey(currentWeapon))
                    throw new CorruptedInvalidValuesException(filename, 36 + 6 * i);
                var weaponItem = AllItems.Items[currentWeapon];
                if (weaponItem is not Weapon pastSelfWeapon)
                    throw new CorruptedInvalidValuesException(filename, 36 + 6 * i);
                
                if (!int.TryParse(lines[35 + 6 * i], out var currentArmour))
                    throw new CorruptedFormatSaveFileException(filename, 36 + 6 * i, typeof(int));
                if (!AllItems.Items.ContainsKey(currentArmour))
                    throw new CorruptedInvalidValuesException(filename, 37 + 6 * i);
                var armourItem = AllItems.Items[currentArmour];
                if (armourItem is not Armour pastSelfArmour)
                    throw new CorruptedInvalidValuesException(filename, 37 + 6 * i);
                
                if (!double.TryParse(lines[36 + 6 * i], out var currentHealth))
                    throw new CorruptedFormatSaveFileException(filename, 37 + 6 * i, typeof(double));
                if (currentHealth < 0)
                    throw new CorruptedInvalidValuesException(filename, 38 + 6 * i);
                
                var currentDescription = lines[37 + 6 * i];
                
                var pastSelf = new PastSelf(currentName, currentAttack, currentDefense, pastSelfWeapon, pastSelfArmour,
                    currentHealth, currentDescription);
                pastSelves.Add(pastSelf);
            }

            var humanPlayer = new HumanPlayer(name, humanLevel, experiencePoints, innateAttack,
                innateDefense, innateCriticalChance, innateArmourPenetration, manaRegenerationRate,
                mana, health, maximumHealth, totalMana, inventory, gold, school, sanity, maximumSanity,
                killCount, difficulty, weap, arm, pastSelves);
            return new Tuple<HumanPlayer, int, DateTime>(humanPlayer, gameLevel, dateTime);
        }
        
    }
}