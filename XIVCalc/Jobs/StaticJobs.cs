using System.Diagnostics.CodeAnalysis;
using XIVCalc.Interfaces;

namespace XIVCalc.Jobs;

/// <summary>
/// Collection of job modifiers
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class StaticJobs
{
    /// <summary>
    /// Adventurer
    /// </summary>
    public static readonly IJobModifiers ADV = new FixedJobModifiers
    {
        Job = Job.ADV,
        HitPoints = 100,
        Strength = 100,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 100,
        Mind = 100,
        PrimaryStat = StatType.None, 
    };
    /// <summary>
    /// Gladiator
    /// </summary>
    public static readonly IJobModifiers GLA = new FixedJobModifiers
    {
        Job = Job.GLA,
        HitPoints = 130,
        Strength = 95,
        Vitality = 100,
        Dexterity = 90,
        Intelligence = 50,
        Mind = 95,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Gladiator
    /// </summary>
    public static readonly IJobModifiers PGL = new FixedJobModifiers
    {
        Job = Job.PGL,
        HitPoints = 105,
        Strength = 100,
        Vitality = 95,
        Dexterity = 100,
        Intelligence = 45,
        Mind = 85,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Gladiator
    /// </summary>
    public static readonly IJobModifiers MRD = new FixedJobModifiers
    {
        Job = Job.MRD,
        HitPoints = 135,
        Strength = 100,
        Vitality = 100,
        Dexterity = 900,
        Intelligence = 30,
        Mind = 50,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Gladiator
    /// </summary>
    public static readonly IJobModifiers LNC = new FixedJobModifiers
    {
        Job = Job.LNC,
        HitPoints = 110,
        Strength = 105,
        Vitality = 100,
        Dexterity = 95,
        Intelligence = 40,
        Mind = 60,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Archer
    /// </summary>
    public static readonly IJobModifiers ARC = new FixedJobModifiers
    {
        Job = Job.ARC,
        HitPoints = 100,
        Strength = 85,
        Vitality = 95,
        Dexterity = 105,
        Intelligence = 80,
        Mind = 75,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Conjurer
    /// </summary>
    public static readonly IJobModifiers CNJ = new FixedJobModifiers
    {
        Job = Job.CNJ,
        HitPoints = 100,
        Strength = 50,
        Vitality = 95,
        Dexterity = 100,
        Intelligence = 100,
        Mind = 105,
        PrimaryStat = StatType.Mind,
    };
    /// <summary>
    /// Conjurer
    /// </summary>
    public static readonly IJobModifiers THM = new FixedJobModifiers
    {
        Job = Job.THM,
        HitPoints = 100,
        Strength = 40,
        Vitality = 95,
        Dexterity = 95,
        Intelligence = 105,
        Mind = 70,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Paladin
    /// </summary>
    public static readonly IJobModifiers PLD = new FixedJobModifiers
    {
        Job = Job.PLD,
        HitPoints = 140,
        Strength = 40,
        Vitality = 95,
        Dexterity = 95,
        Intelligence = 105,
        Mind = 70,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Mok
    /// </summary>
    public static readonly IJobModifiers MNK = new FixedJobModifiers
    {
        Job = Job.MNK,
        HitPoints = 110,
        Strength = 110,
        Vitality = 100,
        Dexterity = 105,
        Intelligence = 50,
        Mind = 90,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Warrior
    /// </summary>
    public static readonly IJobModifiers WAR = new FixedJobModifiers
    {
        Job = Job.WAR,
        HitPoints = 145,
        Strength = 105,
        Vitality = 110,
        Dexterity = 95,
        Intelligence = 40,
        Mind = 55,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Dragoon
    /// </summary>
    public static readonly IJobModifiers DRG = new FixedJobModifiers
    {
        Job = Job.DRG,
        HitPoints = 115,
        Strength = 115,
        Vitality = 105,
        Dexterity = 100,
        Intelligence = 44,
        Mind = 65,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Bard
    /// </summary>
    public static readonly IJobModifiers BRD = new FixedJobModifiers
    {
        Job = Job.BRD,
        HitPoints = 105,
        Strength = 90,
        Vitality = 100,
        Dexterity = 115,
        Intelligence = 85,
        Mind = 80,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// White Mage
    /// </summary>
    public static readonly IJobModifiers WHM = new FixedJobModifiers
    {
        Job = Job.WHM,
        Strength = 55,
        Dexterity = 105,
        Intelligence = 105,
        Mind = 115,
        Vitality = 100,
        HitPoints = 105,
        PrimaryStat = StatType.Mind,
    };
    /// <summary>
    /// Black Mage
    /// </summary>
    public static readonly IJobModifiers BLM = new FixedJobModifiers
    {
        Job = Job.BLM,
        HitPoints = 105,
        Strength = 45,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 115,
        Mind = 75,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Arcanist
    /// </summary>
    public static readonly IJobModifiers ACN = new FixedJobModifiers
    {
        Job = Job.ACN,
        HitPoints = 100,
        Strength = 85,
        Vitality = 95,
        Dexterity = 95,
        Intelligence = 105,
        Mind = 75,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Summoner
    /// </summary>
    public static readonly IJobModifiers SMN = new FixedJobModifiers
    {
        Job = Job.SMN,
        HitPoints = 105,
        Strength = 90,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 115,
        Mind = 80,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Scholar
    /// </summary>
    public static readonly IJobModifiers SCH = new FixedJobModifiers
    {
        Job = Job.SCH,
        HitPoints = 105,
        Strength = 90,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 105,
        Mind = 115,
        PrimaryStat = StatType.Mind,
    };
    /// <summary>
    /// Rogue
    /// </summary>
    public static readonly IJobModifiers ROG = new FixedJobModifiers
    {
        Job = Job.ROG,
        HitPoints = 103,
        Strength = 80,
        Vitality = 95,
        Dexterity = 100,
        Intelligence = 60,
        Mind = 70,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Ninja
    /// </summary>
    public static readonly IJobModifiers NIN = new FixedJobModifiers
    {
        Job = Job.NIN,
        HitPoints = 108,
        Strength = 85,
        Vitality = 95,
        Dexterity = 110,
        Intelligence = 65,
        Mind = 75,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Machinist
    /// </summary>
    public static readonly IJobModifiers MCH = new FixedJobModifiers
    {
        Job = Job.MCH,
        HitPoints = 105,
        Strength = 85,
        Vitality = 100,
        Dexterity = 115,
        Intelligence = 80,
        Mind = 85,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Dark Knight
    /// </summary>
    public static readonly IJobModifiers DRK = new FixedJobModifiers
    {
        Job = Job.DRK,
        HitPoints = 140,
        Strength = 105,
        Vitality = 110,
        Dexterity = 95,
        Intelligence = 60,
        Mind = 40,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Astrologian
    /// </summary>
    public static readonly IJobModifiers AST = new FixedJobModifiers
    {
        Job = Job.AST,
        HitPoints = 105,
        Strength = 50,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 105,
        Mind = 115,
        PrimaryStat = StatType.Mind,
    };
    /// <summary>
    /// Samurai
    /// </summary>
    public static readonly IJobModifiers SAM = new FixedJobModifiers
    {
        Job = Job.SAM,
        HitPoints = 109,
        Strength = 112,
        Vitality = 100,
        Dexterity = 108,
        Intelligence = 60,
        Mind = 50,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Red Mage
    /// </summary>
    public static readonly IJobModifiers RDM = new FixedJobModifiers
    {
        Job = Job.RDM,
        HitPoints = 105,
        Strength = 55,
        Vitality = 100,
        Dexterity = 105,
        Intelligence = 115,
        Mind = 110,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Blue Mage
    /// </summary>
    public static readonly IJobModifiers BLU = new FixedJobModifiers
    {
        Job = Job.BLU,
        HitPoints = 105,
        Strength = 70,
        Vitality = 100,
        Dexterity = 110,
        Intelligence = 115,
        Mind = 105,
        PrimaryStat = StatType.Intelligence,
    };
    /// <summary>
    /// Gunbreaker
    /// </summary>
    public static readonly IJobModifiers GNB = new FixedJobModifiers
    {
        Job = Job.GNB,
        HitPoints = 140,
        Strength = 100,
        Vitality = 110,
        Dexterity = 95,
        Intelligence = 60,
        Mind = 100,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Dancer
    /// </summary>
    public static readonly IJobModifiers DNC = new FixedJobModifiers
    {
        Job = Job.DNC,
        HitPoints = 105,
        Strength = 90,
        Vitality = 100,
        Dexterity = 115,
        Intelligence = 85,
        Mind = 80,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Reaper
    /// </summary>
    public static readonly IJobModifiers RPR = new FixedJobModifiers
    {
        Job = Job.RPR,
        HitPoints = 115,
        Strength = 115,
        Vitality = 105,
        Dexterity = 100,
        Intelligence = 80,
        Mind = 40,
        PrimaryStat = StatType.Strength,
    };
    /// <summary>
    /// Sage
    /// </summary>
    public static readonly IJobModifiers SGE = new FixedJobModifiers
    {
        Job = Job.SGE,
        HitPoints = 105,
        Strength = 60,
        Vitality = 100,
        Dexterity = 100,
        Intelligence = 115,
        Mind = 115,
        PrimaryStat = StatType.Mind,
    };
    /// <summary>
    /// Viper
    /// </summary>
    public static readonly IJobModifiers VPR = new FixedJobModifiers
    {
        Job = Job.VPR,
        HitPoints = 111,
        Strength = 100,
        Vitality = 100,
        Dexterity = 110,
        Intelligence = 45,
        Mind = 55,
        PrimaryStat = StatType.Dexterity,
    };
    /// <summary>
    /// Pictomancer
    /// </summary>
    public static readonly IJobModifiers PCT = new FixedJobModifiers
    {
        Job = Job.PCT,
        HitPoints = 105,
        Strength = 50,
        Vitality = 100,
        Dexterity = 110,
        Intelligence = 115,
        Mind = 80,
        PrimaryStat = StatType.Intelligence,
    };
}