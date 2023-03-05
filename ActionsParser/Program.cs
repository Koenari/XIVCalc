// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XivCalc.ActionsParser.ActDefinitions;
using XIVCalc.Interfaces;

namespace XIVCalc.ActionsParser;

public class Program
{
    private static readonly string ACTDefBaseURL = "https://raw.githubusercontent.com/ravahn/FFXIV_ACT_Plugin/master/Definitions/";
    private static readonly List<(string fileName, Job job)> Classes = new List<(string, Job)>()
    {
        ("Astrologian",Job.AST),
        ("Bard",Job.BRD),
        ("BlackMage",Job.BLM),
        ("Dancer",Job.DNC),
        ("DarkKnight",Job.DRK),
        ("Dragoon", Job.DRG),
        ("Gunbreaker", Job.GNB),
        ("Machinist", Job.MCH),
        ("Monk", Job.MNK),
        ("Ninja", Job.NIN),
        ("Paladin", Job.PLD),
        ("Reaper", Job.RPR),
        ("RedMage", Job.RDM),
        ("Sage", Job.SGE),
        ("Samurai",Job.SAM),
        ("Scholar", Job.SCH),
        ("Summoner", Job.SMN),
        ("Warrior", Job.WAR),
        ("WhiteMage", Job.WHM)
    };
    private static readonly string JsonExt = ".json";
    public static void Main()
    {
        Dictionary<Job, string> actDefinitions = new();
        Dictionary<Job, List<LuminaGameActionImpl>> actionLookup = new();
        LoadFiles(actDefinitions);
        foreach ((_, Job job) in Classes)
        {
            if (!actDefinitions.ContainsKey(job))
            {
                Console.WriteLine($"Job definition for {job} not found");
                continue;
            }
            ActJobDefinition? actDef = ParseJobDefinition(actDefinitions[job]);
            if (actDef == null) continue;
            actionLookup[job] = ConvertActions(actDef);
        }
        using var writer = new StreamWriter("output" + JsonExt);
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };
        writer.Write(JsonConvert.SerializeObject(actionLookup, settings));
    }
    private static List<LuminaGameActionImpl> ConvertActions(ActJobDefinition actDef)
    {
        List<LuminaGameActionImpl> result = new();
        foreach (ActionsItem action in actDef.actions)
        {

            IGameAction.ActionType actionType = IGameAction.ActionType.None;
            if (action.damage is not null) actionType = IGameAction.ActionType.DAMAGE;
            if (action.heal is not null) actionType = IGameAction.ActionType.HEAL;
            if (actionType == IGameAction.ActionType.None)
            {
                Console.WriteLine($"Action {action.Name} has no type");
                continue;
            }
            var potencies = actionType == IGameAction.ActionType.DAMAGE ? action.damage : action.heal;
            if (potencies is null) continue;
            int comboPot = potencies.FirstOrDefault(p => p.combo > 0)?.combo ?? 0;
            int aoePot;
            if (potencies.Any(p => p.targetindex is not null))
            {
                aoePot = potencies.First(p => p.targetindex is null).potency;
            }
            else
            {
                aoePot = 0;
            }
            int potency = aoePot > 0 ? potencies.First(p => p.targetindex is not null).potency : potencies[0].potency;
            result.Add(new(
                action.ID,
                action.Name,
                potency,
                actionType,
                comboPot,
                aoePot
                ));
        }
        return result;
    }
    private static ActJobDefinition? ParseJobDefinition(string json)
    {
        JObject root = JObject.Parse(json);
        JArray actionsJson = (JArray)root.Property("actions")!.Value;
        JArray effectsJson = (JArray)root.Property("statuseffects")!.Value;
        var actDef = JsonConvert.DeserializeObject<ActJobDefinition>(json);
        if (actDef is null)
            return null;
        foreach ((ActionsItem action, JToken curtoken) in actDef.actions.Zip(actionsJson.AsEnumerable()))
        {
            if (curtoken is not JObject curAction)
                continue;
            JProperty info = (JProperty)curAction.First;
            action.info = new(info.Name, info.Value.Value<string>());
        }
        foreach ((StatusEffectsItem action, JToken curtoken) in actDef.statuseffects.Zip(effectsJson.AsEnumerable()))
        {
            if (curtoken is not JObject curAction)
                continue;
            JProperty info = (JProperty)curAction.First;
            action.info = new(info.Name, info.Value.Value<string>());
        }
        return actDef;
    }
    private static void LoadFiles(Dictionary<Job, string> actDefinitions)
    {
        using HttpClient client = new();
        foreach (var job in Classes)
        {
            FileInfo outFile = new("Classes/" + job.fileName + JsonExt);
            string json = "";
            if (!outFile.Exists)
            {
                json = client.GetStringAsync(ACTDefBaseURL + job.fileName + JsonExt).GetAwaiter().GetResult();
                using var writer = new StreamWriter(outFile.OpenWrite());
                try
                {
                    writer.Write(json);
                }
                catch (Exception) { }
            }
            else
            {
                using var reader = new StreamReader(outFile.OpenRead());
                try
                {
                    json = reader.ReadToEnd();
                }
                catch (Exception) { }
            }
            if (json.Length > 0)
                actDefinitions.Add(job.job, json);
        }
    }
}