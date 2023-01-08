// See https://aka.ms/new-console-template for more information

using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Globalization;
using Action = Lumina.Excel.GeneratedSheets.Action;

if (args.Length != 1)
{
    PrintUsage();
    return 1;
}
ConcurrentDictionary<uint, (string name, byte cat, float recast, string potency)> output = new();
int errorcount = 0;
int emptyCount = 0;
int correctCount = 0;
try
{
    ExcelModule Exclemodule = new(new(args[0] + "/game/sqpack"));
    ExcelSheet<Action>? ActionSheet = Exclemodule.GetSheet<Action>();
    ExcelSheet<ActionTransient>? actionTransients = Exclemodule.GetSheet<ActionTransient>();
    if (actionTransients is null || ActionSheet is null)
        throw new NullReferenceException();
    Console.WriteLine("Sheets loaded");

    Parallel.ForEach<Action>(ActionSheet, (Action a) =>
    {
        if (a.ActionCategory.Row < 2 || a.ActionCategory.Row > 4)
            return;
        if (!a.IsPlayerAction)
            return;
        string pot = ParseDescription(actionTransients.GetRow(a.RowId)?.Description.RawString ?? "");
        output[a.RowId] = (a.Name, (byte)a.ActionCategory.Row, a.Recast100ms / 10f, pot);
    });
    using (StreamWriter writer = new StreamWriter("output.cs"))
    {
        writer.WriteLine("Dictionary<uint, (byte Category, float Reccast, int Potency)> ActionDB = new();");
        writer.WriteLine("ActionDB = new()");
        writer.WriteLine("{");
        foreach (var item in output.ToImmutableSortedDictionary())
        {
            var (name, cat, recast, potency) = item.Value;
            writer.WriteLine("    " + formatOuputLine(item.Key, name, cat, recast, potency));
        }
        writer.WriteLine("}");
    }
    Console.WriteLine($"Complex Potencies:{errorcount}");
    Console.WriteLine($"Empty Potencies:{emptyCount}");
    Console.WriteLine($"Correct Potencies:{correctCount}");

    return 0;
}
catch (IOException)
{
    Console.WriteLine("Wrong Game Path");
    return 1;
}
catch (NullReferenceException)
{
    Console.WriteLine("Lumina error");
    return 1;
}

string ParseDescription(string description)
{
    string result = "";
    int idx = description.IndexOf("potency of");
    //No potency
    if (idx == -1)
    {
        Interlocked.Increment(ref emptyCount);
        return "0";//$"0/*{description}*/";
    }

    string pot = description.Substring(idx + 11);
    //Simple number
    if (pot[0] >= '0' && pot[0] <= '9')
    {
        try
        {
            char[] ends = new char[] { '.', ' ' };
            int idx2 = pot.IndexOfAny(ends);
            if (idx2 == -1)
                throw new FormatException();
            pot = pot.Remove(idx2);
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            result = $"{int.Parse(pot, NumberStyles.AllowThousands, provider)}";
        }
        catch (FormatException)
        {
            result = $"0/*{pot}*/";
        }
        Interlocked.Increment(ref correctCount);
        return result;
    }
    //Complex Expression
    //PlayerParameter(68) = ClassJob
    //PlayerParameter(72) = Level

    result = $"\"{pot}\"";
    Interlocked.Increment(ref errorcount);



    return result;
}

void PrintUsage()
{

}
string formatOuputLine(uint id, string name, byte category, float recast, string potency)
{
    return $"[{id}] = (\"{name}\", {category}, {recast}f, {potency})";
}