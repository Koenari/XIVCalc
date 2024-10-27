using System.Diagnostics.CodeAnalysis;

namespace XIVCalc;

/// <summary>
/// FFXIV jobs as their in-game values
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Job : byte
{
    /// <summary>
    /// Adventurer
    /// </summary>
    ADV = 0,
    /// <summary>
    /// Gladiator
    /// </summary>
    GLA = 1,
    /// <summary>
    /// Pugilist
    /// </summary>
    PGL = 2,
    /// <summary>
    /// Marauder
    /// </summary>
    MRD = 3,
    /// <summary>
    /// Lancer
    /// </summary>
    LNC = 4,
    /// <summary>
    /// Archer
    /// </summary>
    ARC = 5,
    /// <summary>
    /// Conjurer
    /// </summary>
    CNJ = 6,
    /// <summary>
    /// Thaumaturge
    /// </summary>
    THM = 7,
    /// <summary>
    /// Carpenter
    /// </summary>
    CRP = 8,
    /// <summary>
    /// Blacksmith
    /// </summary>
    BSM = 9,
    /// <summary>
    /// Armorer
    /// </summary>
    ARM = 10,
    /// <summary>
    /// Goldsmith
    /// </summary>
    GSM = 11,
    /// <summary>
    /// Leatherworker
    /// </summary>
    LTW = 12,
    /// <summary>
    /// Weaver
    /// </summary>
    WVR = 13,
    /// <summary>
    /// Alchemist
    /// </summary>
    ALC = 14,
    /// <summary>
    /// Culinarian
    /// </summary>
    CUL = 15,
    /// <summary>
    /// Miner
    /// </summary>
    MIN = 16,
    /// <summary>
    /// Botanist
    /// </summary>
    BTN = 17,
    /// <summary>
    /// Fisher
    /// </summary>
    FSH = 18,
    /// <summary>
    /// Paladin
    /// </summary>
    PLD = 19,
    /// <summary>
    /// Warrior
    /// </summary>
    WAR = 21,
    /// <summary>
    /// Monk
    /// </summary>
    MNK = 20,
    /// <summary>
    /// Dragoon
    /// </summary>
    DRG = 22,
    /// <summary>
    /// Bard
    /// </summary>
    BRD = 23,
    /// <summary>
    /// White Mage
    /// </summary>
    WHM = 24,
    /// <summary>
    /// Black MAge
    /// </summary>
    BLM = 25,
    /// <summary>
    /// Arcanist
    /// </summary>
    ACN = 26,
    /// <summary>
    /// Summoner
    /// </summary>
    SMN = 27,
    /// <summary>
    /// Scholar
    /// </summary>
    SCH = 28,
    /// <summary>
    /// Rogue
    /// </summary>
    ROG = 29,
    /// <summary>
    /// Ninja
    /// </summary>
    NIN = 30,
    /// <summary>
    /// Machinist
    /// </summary>
    MCH = 31,
    /// <summary>
    /// Dark Knight
    /// </summary>
    DRK = 32,
    /// <summary>
    /// Astrologian
    /// </summary>
    AST = 33,
    /// <summary>
    /// Samurai
    /// </summary>
    SAM = 34,
    /// <summary>
    /// Redmage
    /// </summary>
    RDM = 35,
    /// <summary>
    /// Blue Mage
    /// </summary>
    BLU = 36,
    /// <summary>
    /// Gunbreaker
    /// </summary>
    GNB = 37,
    /// <summary>
    /// Dancer
    /// </summary>
    DNC = 38,
    /// <summary>
    /// Reaper
    /// </summary>
    RPR = 39,
    /// <summary>
    /// Sage
    /// </summary>
    SGE = 40,
    /// <summary>
    /// Viper
    /// </summary>
    VPR = 41,
    /// <summary>
    /// Pictomancer
    /// </summary>
    PCT = 42,
}

/// <summary>
/// Collection of all stat types with their associated in-game index
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
/// <summary>
/// Type of attack
/// </summary>
public enum AttackType
{
    /// <summary>
    /// Unknown 
    /// </summary>
    Unknown,
    /// <summary>
    /// Weapon Skill
    /// </summary>
    WeaponSkill,
    /// <summary>
    /// Auto Attack
    /// </summary>
    AutoAttack
}
/// <summary>
/// List of commonly used classifications of jobs
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Determines if job role is tank
    /// </summary>
    public static bool IsTank(this Job job)
    {
        return job is Job.DRK or Job.GNB or Job.PLD or Job.WAR or Job.GLA or Job.MRD;
    }
    
    /// <summary>
    /// Determines if job role is physical ranged
    /// </summary>
    public static bool IsPhysicalRanged(this Job job)
    {
        return job is Job.BRD or Job.DNC or Job.MCH or Job.ARC;
    }

    /// <summary>
    /// Determines if job role is melee
    /// </summary>
    public static bool IsMelee(this Job job)
    {
        return job is Job.DRG or Job.MNK or Job.NIN or Job.RPR or Job.SAM or Job.LNC or Job.PGL or Job.ROG or Job.VPR;
    }

    /// <summary>
    /// Determines if job is a caster type
    /// </summary>
    public static bool IsCaster(this Job job)
    {
        return job is Job.AST or Job.SCH or Job.SGE or Job.WHM or Job.CNJ or Job.BLM or Job.BLU or Job.RDM or Job.SMN
                   or Job.THM or Job.ACN or Job.PCT;
    }
}