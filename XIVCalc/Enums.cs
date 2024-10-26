namespace XIVCalc;

public enum Role
{
    None,
    Tank,
    Heal,
    Melee,
    PhysicalRanged,
    MagicalRanged
}

public enum Job : byte
{
    ADV = 0,
    AST = 33,
    BLM = 25,
    BLU = 36,
    BRD = 23,
    DNC = 38,
    DRG = 22,
    DRK = 32,
    GNB = 37,
    MCH = 31,
    MNK = 20,
    NIN = 30,
    PLD = 19,
    RDM = 35,
    RPR = 39,
    SAM = 34,
    SCH = 28,
    SGE = 40,
    SMN = 27,
    WAR = 21,
    WHM = 24,
    GLA = 1,
    MRD = 3,
    LNC = 4,
    PGL = 2,
    ARC = 5,
    THM = 7,
    ACN = 26,
    CNJ = 6,
    ROG = 29,
    VPR = 41,
    PCT = 42,
}
public enum StatType : byte
{
    None,
    Strength,
    Dexterity,
    Vitality,
    Intelligence,
    Mind,
    Piety,
    HP,
    MP,
    TP,
    GP,
    CP,
    PhysicalDamage,
    MagicalDamage,
    Delay,
    AdditionalEffect,
    AttackSpeed,
    BlockRate,
    BlockStrength,
    Tenacity,
    AttackPower,
    Defense,
    DirectHitRate,
    Evasion,
    MagicDefense,
    CriticalHitPower,
    CriticalHitResilience,
    CriticalHit,
    CriticalHitEvasion,
    SlashingResistance,
    PiercingResistance,
    BluntResistance,
    ProjectileResistance,
    AttackMagicPotency,
    HealingMagicPotency,
    EnhancementMagicPotency,
    EnfeeblingMagicPotency,
    FireResistance,
    IceResistance,
    WindResistance,
    EarthResistance,
    LightningResistance,
    WaterResistance,
    MagicResistance,
    Determination,
    SkillSpeed,
    SpellSpeed,
    Haste,
    Morale,
    Enmity,
    EnmityReduction,
    CarefulDesynthesis,
    EXPBonus,
    Regen,
    Refresh,
    MovementSpeed,
    Spikes,
    SlowResistance,
    PetrificationResistance,
    ParalysisResistance,
    SilenceResistance,
    BlindResistance,
    PoisonResistance,
    StunResistance,
    SleepResistance,
    BindResistance,
    HeavyResistance,
    DoomResistance,
    ReducedDurabilityLoss,
    IncreasedSpiritbondGain,
    Craftsmanship,
    Control,
    Gathering,
    Perception,
    Unknown73,
    Count,
}
public static class EnumExtensions
{
    public static bool IsTank(this Job job)
    {
        return job is Job.DRK or Job.GNB or Job.PLD or Job.WAR or Job.GLA or Job.MRD;
    }

    public static bool IsPhysicalRanged(this Job job)
    {
        return job is Job.BRD or Job.DNC or Job.MCH or Job.ARC;
    }

    public static bool IsMelee(this Job job)
    {
        return job is Job.DRG or Job.MNK or Job.NIN or Job.RPR or Job.SAM or Job.LNC or Job.PGL or Job.ROG or Job.VPR;
    }

    public static bool IsCaster(this Job job)
    {
        return job is Job.AST or Job.SCH or Job.SGE or Job.WHM or Job.CNJ or Job.BLM or Job.BLU or Job.RDM or Job.SMN
                   or Job.THM or Job.ACN or Job.PCT;
    }
}