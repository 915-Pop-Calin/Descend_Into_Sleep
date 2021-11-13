Welcome to Descend into Sleep, my C# RPG Game! Here are the patch notes for the past few patches

<b>Patch Notes 0.2.0</b>

I will start with the most important feature of the new patch - the full change of engine used to write the game, as I have moved on from Python to C# to improve the time in which the game runs, and as well to have a possibility to create a graphical user interface if I ever decide to using Unity. In refractoring the code, which took me a long time, I managed to fix some bugs caused by, frankly, my lack of attention, but I also had to give up on Texttable, as I didn't find a proper replacement for it in C#.

Next up, I'll present a new feature - explore - which helps you find mobs at any levels in case you get stuck; however, you now require a certain level to proceed to a boss
the mechanic of leveling was changed from raw leveling to experience points - so you have to fight several mobs to proceed to a boss but you get stuck a lot harder
this new mechanic of leveling has allowed us to change from 7 previous available levels for our human player to 34 - that means, more abilities! (well, not really)
now, instead of creating an object of the class ability and applying it whenever you cast an ability, you have all of them as a player and you just call them, so each human player has their own abilities and now you can level them up for better effects (usually linear, but sometimes exponential)

Regarding abilities, you have to make a decision now at level 4 to choose the school you want to follow for the rest of the game, and you are going to learn abilities specific to that school for the rest of the game, as well

The schools you can choose from are:

Self-harm or Warlock, based on damaging oneself and going a tank build in order to deal a lot of damage  - the more health you are missing, the more damage you are dealing
it has abilities which allows him to abuse the fact that it has a lot of armour and a lot of HP, with the ultimate being the cherry on top

Nature or Druid, based on acting both as a tank and as a damage dealer, like a jack of all trades - not too tanky, but not too much damage either; the ultimate shapeshifts yourself into
a mob which has increased attack and defense points scaling with your attack and defense points

Fire or Mage, based on damaging the hero over a few turns and reducing his defense points
it has abilities which do not have really an immediate effect, but overall, they are better than pure attacks with the ultimate being one of the best scaling abilities in the game as of now

The mobs you have to fight to advance in level become progressively stronger the higher level you are up to the point they are near unbeatable (blame the terrible game designer for that).
I am more than open to change the numbers and balance them accordingly in order for the game to be beatable. The mobs also act as a sneak peek to the main boss of that level as they use almost the same mechanics and you need to tread on them carefully.

Due to me adding so many new levels, I was required to make the bosses stronger so now even the first boss can be quite a challenge for someone who does not know the fight.
I've also added difficulties for the game, which only changes how much stronger you become as levels pass by - for example, by playing with the impossible difficulty, you do not gain any innate attack, armour or health when leveling up - so you are stuck with 20 health for the rest of the game - the mode lives up to its name, really (but I really need to tweak it anyway).

As I haven't as of yet reached the last level, it might be quite buggy due to it running differently than the rest of the levels - but it might not be an issue because I don't think it's reachable with the current numbers and I am working 24/7 to make the game playable.
For my next update, I will try making Weapons and/or Abilities Singletons so I do not create a new instance every time I need them - I just need to figure out how to save them in files - and tweak numbers so the game is beatable.


<b>Patch Notes 0.1.6</b>
- Small bugfixes regarding the last fight, now fixed
- TacosWhisper's effect damage buffed, now deals twice the damage on its fourth shot

<b>Patch Notes 0.1.4</b>
- You can now have up until 10 save files, compared to one
- You can see the state of each save file, so you know where to load
- Save files are now encrypted and are harder to modify by hand
- Items no longer have IDs, as they do not need it
- Specifications to the functions and classes coming soon

<b>Patch Notes 0.1.3</b>
- DoubleEdgedSword's attack nerfed from 35 to 20
- Xalatath's attack nerfed from 15 to 10
- Sell values now nerfed - you only gain 75% gold back instead of 100% gold when selling an item
- Added some QoL features for the last level
- Chance of Fire Deflector deflecting DOT effects lowered from 25% to 10%
- Added missing description to CCImmunity and TrueDamage
- TwoHandedMace's attack nerfed from 75 to 45, TwoHandedMace's cost nerfed to 750 gold and it can be bought from the shop only from level 3

<b>Patch Notes 0.1.2</b>
- Inventory is now printed through a texttable! (Easier to see)
- Out of combat options are now split into Game Options, Player Options and Shop Options
- Added a new option for Player Options: drop current item
- Added the possibility to not wear any armour or weapon at a given time
- Fixed some typos when using human abilities

<b>Patch Notes 0.1.1</b>
- Removed Experience Potion
- Changed Drops after Spaghetti Monster to GrainOfSalt instead of Experience Potion
- Items in shop no longer disappear after buying them
- Fixed a bug where Max Health would not save along with the rest of your character
- Buffed initial abilities so they're more useful; from linear growth, they went to exponential growth
- Added description to TwoHandedMace, which was missing
- DoubleEdgedSword's attack nerfed from 50 to 35
- RadusBiceps's attack buffed from 50 to 75
- TwoHandedMace attack buffed from 50 to 75
- Added description to FireDeflector, which was missing
- Fixed a bug where DOT effects would carry from a fight to another one
- Nerfed Grain Of Salt healing per level from 10 to 7.5.
