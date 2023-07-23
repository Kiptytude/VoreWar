﻿static class DefaultTooltips
{
    internal static string Tooltip(int tooltip)
    {
        switch (tooltip)
        {
            case 1:
                return "Shows the current race";
            case 2:
                return "The number of villages the empire starts with - set to 0 to disable this empire";
            case 3:
                return "Have the AI control this empire?";
            case 4:
                return @"Which AI to use in the strategic mode - Passive strategic AI only builds garrisons and no armies, also starts towards the center.
Legacy strategic AI starts with small armies and works its way up, but always plays fairly poorly
Basic is equivalent to the AI from 15E, it plays at a decent level 
Advanced is a slightly tweaked version of basic to play at a slightly higher level.
Cheating is designed for advanced players. It gets a moderate amount of extra gold each turn, and gains exp at a slightly faster rate.
Further levels of cheating boost that further, and also effect starting Exp.   Brutals can even feed off each other to an extent, so be warned.";
            case 5:
                return @"Legacy tactical AI plays somewhat wastefully, with poor target prioritization
Full Strength will prioritize damaged targets and generally plays pretty well";
            case 6:
                return "Allows an empire's units to vore other units.  Also affects whether they can devour at the strategic level";
            case 7:
                return "Any empires with the same team number will be allied together";
            case 8:
                return "Affects their displayed color on the strategic map";
            case 9:
                return "Causes the size of tactical maps to be determined by how many units are participating in the battle";
            case 10:
                return @"The base amount of experience required to gain a level
Low values create units that rapidly spike in power, high values generate a more level playing field";
            case 11:
                return @"The rate at which the experience you need to level up increases as you level
Higher values slow down the rate at which super armies form, by slowing down leveling as you level";
            case 12:
                return @"Whether units will regurgitate a friendly unit that somehow ends up in their stomach
Happens if you eat an enemy that's eaten one of your troops, and your unit escapes from the enemy's stomach
Note that surrendered units ignore this setting";
            case 13:
                return "How much gold all the empires start with.  They also gain their normal income for the first turn in addition to this value";
            case 14:
                return @"Allows you to pick a saved map from the map editor to start on.  
It will use the exact terrain, and generate default villages at all the places villages are placed.
Right now it will ignore any armies that exist on the map";
            case 15:
                return "Cancels out of the currently selected map, and causes the game to use a randomly generated map again";
            case 17:
                return @"Controls the % of units that will use ranged instead of melee";
            case 18:
                return @"Controls what % of units will have the higher tier weapons";
            case 19:
                return @"Controls the maximum army size for this empire.  
Note that above 32 can result in significantly slower battles, both during play, and quick resolved";
            case 20:
                return @"Controls the order in which empires act.  Lower numbers act earlier in the turn.
Anywhere there is a tie, it will be sorted from top to bottom, ";
            case 21:
                return "Increases the amount of gold all villages generate (the base value is 50% of the population as gold, and 80% of that for foreign races)";
            case 22:
                return @"Enables Fog of War (You can only see enemy villages and armies that are within 2 tiles armies and villages that belong to your team)
Terrain is always visible though, so you do know where villages are, just not who owns them";
            case 23:
                return @"Enables the Vagrant monster packs";
            case 24:
                return @"Changes the default villagers allowed per farm (default is 6, for a max pop of 48)
Capitals get a 50% higher population cap";
            case 25:
                return @"Causes teams to spawn together, and can be used without teams to enforce fixed spawns even if teams are not being used
The starting spots are assigned counter-clockwise from low team number to high team number";
            case 26:
                return "Sets a soft level cap.  Every level after this will have the experience required doubled from the previous level";
            case 27:
                return "Sets a hard level cap.  This will be the absolute maximum level a unit can reach.";
            case 30:
                return @"Enables weight gain for units, only works for some races.
For a more pronounced effect and allow more room to grow, you might want to have breast size and body size set to minimum for this";
            case 31:
                return @"Controls the amount of females vs males that appear.  
Does not retroactively affect already created units.";
            case 32:
                return @"Affects what % of all units end up as Futanari / Hermaphrodite.  
Does not retroactively affect already created units.";
            case 33:
                return @"Affects what % of all units wear clothes, does not prevent you from changing individual characters in the customizer.  Does not retroactively affect already created units.";
            case 34:
                return @"Breast size modifier for female units, if in the center, all types have an equal chance, but sliding it prevents one level per tick and changes the average size
Does not retroactively affect already created units.";
            case 35:
                return @"Affects the starting weight of units, this only affects certain races
Does not retroactively affect already created units if weight gain is on, or their weight has been manually changed.";
            case 36:
                return @"Changes the settings for the currently running game, or the game you are about to create, but leaves the global settings alone";
            case 37:
                return @"Changes the global settings, which affects any new games you start.
Loading a save game loads settings specific to that game, so you can have a save game that exists separately from your global content settings
Returning to the main menu resets your settings to your currently saved global settings";
            case 38:
                return @"Causes hair color to match fur color, Applies to Cats, Dogs, Foxes, Wolves, Bunnies, Tigers, Taurus, and Sergal.
Does not retroactively affect already created units.";
            case 39:
                return @"Allows females to use the 'masculine' hair types (short and spiky) on random generation
Only affects certain races
Does not retroactively affect already created units.";
            case 40:
                return @"Allows males to use the 'feminine' hair types (long or the bows) on random generation
Only affects certain races
Does not retroactively affect already created units.";
            case 41:
                return @"Opens up clothing options for females that don't cover the breasts (currently shorts with no top, and a bikini with no top)
Does not retroactively affect already created units.";
            case 42:
                return @"Allows very large breast sizes, these don't play well with the current bikinis
Does not retroactively affect already created units.";
            case 43:
                return @"Allows very large dick sizes
Does not retroactively affect already created units.";
            case 44:
                return @"Whether units acquired using the subjugation pen will default to wearing rags, they can still be changed manually
Does not retroactively affect already created units.";
            case 45:
                return @"Whether corpses will be visible, or simply vanish on death
Does not retroactively affect already dead units.";
            case 46:
                return @"Allows units to eat corpses, Be warned - this greatly affects balance
Units from either side can eat corpses, and after the battle, the remaining troops will eat all of the remaining corpses on the field automatically
Visible corpses must be on to enable this";
            case 47:
                return @"Imports the saved global content settings into this game
Useful for bringing old save games up to speed, but new games are automatically generated with the saved global settings";
            case 48:
                return "Bellies with prey in them will be animated";
            case 49:
                return "There's a brief skull that flashes up when a unit is digested that lasts under a second";
            case 50:
                return "Affects the percentage of burps when a unit has finished the absorption phase (digestion if that option is on)";
            case 51:
                return @"Whether Scat (i.e. poop) is generated when a unit is finishing absorbing
Also toggles any scat related tactical log messages on or off.";
            case 52:
                return @"Causes units to surrender when they receive what would normally be a killing blow, allowing them to be eaten, or possibly survive.
You can adjust the fractional odds of it occuring, to anywhere from 0% to 100% of the time.";
            case 53:
                return "Whether scat can contain bones (it's random whether or not they're visible)";
            case 54:
                return @"Uses the new unit graphics for the races that have it. (Most of the races except the first 8 or so only have new graphics)
<color=yellow>CAUTION: This will cause any race that has a different new graphic and old graphic to re-randomize all of its units' customizations</color>";
            case 55:
                return @"Affects the percentage of units that will spawn as fully furries - only for units using the New Graphics
Only applies to races that have the furry toggleable.
Does not retroactively affect already created units.";
            case 56:
                return @"Affects the percentage of units that will spawn with furry hands and feet
Only applies to races that have the furry toggleable.
Has no effect on units that spawn as fully furry";
            case 57:
                return @"Dick size modifier, if in the center, all types have an equal chance, but sliding it prevents one level per tick and changes the average size
Does not retroactively affect already created units.";
            case 58:
                return "Causes all Dicks to be invisible and hidden from view";
            case 59:
                return "Causes all Breasts to be invisible and hidden from view (may have weird results on clothed units)";
            case 60:
                return @"Causes Lamia to use tail as a second stomach (when they contain more than 2 full sized prey, their tail will start to get bigger and their belly will be smaller
If off, tail size is controlled by bodysize / affected by weightgain";
            case 61:
                return "Whether the genitals of furry units will use an alternate furry only sprite";
            case 62:
                return @"Whether furry or partially furry units will have the extra fluff on their arms and legs
Note that this fluff hides the seams, which look fine for half-furries, but are more noticeable on fully furries";

            case 67:
                return @"If you have the AI take your turn for you, it will take this long to move between squares to allow you to see what's going on at your preferred pace";
            case 68:
                return @"Player units take this long to move between squares";
            case 69:
                return @"The tactical AI will wait this long after every square moved to allow you to see what's going on at your preferred pace";
            case 70:
                return @"The tactical AI will wait this long after every attack action to allow you to see what's going on at your preferred pace";
            case 71:
                return @"The tactical AI will wait this long after every devour action to allow you to see what's going on at your preferred pace";
            case 72:
                return @"The strategic AI will wait this long after every square moved to allow you to see what's going on at your preferred pace";
            case 73:
                return @"Displays damage numbers when units are damaged, if this is off, they will simply flash red";
            case 74:
                return @"Allows game to run while in the background (window not in focus, or minimized)";
            case 75:
                return @"Displays the unit's level just under its healthbar";
            case 77:
                return "Uses green and red for base colors instead of blue and red.  Not recommended with subtle colors as green blends in with the grass";
            case 78:
                return "Asks if you're sure if you try to end your turn in tactical mode while units are still capable of moving";
            case 79:
                return "The battle will automatically pause at the very end of any battle that you're watching so that you can review the battlefield";
            case 80:
                return @"Enable unbirth

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 81:
                return @"Enable cock vore.  Turning this on will disable generic digestions affecting cock size if weightgain is on
But you can cock vore enemies to increase it

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 82:
                return @"Enable breast vore.  Turning this on will disable generic digestions affecting breast size if weightgain is on
But you can breast vore enemies to increase it

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 83:
                return @"Enables KuroTenko's mod that provide additional effects for breast vore, cock vore, and unbirth.  The particulars can be controlled by the settings below.
Since this affects vore types differently, it will affect balance in some cases.  
Was restored and expanded by Scarabyte, and should be in working order, though it's possible a bug might have slipped through.";
            case 84:
                return @"Causes the races to be different from each other with different traits that affect how they play
Right now each race has set traits, but random traits will be in at some point as well";
            case 85:
                return "The camera will scroll to the location of a battle before the battle starts";
            case 86:
                return @"Allied Surrendered units will be automatically eaten at the end of battle
Can help even the odds when auto-surrender is on and a weaker army might not kill as many attackers
Happens off-screen after the end of the battle";
            case 87:
                return @"Controls whether bones are generated at the end of the absorption phase
Note that scat overrides this as they both compete for the same slot and ground area";

            case 88:
                return @"Enables the Serpent monster packs";
            case 89:
                return @"Enables the Wyvern monster packs (consisting of adult wyvern and young wyvern)";
            case 90:
                return @"Enables the Compy monster packs (a small cockvore predator)";
            case 91:
                return @"If turned on, oral weight gain has a chance of raising the cock/breast size
Breastvore and cockvore always have a chance of raising their respective part.
Only currently functions if weight gain is on.";
            case 92:
                return @"Enables the Sky Shark monster packs";
            case 93:
                return "When a unit has cockvored another unit, its clothes will vanish until it is done";
            case 94:
                return "Controls how often an army will spawn, 0% per turn at the far left, and 100% each turn on the far right";
            case 95:
                return "Controls the maximum number of armies this side can field";
            case 96:
                return @"Controls how much experience they spawn with. Its base is the 80th percentile of experience for all non-leader units.
Then it multiplies by this percentage, and randomizes it a little
Keep in mind that some units like adult wyverns, are stronger for their exp, and some like the compy are weak for their exp";
            case 97:
                return @"If enabled, leaders will spawn with a random saved customization from your saved customizations
Only picks ones specifically saved from a leader - if none exist, they will just be randomly generated like usual
Also uses their name as well";
            case 98:
                return "Removes this race from this game";
            case 99:
                return "Herms now have a vagina in addition to their normal parts (They can unbirth if that's enabled)";
            case 100:
                return "If enabled experience will not be scaled based on the level difference between attacker and target and will always be the default";
            case 101:
                return "Will automatically skip any monster vs monster battles";
            case 102:
                return @"Enables the feral wolf (i.e. actual wolf instead of demi) monster packs";
            case 104:
                return @"Makes physical attacks half as likely to miss, affects combat balance, but good for those easily frustrated
Doesn't affect vore odds however";
            case 105:
                return @"Controls what team the monsters belong to, so you can make all of the monsters allied together
You could also make monsters allied with a particular player if you wish.
If changed, it takes effect when the monster's turn begins for compatibility reasons";
            case 106:
                return @"Shows notifications on events, the only event right now is a village being taken
Shows a small window near the top of the Strategic screen that only appears when there is an event.";
            case 107:
                return @"Makes the tooltips in tactical mode show some extra vore related info (escape rate, and the % of health / % absorbed";
            case 108:
                return @"Enables the cake (a 6 month celebration) monster packs";
            case 109:
                return "Controls how many movement points armies start with every turn";
            case 110:
                return @"Doesn't randomize the appearance of village fields
May require reloading your saved game to take effect";
            case 111:
                return @"Doesn't use any of the forests with white trunks
May require reloading your saved game to take effect";
            case 112:
                return "Digesting units make continous noise while digesting, this sound falls off rapidly with distance";
            case 113:
                return @"Controls what monsters do when they successfully attack a village
Devour and disperse, they will eat 2 villagers per monster, but will leave the village with at least 2 population and vanish
Devour and hold, takes control of village and devours each turn to keep pop at 50%
Complete devour - eats all population, then moves on or holds position or repopulates with monsters (not recommended for balance)
Fortify takes repopulation to an extreme and garrisons the village as well -- turning it near invincible in some cases";
            case 114:
                return @"How many attempts the monster will make to spawn each turn
Each one uses the given odds, but the rest will be skipped if the monster reaches its maximum army count";
            case 115:
                return "If enabled, young wyvern will spawn with the adults";
            case 116:
                return "If enabled, dark swallowers will spawn with the sharks";
            case 117:
                return "Whether discarded clothing will be created when a unit is digested, if it is wearing any clothing that has discard sprites";
            case 118:
                return "Sets the minimum number of units that will spawn in an army";
            case 119:
                return @"Sets the maximum number of units that will spawn in an army
This is capped behind the scenes at 48, but the army info screen can only display 32, and anything over that will likely result in performance issues during battles, both for autocalc and for watching";
            case 120:
                return "Sets the maximum number of armies a given empire can have at a time";
            case 121:
                return @"Whether or not the Annoynimouse's building mod is on.  It adds a lot of extra town buildings, but is not particularly concerned with balance.
Internally I call it the crazy buildings mod.
<color=yellow>(On default settings with all of the buildings built, a capital will generate roughly 4900 gold and have a population of 1753)</color>";
            case 122:
                return "How much gold per turn each gold mine generates";
            case 123:
                return "Upon its death, the leader will lose this many levels, keeping the same % to the next level";
            case 124:
                return "Upon its death, the leader will lose this percentage of its total experience";
            case 125:
                return @"Whether or not units can spawn with the more unconventional hair/fur colors
You can always manually move a unit to it, but this controls whether they can spawn that way or not";
            case 126:
                return @"Enable tail vore.  This is only available for certain races (Lamia, Succubus, Bees)

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 127:
                return @"Enables anal vore.  Anal vore currently puts units in the stomach like oral vore

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 128:
                return @"Controls whether goblin caravans spawn.  They wander around between villages, villages within 3 tiles of one will recieve 10 gold
Can also be attacked for a moderate sum of gold.";
            case 129:
                return @"Enables the Harvester monster packs (an insectoid / worm type)";
            case 130:
                return @"Controls the order in which monsters act.  Lower numbers act earlier in the turn.
Monsters can now be mixed in the normal empire order Anywhere there is a tie, it will be sorted from top to bottom, Races and then monsters ";
            case 131:
                return @"Controls how many mercenary houses the generator will spawn.  If one, it will be located in the center of the map like before
Places them in a simple manner, unconcerned with things like fairness";
            case 132:
                return @"Controls how many gold mines the generator will spawn.
Places them in a simple manner, unconcerned with things like fairness";
            case 133:
                return "Hides the unit preview picture that shows up in tactical mode that shows you what unit's info is being displayed";
            case 134:
                return @"If enabled, right clicking on any tile in tactical mode brings up the right click menu to allow casting tile type spells at that tile, 
if disabled, right click defaults to move, but you can bring up the right click menu on a tile by holding shift and right clicking
Right clicking on a unit will always bring up the menu, regardless of this setting";
            case 135:
                return @"If enabled, a male unit will have an erection while a unit is vored";
            case 136:
                return @"If enabled, a male unit will have an erection while a unit is cock vored";
            case 137:
                return @"Controls the names assigned to herm units, whether they will be given male names, or female names";
            case 138:
                return "If enabled, Collectors will spawn with the Harvesters";
            case 139:
                return @"Allows choosing between the old map generator and the current one.
The old one does not take any of the below inputs";
            case 140:
                return @"Controls the amount of water generated on the map, from nothing, to a very watery world
Note that it will generate paths from villages to the center of the map if a village is cut off
Mostly, anything beyond 2/3rds is likely to require these automatic land bridges.
You can potentially end up with a map with no needed paths on full water if it's a very cold map, as enough of the water will freeze.";
            case 141:
                return @"Controls the temperature of the map (cooler results in more ice, warmer in desert), as well as potentially affecting other terrain";
            case 142:
                return @"Makes the north of the map colder and the south of the map warmer to simulate climate zones
(North will be icy, and the south will more desert like)";
            case 143:
                return @"If off, teams will be locked to their set values.  If on, nations can broker peace agreements or alliances, or split off from their team
Attacking someone directly will upset them and their allies, but their enemies will hold you in higher regard
Relations will slowly move towards certain values depending on whether you're at war, peace, or alliance";
            case 144:
                return @"Enables the Voilin monster packs";
            case 145:
                return @"Enables the Bat monster packs";
            case 146:
                return "Disables all breast sprites for lizards (and Kobolds, as they are pretty lizardy)";
            case 147:
                return @"Controls the order of the Races, either all races alphabetical, races alphabetical but separated by type (main, merc, monster, special merc), or just in the game's internal order";
            case 148:
                return @"Makes it so that the info card only displays the final stat, instead of the final stat and the base stat";
            case 149:
                return "Makes the camera scroll if the mouse cursor is near the edge of the screen.  Not recommended if not in full-screen";
            case 150:
                return @"An alternate scat system created by dddddd2 with more variety.";
            case 151:
                return "Armies are prevented from moving during the first turn to allow the building of garrisons";
            case 152:
                return "Units will slowly lose weight over time (20% chance per turn each to drop a size of body/breast/dick)";
            case 153:
                return @"Enables the Frog monster packs";
            case 154:
                return @"Controls what gender females are attracted to
Right now this only affects flirting messages in the tactical combat log
Always attracted to herms if they exist to keep logic simple";
            case 155:
                return @"Controls what gender males are attracted to
Right now this only affects flirting messages in the tactical combat log
Always attracted to herms if they exist to keep logic simple";
            case 156:
                return @"Whether lewd actions will show up in the tactical combat log";
            case 157:
                return "Whether hard vore messages show up (i.e. stronger references to acids, etc)";
            case 158:
                return "Controls the % chance for each category that weight will be lost every turn, only happens if weight loss is on";
            case 159:
                return @"Affects the starting values and values it trends towards for the diplomatic relations between races
Goes in the order of Friendly - Normal - Suspicious - Distrustful
At distrustful it will be rather difficult for peace or alliances to be formed
Unforgetting is a special type that causes relations to not try to normalize (i.e. they remember everything that's ever happened to them)";
            case 160:
                return "Enables the Dragon monster packs (1 Dragon and the rest as kobolds)";
            case 161:
                return @"Enables the Dragonfly monster packs";
            case 162:
                return @"Controls the amount of forests generated on the map, note that there are many factors involved so consider this a suggestion rather than an absolute % slider
Also note that desert forests do not exist so this slider has no effect on sandy terrain";
            case 163:
                return @"Controls the amount of swamps generated, note that there are many factors involved so consider this a suggestion rather than an absolute % slider
Note that frozen swamps and desert swamps do not exist, so this only affects grassy terrain";
            case 164:
                return @"Controls the amount of hills generated on the map, note that there are many factors involved so consider this a suggestion rather than an absolute % slider
Affects mountains as well as hills, so extreme values will start to close off terrain a little bit";
            case 165:
                return @"Prevents all AI from retreating in any tactical battle";
            case 166:
                return @"Allows the AI to hire special Mercenaries (though it will be rare)";
            case 167:
                return "If enabled, prey will be freed with 2 to 5 AP, rather than having full AP.   Enough to allow them to defend themselves and reposition, but not go flying off.";
            case 168:
                return @"Affects odds of vore success, note that this can affect the balance between races";
            case 169:
                return @"Affects odds of prey escaping, note that this can affect the balance between races";
            case 170:
                return "Enables the Twisted Vines monster packs";
            case 171:
                return "Enables the Fairy monster packs";
            case 172:
                return "Controls whether monsters will drop spells randomly when defeated.  Between 0 and 2 drop per monster army.";
            case 173:
                return @"Controls how the enhanced breast for sprites for certain races that have different left and right sprites pick which side they put prey in.
Randomly, Picked (only for player units, otherwise random), or shared (evenly divided)";
            case 174:
                return @"Allows your own units the option to attack / vore friendly units.  
They will only do it at your command, the AI doesn't attack friendlies.
It's recommended to turn off auto-regurgitate friendlies if you plan to use this to vore";
            case 175:
                return "Controls how often events will show up at the start of a player turn, note that there are about 25 events, so it will eventually run out if repeat is not enabled";
            case 176:
                return @"Controls whether the player events above are allowed to repeat within a particular game (otherwise only 1 of each event can trigger for each player)
Only applies to built in events, and not the custom events defined in the events.txt , those automatically repeat as they are considered to be less significant";
            case 177:
                return @"Controls how often AI centric events happen - They often involve relations changing, and occasionally gold changing hands, or rebels spawning
Note that this is the odds for a single one happening, so you'll want this low with few races, and higher with many races
Events have a negative trend, so having it set too high for the size will lead to mostly war";
            case 178:
                return "Enables the Ant monster packs";
            case 179:
                return @"Controls how often custom events (specified in the events.txt) happen when an event is chosen
If it has trouble finding an event that hasn't happened, it will use a custom event even if it picks a built-in
(Even at 0% custom, custom events will eventually end up regularly happening if it runs out of events and you don't have event repeat on)";
            case 180:
                return "Controls the maximum level of spells dropped from monsters or goblins, or that spawn on mercenaries or adventurers, or given by the mad scientist trait.  Note that the maximum also depends on the monsters level, with level 4 spells not generally appearing until after level 10, but this clamps that to your maximum";
            case 181:
                return @"If enabled, units will have a 25% chance of switching sides when auto-surrender triggers, available after the battle
Note that units that are leaders, eternal, summoned or saved copies are immune to this check
Also, is mostly pointless if you have surrendered units are automatically eaten checked.";
            case 182:
                return @"Controls the average amount of water present on tactical maps
From virtually no water all the way on the left, to about as much water as it can realistically do while still creating valid maps on the right (will generate a blank map if it fails to make a good map enough times)
Note that the snow tileset has no water";
            case 183:
                return @"Controls how scattered the water that exists is.   All the way to the left will result in many small pools and lakes, while all the way to the right will generally result in 1 or 2 big lakes, though it's scattered enough that there might not even be one on the map";
            case 184:
                return "Traits in here will be automatically added to leaders when created\n So for instance you could make all leaders large\nCan add multiple traits i.e. \"Eternal, Large\"\nIf you're not sure if you added a trait right, save and reopen the content settings and real traits will still be there ";
            case 185:
                return "Traits in here will be automatically added to all males of any race\n So for instance you could make all leaders large\nCan add multiple traits i.e. \"Eternal, Large\"\nIf you're not sure if you added a trait right, save and reopen the content settings and real traits will still be there ";
            case 186:
                return "Traits in here will be automatically added to all females of any race\n So for instance you could make all leaders large\nCan add multiple traits i.e. \"Eternal, Large\"\nIf you're not sure if you added a trait right, save and reopen the content settings and real traits will still be there ";
            case 187:
                return "Traits in here will be automatically added to all herms of any race\n So for instance you could make all leaders large\nCan add multiple traits i.e. \"Eternal, Large\"\nIf you're not sure if you added a trait right, save and reopen the content settings and real traits will still be there ";
            case 188:
                return "AI to AI relations are locked, diplomacy will only change to and from players.  Useful if you want to group the ai into teams and have them stay that way while still having a flexible player";
            case 189:
                return @"The number of empty villages to place on the map, available for claiming by any empire.
They will generally appear on the borders between empires, but aren't really placed in a perfectly fair manner";
            case 190:
                return @"The AI will on rare occasions be given one of the available special mercenaries for free when they create an army.
This obviously adds difficulty to the game and adds a considerable amount of randomness, but can be used to spice up a game.
Designed to be a way to get more of them in play if you don't usually buy them yourself, but without having the AI go majorly out of their way to get them.";
            case 191:
                return "Enables the Gryphon monster packs";
            case 192:
                return "Causes lava and Volcanic terrain to skip using the terrain blender, this will get rid of the grass border between lava and volcanic, but makes them the old blocky style.  Can be useful if you have a lot of lava and volcanic terrain.";
            case 193:
                return "Controls whether units are allowed to defect to rejoin their home race when they battle against an empire with their race.\nThe odds decrease as levels increase, and it doesn't happen after about 40 will or so";
            case 194:
                return "Enables the Slug monster packs - it's a pack of four different slugs with complimentary abilities.";
            case 195:
                return "Enabling this will prevent weight gain from letting boobs/dick/weight go past whatever limits of random generation you set in the race editor.";
            case 196:
                return @"For compatibility reasons, oral vore is permanently enabled, though you can adjust its weight using the weight system.

The weights affect how often the AI will choose one in comparison to the others.  
(I.e. if unbirth is high, and everything else is low, unbirth will be a high % of what they choose, provided that the race is capable of it.)";
            case 197:
                return @"Allows you to pick a saved game to read the content settings from.
This makes it easier to use something else's content settings without overwriting your default content settings.
Note that this doesn't run the pass to try and update the settings between old versions, so loading a save from an old version may not result how you'd expect.  It may be beneficial to load the save and save it in the modern version.";
            case 198:
                return "Enables the Salamander monster packs";
            case 199:
                return "Affects the spawn % of all monsters by this ratio.  Set at max to use the set percentages.  If you set this to half, then a monster with a 40% chance to spawn per turn would only spawn 20% per turn.  \nThis is intended as a quick way of tuning monsters for smaller maps.";
            case 200:
                return "Affects the maximum armies for all monsters by this ratio.  Set at max to use the set max.  If you set this to half, then a monster with a 4 max armies would only have a max of 2.  This rounds down, but has a minimum of 1.  I.e. 99% of 4 armies would be 3, but 1% of 4 armies would be 1.  \nThis is intended as a quick way of tuning monsters for smaller maps.";
            case 201:
                return "If enabled, Leaders will automatically gain 3 points of leadership every level, and will be unable to manually put more points in.  Note that this raises the power of leaders.";
            case 202:
                return @"Allows villages to have more than one race as population, and each race is tracked separately.
<color=yellow>CAUTION: This is considered something of an experimental feature, some things may not work exactly as expected, and bugs are possible.
It should be fairly stable though.  There are a few places that are still keyed into the 'main' race, like happiness for villages. </color>";
            case 203:
                return "Enables the Mantis monster packs";
            case 204:
                return "Enables the Eastern Dragon monster packs";
            case 205:
                return "Disables any race specific prey graphics (i.e. Selicia prey graphics, or similiar things)\nBasically makes bulges smooth";
            case 206:
                return "Herms will only use female hair, instead of picking randomly from all possible hair types (has no effect if the race isn't divided into male/female hair)";
            case 207:
                return "Prevents monster armies from reforming after they're defeated (Any combination of fled units / eternal / lucky survival).  The remaining units will simply vanish.  This setting doesn't affect anything when the monster army wins.";
            case 208:
                return "Enables the Catfish monster packs";
            case 209:
                return "If enabled, Raptors will spawn with the compy";
            case 210:
                return "Enables the Gazelle monster packs";
            case 211:
                return "Enables Multi race villages to automatically flip their displayed race to the most populous race, note that this has some minor gameplay side effects, such as affecting happiness, and the Ai's priority for taking towns of their own race.";
            case 212:
                return "Completely disables adventurers from appearing (takes effect on the next turn if just enabled)";
            case 213:
                return "Completely disables standard mercenaries from appearing (takes effect on the next turn if just enabled).\nNote that this doesn't affect special mercs, but you can still turn them off manually in the mercenary area of content settings.";
            case 214:
                return "Affects how much population villages start with, though it won't exceed their max population";
            case 215:
                return "If enabled, retreated units will go to a random village individually, instead of the closest one, scattering defeated armies around more. (Also serves to break up eternal armies when defeated instead of sending them all to the same village)";
            case 216:
                return "Controls whether winter holiday mode is enabled (changes the decorations for snow, and enables certain races to spawn with holiday related outfits)\nNote that when this expires or is turned off, units will still keep wearing what they were wearing.";
            case 217:
                return "Controls how much maintenance each unit in an army costs per turn.";
            case 218:
                return "Changes which types of feeding are allowed. After a unit digests a unit in their breast(s) or cock, they can use that organ to feed allies in order to heal them.";
            case 219:
                return "If enabled, feeding a unit while their health is already maxed will result in a boost in EXP that scales with the digested unit's level(s).";
            case 220:
                return "Changes how unbirth is handled. If conversion is enabled, unbirthed units will be converted to their pred's side upon being digested. If rebirth is enabled, unbirthed units will be reborn as units of their pred's race as well as converted to their pred's side upon being absorbed. If both are enabled, conversion will occur first, but units can still be rebirthed by unbirthing a corpse or transferring a unit through CV.";
            case 221:
                return "If enabled, males and herms will be able to transfer cock vored units to their allies, allowing their allies to digest/absorb them. Prey has a chance of escaping if they are still alive when transferring. Prey can be transferred to a ally's stomach, or womb if they are female (or herm if herms have vaginas).";
            case 222:
                return "If enabled, preds can transfer units they have 'cumgested' through cock vore to strengthen a target unit. The target unit must be unbirthed by one of the pred's allies, as well as either be on the pred's side themselves, or have already been digested inside the pred's ally's womb.";
            case 223:
                return @"If enabled, any unit that's converted by the conversion trait or the KuroTenko mod will completely randomize their name and gender each time.
If disabled, it will only randomize if the two races don't have the same set of allowed genders (which most commonly are the same, except a good percentage of the monster races).";
            case 224:
                return "If enabled, special mercenaries can convert other units to copies of themselves with the kurotenko rebirth or the rebirth trait.";
            case 225:
                return "Enables the Earthworm monster packs";
            case 226:
                return "Will make it so that the maximum increase to a garrison's size from the capital or the insane buildings mod is 150% of the base value.   Basically designed to better support small army/garrison sizes, so if your max garrison size is 4, the a standard capital's max garrison will be a reasonable 6, instead of getting the full +8 and becoming 12.";
            case 227:
                return @"Controls the % of units that will have a random spell book in addition to their normal weapon.   Note that this doesn't even pretend to be balanced";
            case 228:
                return @"The Max level of the random spell book assigned (1-4)";
            case 229:
                return @"Disables scat after a transfer of someone dead.  It's assumed you're not passing along enough matter for them to create that.  
(You might argue the first pred still should, but that would require doubling up the prey, so it's best kept simple.)";
            case 230:
                return "Enables the Feral Lizard monster packs";
            case 231:
                return "Pressing the end turn button in tactical battles is always treated as if you clicked the 'let ai finish your turn' button.";
            case 232:
                return "If enabled, Komodos will spawn with the Monitors";
            case 233:
                return "Enables the Monitor monster packs";
            case 234:
                return "Enables the Schiwardez monster packs";
            case 235:
                return "If enabled, leader appearance will be re-randomized when they're brought back to life.  Note that the above option can limit their random options significantly.";
            case 236:
                return @"Desaturates the colors used for snow/sand tilesets in tactical battles, to make them less blinding to the photosensitive.
Note, due to the way this is set up, you have to restart the game client for changes to take effect.";
            case 237:
                return "Controls what parts can be suckled from.";
            case 238:
                return "Enables the Terrorbird monster packs";
            case 239:
                return @"If enabled, each disconnected village will attempt to connect to the main landmass, instead of just 1 village per disconnected landmass.  
(Only really applies on high water % maps.  This was the default behavior before V38, but was due to a bug rather than by design)";
            case 240:
                return "Hides the vulva / slits on female vipers. This causes their nethers to look similar to males without their dick out";
            case 241:
                return "Whether cumstains / milk puddles are created on the ground after absorption finishes for UB CV and BV.";
            case 242:
                return "Changes the timing when burps occur to be after digestion. By default it's after absorption.";
            case 243:
                return "Enables farts after absorption, as well as fart content for tactical logs.";
            case 244:
                return "Affects the percentage of farts when a unit has finished the absorption phase";
            case 245:
                return "Whether tactical log fluff may break the fourth wall sometimes, having units treat the player as another unit they can address." +
                    "\nCan also be turned on selectively for either side";
            case 246:
                return @"Enables the player to use belly rub on enemies, is mostly a toggle so you don't do it by accident.
Note, that units with the SeductiveTouch cheat trait will be able to rub enemies even when this is disabled";
            case 247:
                return @"Enables the Dratopyr monster packs";
            case 248:
                return @"Enables the Feral Lions monster packs";
            case 249:
                return @"How many times as strong an enemy army can be, before this monster ceases to view it as a target.";
            case 250:
                return @"Breast size modifier for herm units, if in the center, all types have an equal chance, but sliding it prevents one level per tick and changes the average size
Does not retroactively affect already created units.";
            case 251:
                return "How many tiles armies and villages can see if fog of war is enabled.  The game uses a simple system where line of sight isn't blocked by obstacles. ";
            case 252:
                return "Every amount of growth gained is multiplied by this percentage (requires Growth trait)";
            case 253:
                return "How much percent of its original size a unit can grow using the Growth trait (other means of size increase are unaffected – they stack on top)";
            case 254:
                return "A percentual reduction in the size of growth units that's applied each strategic turn (but they won't lose more than they've grown)";
            case 255:
                return "Makes more grown units decay at a different pace. This value is how much extra decay 1 factors of growth adds in ‰.\n" +
                    "For example, default is 40‰ (4%), which would be +2% decay at 1.5x growth, or +4% at 2x growth.";
            case 256:
                return "Modifies how mighty the game thinks the Race is (Affects power calculations).\n" +
                    "A way to make your balance changes known to the AI, or deliberately make it misjudge. " +
                    "In the default balance, this value ranges from 50 (compy) to 1200 (dragon).";
            case 257:
                return @"Enables the Goodra monster packs";
            case 258:
                return "Normally only base endurance and strength influence HP. With this, size and other boosts (green/red numbers) will be used as well.";
            case 259:
                return @"If enabled, condoms of varying colors will be used for Cock Vore disposal.
                Otherwise the standard cumstain will be used.
                Does nothing if the Milk/Cumstains option is toggled off.";
            default:
                return "";
        }
    }
}
