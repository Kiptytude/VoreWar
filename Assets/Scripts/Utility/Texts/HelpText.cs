using System;
using System.Text;

static class HelpText
{
    public static string GameBasics = @"Goals:
Claim all enemy villages for your side, to do this raise armies at your villages by recruiting soldiers and equipping them with items in the village shop then moving them towards the enemy.  There's 3 different selectable victory conditions.

Terms:
Village, can be inhabited by any race and owned by any side. You get income from these, you can only recruit soldiers at villages of your own race, unless you build subjugation pens in them.
Army: how you take your fight to the enemy, armies by default can have up to 16 soldiers but that can be changed in the create strategic screen.

Gold and you:
You need gold to buy weapons for garrisons, recruit soldiers and buy equipment. You get gold based on the population of your villages, you also however lose 3 gold per turn for each soldier in your armies (armies are the little banners).

The game autosaves at the beginning of every tactical battle, and the beginning of every strategic turn, so it's recommended to make a proper save when you stop playing so you don't overwrite it.";

    public static string StrategyMode = @"Strategy Mode - The main view of a normal campaign, you can click on villages to go into them, or click on armies to select them.  To view a village that's occupied by an army, you have to select the army and then click on unit info.  To move the selected army, use the numpad, or whatever you have rebound it to.

Villages have a happiness value that decreases when conquered by other races, and will gradually increase to 80 if they are not the same race as you, and 100 if they are.  Happiness controls the amount of taxes paid.

If your army enters the same tile as an enemy army, a battle will start and the game switches to tactical mode.

You can buy weapons to outfit garrisons at your villages, capitals get 8 bonus troop slots. When you are attacked any units in your hiring list come to the towns defense, if that's not sufficient, townspeople are drafted and given weapons.

Your soldiers can equip two items each, one of which should be a ranged or melee weapon. 

If there is not enough gold to pay for maintenance, army troops will start deserting.

Health Recovery:
Put your wounded armies in villages. If the village has an inn, they will heal faster. If you want it to go faster still then you'll have to accept villages being emptier when you're done and devour village population.";

    public static string RecruitMode = @"Recruit / Village Mode:

You can create armies by recruiting up to your empire's maximum army size to join the army.   Make sure you visit the shop to buy items for your troops, as troops without items are much less effective in combat.  You can use the cheap equip button to buy basic weapons for all of your troops to save time.

You can recruit units from any race in a village, but note that units from other races may defect when they encounter armies or villages of their own race.  

The shop will open for whatever character you currently have selected for buying or selling, but there are options to modify multiple units at once.

Villages will use any stationed defenders (in the hiring list), and / or draft units and give them stored weapons to form a defensive squad up to your empires maximum garrison size.  Note that a defender can use both his army and garrison for defense at the same time, making a large defensive force.

You can also dismiss units, who will then stay on station in the town, they cost no maintenance and can be rehired using the hire button.  Units dimissed outside of town will return to the closest town, but take a few turns to get there.

You can also purchase upgrades for villages in the village view.";

    public static string TacticalMode = @"Tactical mode:

Two factions battle it out until only one side remains.  

To move, either use the keyboard movement keys or the mouse movement mode (see controls), and then you can left click on the tile you wish to move to.  (yellow dots mean if you move here you can still attack, and if you move to the red dot, you won't be able to take any further actions)

To perform other actions, click the action buttons, or use the hotkeys.  Red text is for targets in range, black for targets out of range, and yellow in vore mode for targets that are currently too big to be eaten by you.

You can click click on a unit, including the selected units, to get a list of options.  Things that are out of range are greyed out.  

Units that do not have a melee weapon equipped can use their claws as a weak melee weapon.

After the 8th turn, units can retreat by heading to the map edge of the side they started from (top/bottom) and hitting the retreat button.

You can use the AI move button to let the AI make the rest of your units act and end your turn, which can be useful to speed up battles that you don't care as much about.

You can also press nextunit key to select a unit that has moves remaining.";

    public static string Stats = @"Stats:
strength, controls hit chance for melee and affects melee damage, also increases max health, also plays a minor role in vore defense and escape
dexterity, controls hit chance for ranged and affects ranged damage, plays a minor role in vore escape
agility, controls melee and ranged defense and movement points
endurance, controls health maximum, also reduces damage from acid, has a minor role in escape chance.
mind, increases damage, effect, accuracy and duration of magic spells.  
will, has the largest share of vore defense, and escape boost.  Also controls magic defense, slightly effects digestion damage has an effect on mana points.
voracity, controls vore hit chance and has a minor effect on keeping prey down and digestion damage
stomach, controls digestion damage, keeping prey down, and stomach size";

    public static string Items = @"Item Reference:
claws, very weak innate melee weapon that all units have
mace, simple melee weapon, weak but slightly higher accuracy
axe, stronger melee weapon
bow, simple ranged weapon, weak but slightly higher accuracy
compound bow, stronger ranged weapon
helmet, raises agility
body armor, raises health
gauntlet, raise strength
gloves, raises dexterity
shoes, raises agility";

    public static string Vore = @"Vore is an important mechanic in this world.

A unit can attempt to eat another unit.  The odds are determined by the attackers Voracity stat and health, and the defender's will, strength and remaining health %. There are also other modifiers, for instance, the bigger the prey, the more difficult, and having more space in a stomach makes it easier.

Units can fight their way out of a stomach, this is a random chance that depends on the relative health between the two targets, the predator's stomach stat, and the prey's strength, endurance dexterity and will.  The first few turns after being eaten, the prey's chance of escape is reduced while they get their bearings.

A living unit will be digested and slowly lose health, once a unit dies, it will enter the absorption phase, and the predator will be slowly healed until absorption finishes. 

Units have a certain amount of prey capacity determined by a combination of their race and their stomach stat (you can hover over the stomach stat to show the total).   Eating units that have eaten other units take up more space.

A predator's available movement will reduce as they have more prey in their stomach, and they are also easier to hit.  

When units are killed in a stomach, a few turns later any living prey they had will be able to fight their way out into the main stomach.  Dead prey only breaks out once the host is completely absorbed.   If friendly regurgitation is on, friendly units will be regurgitated, otherwise they will simply be digested.  Units can also be freed by killing the host as long as the prey is still alive.";

    public static string Experience = @"Your soldiers can gain experience in battle
You can also train them for experience at any village with a training building, but there are diminishing returns on training speed.  The upper levels of training are mainly designed to help out when you have an overwhelming money advantage but the enemy has a very strong army.  Units also start with experience points depending on how many training buildings are built for your team.  

To level up, click the level up button in the army/recruit window.  It will raise all of your stats by a small amount, and you can pick one stat from 3 randomly chosen stats to get a large bonus. ";

    public static string Controls = $@"These are the default controls unless you've changed them

Camera:
wasd or arrow keys move the camera
you can also click and drag to move the camera
scroll wheel to zoom in/out

Gameplay:
numpad to move units around
m to enable mouse movement mode, then click to move
you can also right click when you have a unit selected to move
n selects next unit capable of moving
space cancels current action or deselects units
enter ends turn

tactical only:
1 for melee attack
2 for ranged attack
3 for vore attack
Right click for possible actions

Misc:
Esc opens the menu or closes it
F5 is quicksave
F9 is quickload
p pauses game, its main use is if you want to look around during the AI's turn

on a message box, space will close it";

    public static string Teams = @"In the strategic creation screen, you can assign races to different teams.  Armies can't occupy the same space as allied armies, but you can sit in allied towns.  You can also recruit units from, or create entire armies from allied towns.  If attacked while in an allied town, your units will join their garrison on the defense.  If a human player controls either the defending army or the garrison, he can control both.  

If someone on a team takes back a village that is a teammate's race from the enemy, that village will switch to the side of its race.  

Right now the only benefit shared across teams is the +exp bonus from the number of training facilities built. 


Each empire has its own side, regardless of teams or diplomacy, which generally determines who controls something and on whose turn. In tactical, the sides can be taken literally, as they correspond to what half of the battlefield a unit starts in.

However, units with certain traits, like Infiltrator, can have a fixed allegiance that's different to their side and can end up being controlled by the enemy or a third party in the middle of their supposed side's turn. If that allegiance is not known to an enemy, they can be sneak-attacked for extra hit/vore chance and extra attack damage.

Certaín effects can cause or prevent a change in this allegiance. Normal defection does not affect it, and can thus be used by infiltrators to infiltrate.
Units with the Untamable trait cannot be converted by any means other than vore – e.g the DigestionConversion trait or Unbirth with KuroTenko rebirth/conversion settings.
Corruption prevents these vore-based conversions and may instead corrupt the predator, changing their allegiance to the prey's (unless they have Untamable).
Check the Traits help section or a trait's tooltip for more info. As a rule of thumb, hidden traits will have something to do with special allegiances.

If only units that are friendly to eachother remain on the battlefield, Infiltrators will flee and other units with a special allegiance will defect.
";

    public static string Diplomacy = @"If diplomacy is enabled, you can declare war, or ask for peace and alliances from allies, you need a minimum amount of influence to achieve peace or alliances. 

Relations increase when you: Give gold, attack the enemy of an empire, or give someone back a village of their race (offered if you capture a village belonging to your ally or someone you're at peace with)

Relations decrease when you: Attack someone directly, attack the ally of an empire, or devour 100% of a town that's not your race.  

Relations will slowly work their way back to a semi-neutral state over time depending on your current relation type (to 1.5 for allies, 0 for peace/war)

Allies can win the game as a team, but sides with simply a peace treaty can't.   If an AI has no enemies, it will declare war on the neutral empire it likes the least (to keep the game from ending up in a stalemate), but this system doesn't declare war on allies";

    public static string Traits()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Traits trait in (Race[])Enum.GetValues(typeof(Traits)))
        {
            sb.AppendLine($"{trait} - {HoveringTooltip.GetTraitData(trait).Replace('\n', ' ')}");
        }
        return sb.ToString();

    }

    public static string Spells()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"Spells have a variety of effects and can be cast using mana.  Some spells can target tiles (done by using shift-right click and choosing the spell, or shift-left clicking when the spell is selected
Mana regenerates 60% of its max on each strategic turn.");

        foreach (Spell spell in SpellList.SpellDict.Values)
        {
            sb.AppendLine($"{spell.Name}\n{spell.Description}\nRange: {spell.Range.Min}-{spell.Range.Max}\nMana Cost: {spell.ManaCost}\nTargets: {string.Join(", ", spell.AcceptibleTargets)}\n");
        }
        return sb.ToString();
    }
}

