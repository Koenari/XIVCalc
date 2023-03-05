namespace XIVCalc.Interfaces;
public enum DamageType
{
    Physical,
    Magical,
    Other
}

public interface IStatusEffect
{
    int Potency { get; }
    int Amount { get; }
    int NumTicks { get; }
    EffectType Type { get; }
    DamageType DmgType { get; }
    DamageType LimitTo { get; }

    public enum EffectType
    {
        DoT,
        HoT,
        GroundHeal,
        GroundDamage,
        HealReceiveMultiplier,
        DamageReceivedMultiplier,
        HealDoneMultiplier,
        DamageDoneMultiplier,
        PotencyShield,
        TargetHpPercentShield,
        HealPercentShield,
        CriticalHitMultiplier,
        CriticalReceivedMultiplier,
        DirectHitMultiplier,
        HealOnHealCastProc,
        HealOnDamageDealtProc,
    }
}
