Welcome to Descend into Sleep, my C# RPG Game! Here are the patch notes for the past few patches

<b>PLAY THE GAME WITH THE CONSOLE ON FULL SCREEN - Game becomes buggy when the Console is not maximized
  
Some bosses may be unfair, the last bosses are all overtuned or undertuned and some sidebosses are extremely overtuned or extremely undertuned - and that's fine because I'll tune their number with time, so if you feel a boss is unfair or if you find a bug, just bring it up to me
  
To run the game, you'll need to download the latest release (1.0.0.0) and then run into command line "Descend Into Sleep.exe"</b>

<b>Patch Notes 1.0.1</b>
- Shop looks nicer now, less clunky
- Each items stores its own price instead of the shop storing it
- Focus has been buffed: MinimumSanityRestored = 10 -> 25 and MaximumSanityRestored = 41 -> 46

<b>Patch Notes 1.0.0</b>

Finally, I consider that all implementations that I wanted to implement were done, so I am releasing the 1.0.0.0 Version.
Of course, there are going to be several bugs and there will be balance patches required to balance the game, but the implementations are mostly done!
- <i>TentacleMenace</i>'s <i>Armour</i> was nerfed from 10 to -100
- Fixed a bug where you couldn't buy items containing the letter X
- Fixed a bug where <i>Titan's Findings</i> wouldn't restore <i>Sanity</i> on hit
- Now, to buy multiple items you must use * instead of X
- <i>RandomHelper</i> helps us generate random stuff and decide random outcomes instead of reusing the same code every time
- Each character has a bool <i>Spared</i>, a list of <i>Actions</i> which must be performed in a certain order and a <i>ChanceOfSuccessfulAct</i> in order to spare it
- You can <i>Act</i> in a fight and <i>Spare</i> a character instead of killing it
- You can't run multiple instances of <i>Descend Into Sleep</i> by running a <i>Mutex</i> with a specific name
- <i>Game</i> now exists through <i>Exit Game Exceptions</i> so we can free the mutex
- We have 4 possible <i>Exit Game Exceptions</i>: <i>Game Over Exception</i> and one for each ending: <i>Pacifist/Neutral/Genocide Ending Exception</i>
- Some <i>SideEnemy</i> classes are now abstract
- <i>Spareable</i> enemies are yellow when checking their stats
- When you <i>Act</i> successfully in combat, the <i>Attack</i> of the enemy is reduced according to a formula given by <i>FormulaHelper</i> for which I have studied several functions, but I observed one function that is best for this purpose
- <i>Serialization</i> is now done manually instead of using JSON, so <i>corruption</i> is easier to spot and easier to point out to the user
- Depending on <i>file corruption</i> type, we can have three types of exceptions: <i>Invalid Length Exception</i> (file is too short), <i>Invalid Values Exception</i> (values in the file are not valid) and <i>Invalid Format Exception</i> (format is not proper on a line, for example we need an integer but we have a string literal there)
- All sums of strings are now done using string <i>interpolation</i> because it looks nicer
- <i>PastSelf</i> is now its own class for one of the endings
- Now we don't open the files every time we can, but we keep the information stored after start-up and change it when we save, so a change there while the app is running can cause chaos, but it runs way faster than before
- Now, whenever a stat changes (Health, Attack, etc.) its new value is written for clarity and debugging purposes
- Some unnecessary functions were removed
- Everything printed on the console is rounded to 2 digits
- <i>Genocide</i> and <i>Neutral</i> ending bosses added
- Now you can level up multiple times after a fight
- Now there's a finite number of sidebosses and they're saved in the file
- Instead of <i>IsAutoattacker</i>, now it works by a <i>GetAutoattackOdds</i> which tells us how likely a character is to attack
- The list of enemies is given to a level when you load
- <i>Acts</i> have been implemented for each boss
- Due to a bug with the <i>ConsoleHelper</i>, I was required to change the following ability names due to their names being too long: <i>NatureCleansing</i> was changed into <i>Empower</i>, <i>UltimateMadness</i> was changed into <i>Hysteria</i> and <i>ChaoticReflection</i> was changed into <i>Reflection</i>
- Now you start with 30 <i>Gold</i> instead of 0 <i>Gold</i> so you don't depend on RNG early on
- <i>SonOfTheSun</i> was renamed into <i>FlameOfTheSun</i>
- <i>Experience</i> gained after each battle isn't written anymore on the Console 
- <i>ExperiencePoints</i> were buffed so that you don't have to grind for Maximum Level
- Fixed a bug with <i>Madness Hit</i> where it would consider <i>MaximumSanity</i> to always be 100
- Now you can see your <i>Inventory</i> as a table and choose items to equip with the keys instead of having to write their names
- <i>Effect</i> is now called <i>Active</i> so an item can have either an <i>Active</i> or a <i>Passive</i>
- <i>SteelPlateau</i> and <i>BoilingBlood</i> now have <i>Passives</i> instead of <i>Actives</i> because it makes more sense
- The last boss can realistically be beaten now only by going through all his phases, because <i>Reflector</i> Items do not take ability damage
- Changed a small detail in the <i>Genocide Route</i> regarding the dialogue
- Now the descriptions of abilities and items are detailed with regard to the numbers
- Now you can see abilities description while in combat
- Now it will show an error message when the inventory is empty and you're trying to sell something
- The order of the actions shown when trying to <i>Act</i> is randomized so you can't figure out the exact order easily
- <i>Save File</i> errors now mention their Save File number, not their exact file path
- Now <i>Enemies</i> lose attack when successfully acting depending on their unbuffed attack value

<b>Patch Notes 0.4.0</b>
- Fixed a bug where you couldn't sell any item
- <i>TemArmour</i> cost nerfed from 150 Gold to 450 Gold
- <i>Eclipse</i> cost nerfed from 150 Gold to 400 Gold
- <i>SanityPotion</i> cost buffed from 200 Gold to 100 Gold
- <i>Amalgamation</i>'s Attack Value buffed from 1 to 7
- <i>SpaghettiMonster</i>'s Attack Value buffed from 1 to 10
- Fixed a bug where you could gain more mana than the Maximum amount you could have
- Nerfed <i>TentacledMenace</i>'s attack from 30 to 20
- Now <i>Armours</i> can have extra <i>Health</i> and <i>Sanity</i> on them
- <i>Characters</i> now have a <i>MaximumSanity</i> field due to it not being constantly 100
- Now you can see ALL stats on your characters and on all <i>Items</i>
- All protected fields were made private in the <i>HumanPlayer</i> class
- New Item: <i>Will Power</i> which increases your <i>Sanity</i> by 50

<b>Patch Notes 0.3.0</b>

- Fixed <i>Undying Will</i> having no description
- Fixed items showing without any description in the shop
- Fixed items showing ugly in shop due to newlines
- Fixed abilities not costing mana when cast by the <i>HumanPlayer</i>
- Made the menu traversable by arrows instead of having to type out the commands
- <i>HumanPlayer</i> casts abilities differently compared to usual mobs now
- Class <i>ConsoleHelper</i> helps us with the arrow traversal
- Fixed a bug which crashed your game when you tried to use <i>Actions</i> in a <i>Fight</i>, but you had no <i>Actions</i>
- Fixed a bug where <i>Checking Stats</i> would skip a turn
- Now when checking stats, you can see your <i>Max Health</i> and your <i>Current Health</i>, both rounded up to 2 digits
- Fixed a bug where you couldn't back away after choosing <i>Actions</i> in <i>Combat</i>
- Made all numbers look nicer by rounding them up to <i>2 decimals</i>
- Fixed a bug where if you tried to do an <i>Action</i> in a fight, but you had none, it would be stuck in an endless loop
- Now game exits on <i>Environment.Exit(0)</i> (there might appear bugs in this domain - just contact me with the Exceptions stack, in that case)
- Each Character now has a <i>Level</i>
- You gain <i>Gold</i> and <i>Experience</i> at the end of a <i>Fight</i> depending on your enemy's <i>Level</i>
- Fixed a bug which wouldn't grant you <i>Experience</i> and <i>Gold</i> if you killed an enemy during its turn
- Made the Game playable on <i>MacOS</i> and <i>Unix Distributions</i> as well, hopefully (I cannot test that)
- <i>SaveFiles</i> are now singletons and have their constructor private
- Added class <i>FileHelper</i> which helps us check whether our app directory exists and our whether our save files exist 
- Fixed a bug where Save File wouldn't close after opening due to the stream not closing
- All Items are now <i>Singletons</i>; there exists only one instance of each item
- Fixed a bug where <i>MacOS</i> was swapped with <i>Linux</i>, thus crashing the game on both platforms
- Fixed a bug where the <i>Singleton</i> items would be initialsied in the <i>LanguageHacker</i> item and in the <i>LastBossFight</i>
- <i>Levels</i> are now initialised only after <i>Player</i> is initialised3
- Fixed a bug where you couldn't fight any <i>SideBoss</i> in the last <i>Level</i>
- Fixed a bug where getting hit by a <i>FireSideEnemy</i> would print your stats
- Made all main enemies <i>Singletons</i>
- <i>MainEnemies</i> is now a list instead of a single object, due to the last level having multiple of them
- Depending on your type of ending, the colour of the console is different 
- Added Table to simulate GUI and made it work like previously

<b>Patch Notes 0.2.0</b>

I will start with the most important feature of the new patch - the full change of engine used to write the game, as I have moved on from Python to C# to improve the time in which the game runs, and as well to have a possibility to create a graphical user interface if I ever decide to using Unity. In refractoring the code, which took me a long time, I managed to fix some bugs caused by, frankly, my lack of attention, but I also had to give up on Texttable, as I didn't find a proper replacement for it in C#.

Next up, I'll present a new feature - explore - which helps you find mobs at any levels in case you get stuck; however, you now require a certain level to proceed to a boss
the mechanic of leveling was changed from raw leveling to experience points - so you have to fight several mobs to proceed to a boss but you get stuck a lot harder
this new mechanic of leveling has allowed us to change from 7 previous available levels for our human player to 34 - that means, more abilities! (well, not really)
now, instead of creating an object of the class ability and applying it whenever you cast an ability, you have all of them as a player and you just call them, so each human player has their own abilities and now you can level them up for better effects (usually linear, but sometimes exponential)

<i>Experience Points</i> are gained similar to <i>Gold</i> after a fight, and as Side Enemies are easier to kill and more abundant, you earn both less <i>Gold</i> and less <i>Experience</i> from killing them to maintain balance

Regarding abilities, you have to make a decision now at level 4 to choose the school you want to follow for the rest of the game, and you are going to learn abilities specific to that school for the rest of the game, as well

The schools you can choose from are:

<i>Self-harm</i> or <i>Warlock</i>, based on damaging oneself and going a tank build in order to deal a lot of damage  - the more health you are missing, the more damage you are dealing
it has abilities which allows them to abuse the fact that it has a lot of armour and a lot of HP, with the ultimate being the cherry on top

<i>Nature</i> or <i>Druid</i>, based on acting both as a tank and as a damage dealer, like a jack of all trades - not too tanky, but not too much damage either; the ultimate shapeshifts yourself into a mob which has increased attack and defense points scaling with your attack and defense points

<i>Fire</i> or <i>Mage</i>, based on damaging the hero over a few turns and reducing his defense points
it has abilities which do not have really an immediate effect, but overall, they are better than pure attacks with the ultimate being one of the best scaling abilities in the game as of now

The mobs you have to fight to advance in level become progressively stronger the higher level you are up to the point they are near unbeatable (blame the terrible game designer for that).
I am more than open to change the numbers and balance them accordingly in order for the game to be beatable. The mobs also act as a sneak peek to the main boss of that level as they use almost the same mechanics and you need to tread on them carefully.

Due to me adding so many new levels, I was required to make the bosses stronger so now even the first boss can be quite a challenge for someone who does not know the fight.
I've also added difficulties for the game, which only changes how much stronger you become as levels pass by - for example, by playing with the impossible difficulty, you do not gain any innate attack, armour or health when leveling up - so you are stuck with 20 health for the rest of the game - the mode lives up to its name, really (but I really need to tweak it anyway).

As I haven't as of yet reached the last level, it might be quite buggy due to it running differently than the rest of the levels - but it might not be an issue because I don't think it's reachable with the current numbers and I am working 24/7 to make the game playable.
For my next update, I will try making Weapons and/or Abilities Singletons so I do not create a new instance every time I need them - I just need to figure out how to save them in files - and tweak numbers so the game is beatable.


<b>Patch Notes 0.1.6</b>
- Small bugfixes regarding the last fight, now fixed
- <i>TacosWhisper's</i> effect damage buffed, now deals twice the damage on its fourth shot

<b>Patch Notes 0.1.4</b>
- You can now have up until 10 save files, compared to one
- You can see the state of each save file, so you know where to load
- Save files are now encrypted and are harder to modify by hand
- Items no longer have IDs, as they do not need it
- Specifications to the functions and classes coming soon

<b>Patch Notes 0.1.3</b>
- <i>DoubleEdgedSword's</i> attack nerfed from 35 to 20
- <i>Xalatath's</i> attack nerfed from 15 to 10
- Sell values now nerfed - you only gain 75% gold back instead of 100% gold when selling an item
- Added some QoL features for the last level
- Chance of <i>Fire Deflector</i> deflecting DOT effects lowered from 25% to 10%
- Added missing description to <i>CCImmunity</i> and <i>TrueDamage</i>
- <i>TwoHandedMace's</i> attack nerfed from 75 to 45, <i>TwoHandedMace's</i> cost nerfed to 750 gold and it can be bought from the shop only from level 3

<b>Patch Notes 0.1.2</b>
- Inventory is now printed through a texttable! (Easier to see)
- Out of combat options are now split into Game Options, Player Options and Shop Options
- Added a new option for Player Options: drop current item
- Added the possibility to not wear any armour or weapon at a given time
- Fixed some typos when using human abilities

<b>Patch Notes 0.1.1</b>
- Removed <i>Experience Potion</i>
- Changed Drops after <i>Spaghetti Monster</i> to <i>GrainOfSalt</i> instead of <i>Experience Potion</i>
- Items in shop no longer disappear after buying them
- Fixed a bug where Max Health would not save along with the rest of your character
- Buffed initial abilities so they're more useful; from linear growth, they went to exponential growth
- Added description to <i>TwoHandedMace</i>, which was missing
- <i>DoubleEdgedSword's</i> attack nerfed from 50 to 35
- <i>RadusBiceps's</i> attack buffed from 50 to 75
- <i>TwoHandedMace's</i> attack buffed from 50 to 75
- Added description to <i>FireDeflector</i>, which was missing
- Fixed a bug where DOT effects would carry from a fight to another one
- Nerfed <i>Grain Of Salt</i> healing per level from 10 to 7.5.
