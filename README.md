Welcome to Descend into Sleep, my C# RPG Game! Here are the patch notes for the past few patches

<b>PLAY THE GAME WITH THE CONSOLE ON FULL SCREEN TO REDUCE THE NUMBER OF BUGS</b>

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
