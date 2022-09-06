using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Items.ItemTypes;

namespace ConsoleApp12.Characters.MainCharacters
{
    public static class HumanPlayerSerializer
    {
        private static string GetTextString(HumanPlayer humanPlayer, int gameLevel, List<int> enemies,
            DateTime currentTime)
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
                    id = Shop.Shop.FindIdForItem(item);
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
            toStr += Shop.Shop.FindIdForItem(humanPlayer.GetWeapon()) + "\n";
            toStr += Shop.Shop.FindIdForItem(humanPlayer.GetArmour()) + "\n";
            toStr += currentTime.ToString("dd/MM/yyyy HH:mm:ss") + "\n";
            toStr += gameLevel + "\n";
            for (int i = 0; i < enemies.Count; i++)
            {
                toStr += enemies[i];
                if (i != enemies.Count - 1)
                    toStr += ",";
                else
                    toStr += "\n";
            }

            toStr += humanPlayer.GetPastSelves().Count + "\n";
            foreach (var pastSelf in humanPlayer.GetPastSelves())
            {
                toStr += pastSelf.GetName() + "\n";
                toStr += pastSelf.GetInnateAttack() + "\n";
                toStr += pastSelf.GetInnateDefense() + "\n";
                toStr += Shop.Shop.FindIdForItem(pastSelf.GetWeapon()) + "\n";
                toStr += Shop.Shop.FindIdForItem(pastSelf.GetArmour()) + "\n";
                toStr += pastSelf.GetMaximumHealthPoints() + "\n";
                toStr += pastSelf.GetDescription() + "\n";
            }

            return toStr;
        }

        public static void Save(HumanPlayer humanPlayer, int gameLevel, List<int> enemies, DateTime currentTime,
            string filename)
        {
            var textString = GetTextString(humanPlayer, gameLevel, enemies, currentTime);
            File.WriteAllText(filename, textString);
        }

        public static Tuple<HumanPlayer, int, List<int>, DateTime> Load(string filename, int number)
        {
            var lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
                throw new EmptyFileException(number);

            if (lines.Length < 32)
                throw new CorruptedLengthException(number, lines.Length);

            var name = lines[0];

            if (!int.TryParse(lines[1], out var humanLevel))
                throw new CorruptedFormatSaveFileException(number, 2, typeof(int));
            if (humanLevel < 0 || humanLevel > 34)
                throw new CorruptedInvalidValuesException(number, 2);

            if (!double.TryParse(lines[2], out var experiencePoints))
                throw new CorruptedFormatSaveFileException(number, 3, typeof(double));
            // check this to work

            if (!double.TryParse(lines[3], out var innateAttack))
                throw new CorruptedFormatSaveFileException(number, 4, typeof(double));

            if (!double.TryParse(lines[4], out var innateDefense))
                throw new CorruptedFormatSaveFileException(number, 5, typeof(double));

            if (!double.TryParse(lines[5], out var innateCriticalChance))
                throw new CorruptedFormatSaveFileException(number, 6, typeof(double));
            if (innateCriticalChance < 0 || innateCriticalChance > 1)
                throw new CorruptedInvalidValuesException(number, 6);

            if (!double.TryParse(lines[6], out var innateArmourPenetration))
                throw new CorruptedFormatSaveFileException(number, 7, typeof(double));
            if (innateArmourPenetration < 0 || innateArmourPenetration > 1)
                throw new CorruptedInvalidValuesException(number, 7);

            if (!double.TryParse(lines[7], out var manaRegenerationRate))
                throw new CorruptedFormatSaveFileException(number, 8, typeof(double));

            if (!double.TryParse(lines[8], out var mana))
                throw new CorruptedFormatSaveFileException(number, 9, typeof(double));
            if (mana <= 0)
                throw new CorruptedInvalidValuesException(number, 9);

            if (!double.TryParse(lines[9], out var health))
                throw new CorruptedFormatSaveFileException(number, 10, typeof(double));
            if (health <= 0)
                throw new CorruptedInvalidValuesException(number, 10);

            if (!double.TryParse(lines[10], out var maximumHealth))
                throw new CorruptedFormatSaveFileException(number, 11, typeof(double));
            if (health > maximumHealth)
                throw new CorruptedInvalidValuesException(number, 11);
            if (maximumHealth <= 0)
                throw new CorruptedInvalidValuesException(number, 11);

            if (!double.TryParse(lines[11], out var totalMana))
                throw new CorruptedFormatSaveFileException(number, 12, typeof(double));
            if (mana > totalMana)
                throw new CorruptedInvalidValuesException(number, 12);
            if (totalMana <= 0)
                throw new CorruptedInvalidValuesException(number, 12);

            List<IItem> inventory = new List<IItem>();
            for (int i = 0; i < 8; i++)
            {
                var currentLine = 12 + i;
                if (!int.TryParse(lines[currentLine], out var currentItem))
                    throw new CorruptedFormatSaveFileException(number, currentLine + 1, typeof(int));
                if (currentItem == -1)
                    inventory.Add(null);
                else
                {
                    if (!Shop.Shop.ITEMS.ContainsKey(currentItem))
                        throw new CorruptedInvalidValuesException(number, currentLine + 1);
                    inventory.Add(Shop.Shop.ITEMS[currentItem]);
                }
            }


            if (!double.TryParse(lines[20], out double gold))
                throw new CorruptedFormatSaveFileException(number, 21, typeof(double));
            if (gold < 0)
                throw new CorruptedInvalidValuesException(number, 21);

            string school = lines[21];
            if (school != "" && school != "fire" && school != "self-harm" && school != "nature")
                throw new CorruptedInvalidValuesException(number, 22);

            if (!double.TryParse(lines[22], out var sanity))
                throw new CorruptedFormatSaveFileException(number, 23, typeof(double));
            if (sanity <= 0)
                throw new CorruptedInvalidValuesException(number, 23);

            if (!double.TryParse(lines[23], out var maximumSanity))
                throw new CorruptedFormatSaveFileException(number, 24, typeof(double));
            if (sanity > maximumSanity)
                throw new CorruptedInvalidValuesException(number, 24);
            if (maximumSanity <= 0)
                throw new CorruptedInvalidValuesException(number, 24);

            if (!double.TryParse(lines[24], out var killCount))
                throw new CorruptedFormatSaveFileException(number, 25, typeof(double));
            if (killCount < 0)
                throw new CorruptedInvalidValuesException(number, 25);

            string difficulty = lines[25];
            if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard" && difficulty != "impossible")
                throw new CorruptedInvalidValuesException(number, 26);

            if (!int.TryParse(lines[26], out var weaponId))
                throw new CorruptedFormatSaveFileException(number, 27, typeof(int));

            if (!Shop.Shop.ITEMS.ContainsKey(weaponId))
                throw new CorruptedInvalidValuesException(number, 27);

            IItem weapItem = Shop.Shop.ITEMS[weaponId];
            if (weapItem is not IWeapon weap)
                throw new CorruptedInvalidValuesException(number, 27);


            if (!int.TryParse(lines[27], out var armourId))
                throw new CorruptedFormatSaveFileException(number, 28, typeof(int));

            if (!Shop.Shop.ITEMS.ContainsKey(armourId))
                throw new CorruptedInvalidValuesException(number, 28);

            IItem armItem = Shop.Shop.ITEMS[armourId];
            if (armItem is not IArmour arm)
                throw new CorruptedInvalidValuesException(number, 28);


            if (!DateTime.TryParseExact(lines[28], "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None,
                out var dateTime))
                throw new CorruptedFormatSaveFileException(number, 29, typeof(DateTime));

            if (!int.TryParse(lines[29], out var gameLevel))
                throw new CorruptedFormatSaveFileException(number, 30, typeof(int));
            if (gameLevel < 0 || gameLevel > 7)
                throw new CorruptedInvalidValuesException(number, 30);

            List<int> enemies = new List<int>();
            var integersString = lines[30].Split(",");
            foreach (var integer in integersString)
            {
                if (!int.TryParse(integer, out var enemiesNumber))
                    throw new CorruptedFormatSaveFileException(number, 30, typeof(int));
                enemies.Add(enemiesNumber);
            }

            if (!int.TryParse(lines[31], out var pastSelvesCount))
                throw new CorruptedFormatSaveFileException(number, 32, typeof(int));

            if (pastSelvesCount > 3 || pastSelvesCount < 0)
            {
                throw new CorruptedInvalidValuesException(number, 32);
            }

            if (lines.Length < 32 + 6 * pastSelvesCount)
                throw new CorruptedLengthException(number, lines.Length);

            var pastSelves = new List<PastSelf>();
            for (int i = 0; i < pastSelvesCount; i++)
            {
                var currentName = lines[32 + 7 * i];
                if (!double.TryParse(lines[33 + 7 * i], out var currentAttack))
                    throw new CorruptedFormatSaveFileException(number, 34 + 7 * i, typeof(double));
                if (currentAttack < 0)
                    throw new CorruptedInvalidValuesException(number, 34 + 7 * i);

                if (!double.TryParse(lines[34 + 7 * i], out var currentDefense))
                    throw new CorruptedFormatSaveFileException(number, 35 + 7 * i, typeof(double));

                if (!int.TryParse(lines[35 + 7 * i], out var currentWeapon))
                    throw new CorruptedFormatSaveFileException(number, 36 + 7 * i, typeof(int));
                if (!Shop.Shop.ITEMS.ContainsKey(currentWeapon))
                    throw new CorruptedInvalidValuesException(number, 36 + 7 * i);
                var weaponItem = Shop.Shop.ITEMS[currentWeapon];
                if (weaponItem is not IWeapon pastSelfWeapon)
                    throw new CorruptedInvalidValuesException(number, 36 + 7 * i);

                if (!int.TryParse(lines[36 + 7 * i], out var currentArmour))
                    throw new CorruptedFormatSaveFileException(number, 37 + 7 * i, typeof(int));
                if (!Shop.Shop.ITEMS.ContainsKey(currentArmour))
                    throw new CorruptedInvalidValuesException(number, 37 + 7 * i);
                var armourItem = Shop.Shop.ITEMS[currentArmour];
                if (armourItem is not IArmour pastSelfArmour)
                    throw new CorruptedInvalidValuesException(number, 37 + 7 * i);

                if (!double.TryParse(lines[37 + 7 * i], out var currentHealth))
                    throw new CorruptedFormatSaveFileException(number, 38 + 7 * i, typeof(double));
                if (currentHealth < 0)
                    throw new CorruptedInvalidValuesException(number, 38 + 7 * i);

                var currentDescription = lines[38 + 7 * i];

                var pastSelf = new PastSelf(currentName, currentAttack, currentDefense, pastSelfWeapon, pastSelfArmour,
                    currentHealth, currentDescription, i + 1);
                pastSelves.Add(pastSelf);
            }

            var humanPlayer = new HumanPlayer(name, humanLevel, experiencePoints, innateAttack,
                innateDefense, innateCriticalChance, innateArmourPenetration, manaRegenerationRate,
                mana, health, maximumHealth, totalMana, inventory, gold, school, sanity, maximumSanity,
                killCount, difficulty, weap, arm, pastSelves);
            return new Tuple<HumanPlayer, int, List<int>, DateTime>(humanPlayer, gameLevel, enemies, dateTime);
        }
    }
}