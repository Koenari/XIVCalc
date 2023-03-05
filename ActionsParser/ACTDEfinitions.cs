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
    public int targetindex { get; set; }
}

public class ActionsItem
{
    public int ID => int.Parse($"0x{info.Key}", NumberStyles.HexNumber);
    public string Name => info.Value;
    public KeyValuePair<string, string> info { get; set; }
    public List<RawDamageItem> damage { get; set; }
    public List<RawDamageItem> heal { get; set; }
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
public class StatuseffectsItem
{
    public int ID => int.TryParse($"0x{info.Key}", NumberStyles.HexNumber, null, out int parsed) ? parsed : -1;
    public string Name => info.Value;
    public KeyValuePair<string, string> info { get; set; }
    public TimeprocEffect? timeproc { get; set; }
    public List<PotencyEffect>? potency { get; set; }
    public DamageShieldEffect? damageshield { get; set; }
    public List<MultiplierEffect>? multiplier { get; set; }
    public ReactiveProcEffect? reactiveproc { get; set; }
}

public class ActJobDefnition
{
    public string job { get; set; }
    public List<ActionsItem> actions { get; set; }
    public List<StatuseffectsItem> statuseffects { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#endregion