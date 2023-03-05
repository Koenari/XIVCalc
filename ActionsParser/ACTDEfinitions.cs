using Newtonsoft.Json;
using System.Globalization;

namespace XivCalc.ActionsParser.ActDefinitions;


#region RawDefinitions
#pragma warning disable IDE1006 // Naming Styles
public enum DamageType
{
    Unknown,
    Magic,
    Physical
}
public class RawDamageItem
{
    public int potency { get; set; }
    public int combo { get; set; }
    public object? targetindex { get; set; } = null;
}

public class ActionsItem
{
    public uint ID => uint.TryParse($"{info.Key}", NumberStyles.HexNumber, null, out uint parsed) ? parsed : 0;
    public string Name => info.Value;
    [JsonProperty]
    public KeyValuePair<string, string> info { get; set; }
    public List<RawDamageItem>? damage { get; set; }
    public List<RawDamageItem>? heal { get; set; }
}

public abstract class StatusEffectBase<T> where T : struct
{
    public T ParsedType => Enum.TryParse(type, true, out T parsed) ? parsed : default;
    public string? type { get; set; }
}

public class TimeprocEffect : StatusEffectBase<TimeprocEffect.Type>
{
    public int potency { get; set; }
    public string? damagetype { get; set; }
    public DamageType ParsedDmgType => Enum.TryParse(damagetype, true, out DamageType parsed) ? parsed : DamageType.Unknown;
    public int maxticks { get; set; }
    public enum Type
    {
        Unknown,
        DoT,
        HoT,
        GroundHeal
    }
}
public class PotencyEffect : StatusEffectBase<PotencyEffect.Type>
{
    public int potency { get; set; }
    public int amount { get; set; }
    public string? limittodamagetype { get; set; }
    public bool? isstacked { get; set; }
    public DamageType limitedTo => Enum.TryParse(limittodamagetype, true, out DamageType limited) ? limited : DamageType.Unknown;

    public enum Type
    {
        Unknown,
        GroundHeal,
        GroundDamage,
        HealReceiveMultiplier,
        DamageReceivedMultiplier,
        HealDoneMultiplier,
        DamageDoneMultiplier
    }
}
public class DamageShieldEffect : StatusEffectBase<DamageShieldEffect.Type>
{
    public int amount { get; set; }
    public enum Type
    {
        Unknown,
        Potency,
        TargetHpPercent,
        HealPercent
    }
}
public class MultiplierEffect : StatusEffectBase<MultiplierEffect.Type>
{
    public int amount { get; set; }
    public string? limitto { get; set; }

    public enum Type
    {
        Unknown,
        CriticalHit,
        CriticalReceived,
        DirectHit,
    }
}
public class ReactiveProcEffect : StatusEffectBase<ReactiveProcEffect.Type>
{
    public enum Type
    {
        Unknown,
        HealOnHealCast,
        HealOnDamageDealt,
    }
}
public class StatusEffectsItem
{
    public uint ID => uint.TryParse($"{info.Key}", NumberStyles.HexNumber, null, out uint parsed) ? parsed : 0;
    public string Name => info.Value;
    public KeyValuePair<string, string> info { get; set; }
    public TimeprocEffect? timeproc { get; set; }
    public List<PotencyEffect>? potency { get; set; }
    public DamageShieldEffect? damageshield { get; set; }
    public List<MultiplierEffect>? multiplier { get; set; }
    public ReactiveProcEffect? reactiveproc { get; set; }
}

public class ActJobDefinition
{
    public string job { get; set; }
    public List<ActionsItem> actions { get; set; }
    public List<StatusEffectsItem> statuseffects { get; set; }
    public List<ActionsItem> parsedActions { get; set; }
    public List<StatusEffectsItem> parsedStatuseffects { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#endregion