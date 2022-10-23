/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TacticalMessageLog;
using static LogUtilities;

static class PersonalityLogTextsDummyFile
{
    static PersonalityLogTextsDummyFile()
    {
        InitializeLists();
    }

    internal class EventString
    {
        internal Race ActorRace;
        internal Race TargetRace;
        internal Func<EventLog, string> GetString;
        internal Predicate<EventLog> Conditional;
        internal int Priority;

        public EventString(Func<EventLog, string> getString, int priority = 0, Race actorRace = (Race)4000, Race targetRace = (Race)4000, Predicate<EventLog> conditional = null)
        {
            Priority = priority;
            ActorRace = actorRace;
            TargetRace = targetRace;
            GetString = getString;
            if (conditional == null)
                Conditional = (s) => true;
            else
                Conditional = conditional;
        }
    }

    internal static List<EventString> SwallowMessages;
    internal static List<EventString> RandomDigestionMessages;
    internal static List<EventString> BellyRubMessages;
    internal static List<EventString> CockVoreMessages;
    internal static List<EventString> AnalVoreMessages;
    internal static List<EventString> DigestionDeathMessages;
    internal static List<EventString> BreastVoreMessages;
    internal static List<EventString> AbsorptionMessages;
    internal static List<EventString> BreastRubMessages;
    internal static List<EventString> BallMassageMessages;
    internal static List<EventString> TransferMessages;
    internal static List<EventString> BreastFeedMessages;
    internal static List<EventString> CumFeedMessages;

    internal static void InitializeLists()
    {
        if (SwallowMessages != null)
            return;
        bool PreyDead(EventLog s) => s.Prey.IsDead;
        bool PreyCumgested(EventLog s) => TacticalUtilities.GetPredatorComponentOfUnit(s.Unit)?.PreyInLocation(PreyLocation.balls, false) == 1;
        bool CanBurp(EventLog s) => Config.BurpFraction > .1f;
        bool Scat(EventLog s) => Config.Scat && (s.preyLocation == PreyLocation.stomach || s.preyLocation == PreyLocation.stomach2);
        bool Lewd(EventLog s) => Config.LewdDialog;
        bool HardVore(EventLog s) => Config.HardVoreDialog;
        bool HardVoreInStomach(EventLog s) => Config.HardVoreDialog && (s.preyLocation == PreyLocation.stomach || s.preyLocation == PreyLocation.stomach2);
        bool InStomach(EventLog s) => s.preyLocation == PreyLocation.stomach || s.preyLocation == PreyLocation.stomach2;
        bool InWomb(EventLog s) => s.preyLocation == PreyLocation.womb;
        bool InStomachOrWomb(EventLog s) => s.preyLocation == PreyLocation.stomach || s.preyLocation == PreyLocation.stomach2 || s.preyLocation == PreyLocation.womb;
        bool InBreasts(EventLog s) => s.preyLocation == PreyLocation.breasts || s.preyLocation == PreyLocation.leftBreast || s.preyLocation == PreyLocation.rightBreast;
        bool InBalls(EventLog s) => s.preyLocation == PreyLocation.balls;
        bool FirstTime(EventLog s) => s.Unit.DigestedUnits == 0 && s.Unit.Level < 10 && s.Unit.Type != UnitType.Mercenary && s.Unit.Type != UnitType.SpecialMercenary && State.GameManager.PureTactical == false;
        bool TargetFirstTime(EventLog s) => s.Target.DigestedUnits == 0 && s.Target.Level < 10 && s.Target.Type != UnitType.Mercenary && s.Target.Type != UnitType.SpecialMercenary && State.GameManager.PureTactical == false;
        bool Friendly(EventLog s) => s.Unit.Side == s.Target.Side;
        bool Humanoid(EventLog s) => s.Unit.Race < Race.Vagrants || s.Unit.Race >= Race.Selicia;
        bool Cursed(EventLog s) => s.Target.GetStatusEffect(StatusEffectType.WillingPrey) != null;
        //bool ReqSSW(EventLog s) => SameSexWarrior(s.Unit) != "NULL";
        bool ReqOSW(EventLog s) => AttractedWarrior(s.Unit) != null;
        bool ReqOSWLewd(EventLog s) => AttractedWarrior(s.Unit) != null && Lewd(s);
        bool ReqOSWStomach(EventLog s) => AttractedWarrior(s.Unit) != null && InStomach(s);
        bool ReqOSWBelly(EventLog s) => AttractedWarrior(s.Unit) != null && InStomachOrWomb(s);
        bool ReqSSWAndOSW(EventLog s) => CompetitionWarrior(s.Unit) != null && AttractedWarrior(s.Unit) != null;
        bool ReqTargetCompatible(EventLog s) => RomanticTarget(s.Unit, s.Target);
        bool ReqTargetCompatibleLewd(EventLog s) => RomanticTarget(s.Unit, s.Target) && Lewd(s);
        bool ReqTargetClothingOn(EventLog s) => s.Target.ClothingType != 0;
        bool ReqTargetClothingOff(EventLog s) => s.Target.ClothingType == 0;
        bool WeightGain(EventLog s) => Config.WeightGain;
        bool BonesDisposal(EventLog s) => Config.Bones && (s.preyLocation == PreyLocation.stomach || s.preyLocation == PreyLocation.stomach2);
        bool TargetBoobs(EventLog s) => s.Target.HasBreasts;
        bool ActorBoobs(EventLog s) => s.Unit.HasBreasts;

        //Formatting guide
        // You can find most of the stuff in LogUtilities.cs, this is just a short list of essentials for people too lazy to switch tabs (like me).
        // {i.Unit.Name} - name of the unit initiating action.
        // {i.Target.Name} - name of the unit being targeted by action.
        // {i.preyLocation.ToSyn()} - name of the location prey found themselves in (belly, womb, balls, breasts).
        // {PluralForPart(i.preyLocation)} - used to define whether prey location is singular or plural (e.g. "belly rumbles - balls rumble").
        //
        // NOTE: i.Unit is unit in below examples is for unit initializing action. If you want target of an action, change that to i.Target!
        //
        // {GPPHe(i.Unit)} - He/She/They.
        // {GPPHis(i.Unit)} - His/Hers/Their.
        // {GPPHim(i.Unit)} - Him/Her/Them.
        // {GPPHimself(i.Unit)} - Himself/Herself/Themselves.
        // {GPPHeIs(i.Unit)} - He is/She is/They are.
        // {GPPHeIsAbbr(i.Unit)} - He's/She's/They're.
        // {GPPIsAre(i.Unit)} - Is/Are.
        // {GPPBoyGirl(i.Unit)} - Boy/Girl/Racename.
        // {Capitalize(GPPHis(i.Target))} - Same as above, but capitalized. Just insert appropriate pronoun.
        // {SIfSingular(i.Target)} - if pronoun is singular, adds the "S" to the word before it. 
        // {EsIfSingular(i.Target)} - same but with "Es".
        // {IesIfSingular(i.Target)} - same but with "Ies".
        // {AttractedWarrior(i.Unit).Name} - if lewdness/attraction is on this defines compatible interested unit.
        // {RomanticTarget(i.Unit).Name} - if lewdness/attraction is on this defines unit the unit initiating action is attracted to.
        // {CompetitionWarrior(i.Unit).Name} - if lewdness/attraction is on this defines compatible interested unit attracted to the same person as unit initiating action.
        // {GetAorAN(i.Unit)} - defines whether there's "a" or "an" through space wizardry.
        // {GetPreyDesc(i.Unit)} - gets prey's description like "fluffy", "tasty" or "smooth". Generally meant for preyish and demeaning descriptions.
        // {GetPredDesc(i.Unit)} - gets pred's description like "cunning", "victorious" or "grinning". Generally meant for predish descriptions.
        // {GetRaceDescSingl(i.Unit)} - gets racial description or synonym.
        // {GetWeaponTrueName(i.Unit)} - gets the name of unit's weapon.

        //CONDITIONALS:

        //now let's see the example string
        //new EventString((i) => $"This string appears when unit of Slime race becomes the target of an action, however it only appears when "lewd combat log text" option is on." ,targetRace: Race.Slimes, priority: 9, conditional: Lewd),
        //new EventString((i) => $"This string appears only when Selicia initiates the action, has "lewd combat log text" option activated and target has breasts and a pussy.", actorRace: Race.Selicia, priority: 10, conditional: (i) => Lewd(i) && i.Target.HasBreasts && (i.Target.HasDick == false || Config.HermsCanUB)) ,
        //NOW, WHAT THE FUCK DOES THIS ALL MEAN? Sit down, this is gonna be a doozy.
        // actorRace is the race of unit initiating action. Use it when you want string restricted to only one race (probably because of unique biology)
        // targetRace is the race of unit targeted by an action. Same as actor, but string is restricted by who is on the receiving end.
        // priority: string always has priority. Priority 8 is "base" priority, priority 9 strings are higher priority (50% to choose one of priority 9 strings) and priority 10 strngs are ALWAYS chosen over 8 and 9 (so use them wisely)
        // conditional: the string will only appear if all conditionals are met. You can see rough list of conditionals above, they're pretty self-explanatory.
        // if you need several conditionals, formatting should be : conditional: s => Conditional1(s) && Conditional2(s) && Conditional3(s)  etc. 
        //for example:
        //new EventString((i) => $"Hello, I am a dummy message", priority: 9, conditional: s => InStomach(s) && Humanoid(s)),
        //conditional: (i) => State.RaceSettings.GetBodySize(i.Target.Race) <= 15 - race size

        SwallowMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        RandomDigestionMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        BellyRubMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),


        };

        BreastRubMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        BallMassageMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        BreastVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        CockVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),

        };

        AnalVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };

        DigestionDeathMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
            };

        AbsorptionMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };
        TransferMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };
        BreastFeedMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };
        CumFeedMessages = new List<EventString>()
        {
            new EventString((i) => $"Hello, I am a dummy message and you will only see me here in code because my priority is too low.", priority: 8),
            new EventString((i) => $"Hello, I am a dummy message, I and my comrades appear roughly 50% of the time unless there's a priority 10 message.", priority: 9),
            new EventString((i) => $"Hello, I am a dummy message, I have the highest priority and I will always override all other non- 10 priority messages. You should use me sparingly.", priority: 10),
        };
    }
}
*/