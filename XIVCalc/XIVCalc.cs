using Lumina.Excel;

namespace XIVCalc;
internal class XIVCalc
{

    private static readonly Lazy<ExcelSheet<Lumina.Excel.GeneratedSheets.Action>?> _actionSheetLoader =
        new(() => ExcelModule?.GetSheet<Lumina.Excel.GeneratedSheets.Action>());
    internal static ExcelSheet<Lumina.Excel.GeneratedSheets.Action>? ActionSheet => LuminaIsSetup ? _actionSheetLoader.Value : null;

    internal static ExcelModule? ExcelModule;
    internal static bool LuminaIsSetup => ExcelModule != null;
    public static void Init(ExcelModule? excelModule = null)
    {
        ExcelModule = excelModule;
        if (ExcelModule != null)
            SetUpWithLumina();
        else
            SetUpWithoutLumina();
    }
    private static void SetUpWithLumina()
    {

    }
    private static void SetUpWithoutLumina()
    {

    }
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
