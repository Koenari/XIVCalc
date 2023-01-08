using Lumina.Excel.GeneratedSheets;

namespace XIVCalc.Calculations;

public class StatEquations
{
    /// <summary>
    /// Calculates a multiplicative modifier based on the Job/class
    /// </summary>
    /// <param name="statType">Which stat is being queried</param>
    /// <param name="job">Current job to evaluate for</param>
    /// <returns>Multiplicative modifier to apply to stat</returns>
    public static double GetJobModifier(byte statType, ClassJob? job) => GetJobModifier((StatType)statType, job);
    /// <summary>
    /// Calculates a multiplicative modifier based on the Job/class
    /// </summary>
    /// <param name="statType">Which stat is being queried</param>
    /// <param name="job">Current job to evaluate for</param>
    /// <returns>Multiplicative modifier to apply to stat</returns>
    public static double GetJobModifier(StatType statType, ClassJob? job)
    {
        if (job is null)
            return 1.0;
        return statType switch
        {
            StatType.Strength => job.ModifierStrength,
            StatType.Dexterity => job.ModifierDexterity,
            StatType.Intelligence => job.ModifierIntelligence,
            StatType.Mind => job.ModifierMind,
            StatType.Vitality => job.ModifierVitality,
            StatType.HP => job.ModifierHitPoints,
            StatType.MP => job.ModifierManaPoints,
            StatType.Piety => job.ModifierPiety,
            _ => 100
        } / 100f;
    }
    /// <summary>
    /// Calculates the Attack modifier. Named 'm' in AllagangStudies formulas
    /// </summary>
    /// <param name="level">current level of the job</param>
    /// <param name="job">curretn job</param>
    /// <returns>Attack Modifier 'm'</returns>
    public static double GetAttackModifierM(int level, ClassJob job) => (job.IsTank(), level) switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/LevelModifiers.cs
        (true, <= 80) => level + 35,
        (true, <= 90) => (level - 80) * 4.1 + 115,
        (_, <= 50) => 75,
        (_, <= 70) => (level - 50) * 2.5 + 75,
        (_, <= 80) => (level - 70) * 4.0 + 125,
        (_, <= 90) => (level - 80) * 3.0 + 165,
        _ => double.NaN,
    };
    /// <summary>
    /// Calculates the trait modifier
    /// </summary>
    /// <param name="level">current level of the job</param>
    /// <param name="job">curretn job</param>
    /// <returns>Trait modifier</returns>
    public static double GetTraitModifier(int level, ClassJob job) => (Job)job.RowId switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/JobInfo.cs
        Job.BLU => level switch
        {
            >= 50 => 1.5,
            >= 40 => 1.4,
            >= 30 => 1.3,
            >= 20 => 1.2,
            >= 10 => 1.1,
            _ => 1.0,
        },
        Job.DNC => level switch
        {
            >= 60 => 1.2,
            >= 50 => 1.1,
            _ => 1.0,
        },
        _ => (job.IsCaster(), job.IsPhysicalRanged(), level) switch
        {
            //Maim and mend
            (true, _, >= 40) => 1.3,
            (true, _, >= 20) => 1.1,
            (true, _, _) => 1.0,
            // increased action damage trait
            (_, true, >= 40) => 1.2,
            (_, true, >= 20) => 1.1,
            _ => 1.0,
        }
    };
    /// <summary>
    /// Calculates the Hp multiplier (aslo called ??). Bassically SE's way to balance Vitality to actual HP
    /// Currently depends on level and if you are tank or not
    /// </summary>
    /// <param name="level">Level of the job supplied</param>
    /// <param name="job">THe job to be evaluated for</param>
    /// <returns>A multiplier to calcualte HP from Vit</returns>
    public static double GetHPMultiplier(int level, ClassJob job) => job.IsTank() switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/LevelModifiers.cs
        true => 6.7 + 0.31 * level,
        _ => 4.5 + 0.22 * level
    };
    public static double CalcCritDamage(int criticalHit, int level)
        => Math.Floor(200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level) + 1400) / 1000d;
    public static double CalcCritRate(int criticalHit, int level)
        => Math.Floor(200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level) + 50) / 1000d;
    public static double CalcDirectHitRate(int directHit, int level)
        => Math.Floor(550 * (directHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    public static double CalcDeterminationMultiplier(int determination, int level)
        => Math.Floor(1000 + 140 * (determination - LevelTable.MAIN(level)) / LevelTable.DIV(level)) / 1000d;
    public static double CalcTenacityModifier(int tenacity, int level)
        => (Math.Floor(100 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000d;
    public static double CalcMPPerSecond(int piety, int level)
        => 200d + Math.Floor(150 * (piety - LevelTable.MAIN(level)) / (double)LevelTable.DIV(level));
    public static double CalcGCD(int speed, int level)
        => Math.Floor(2500d * (1000 + Math.Ceiling(130 * (LevelTable.SUB(level) - speed) / (double)LevelTable.DIV(level))) / 10000d) / 100d;
    public static double CalcAADotMultiplier(int speed, int level)
        => (1000d + Math.Ceiling(130d * (speed - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000d;
    public static double CalcDefenseMitigation(int defense, int level)
        => Math.Floor(15 * defense / LevelTable.DIV(level)) / 100d;
    public static double CalcHP(int vitality, int level, ClassJob job)
        => Math.Floor(LevelTable.HP(level) * job.ModifierHitPoints / 100d)
        + Math.Floor((vitality - LevelTable.MAIN(level)) * GetHPMultiplier(level, job));
    public static double CalcBaseDamage(int weaponDamage, int mainStat, int determination, int tenacity, int level, ClassJob job)
    {
        double m = GetAttackModifierM(level, job);
        double trait = GetTraitModifier(level, job);
        double baseDmg =
            Math.Floor((weaponDamage + Math.Floor(LevelTable.MAIN(level) * GetJobModifier(job.PrimaryStat, job) / 10d))
            * (100 + (mainStat - LevelTable.MAIN(level)) * m / LevelTable.MAIN(level))) / 100d;
        double determinationMultiplier = CalcDeterminationMultiplier(determination, level);
        double tenacityMultiplier = 1d + (job.IsTank() ? CalcTenacityModifier(tenacity, level) : 0d);
        return baseDmg * determinationMultiplier * tenacityMultiplier * trait / 100d;
    }
    public static double CalcAvarageDamage(int weaponDamage, int mainStat, int criticalHit, int directHit, int determination, int tenacity, int level, ClassJob job)
    {
        double baseDmg = CalcBaseDamage(weaponDamage, mainStat, determination, tenacity, level, job);
        double critRate = CalcCritRate(criticalHit, level);
        double DHRate = CalcDirectHitRate(directHit, level);
        double critDmgMod = CalcCritDamage(criticalHit, level);
        double dHDmgMod = 1.25;
        double critDHRate = critRate * DHRate;
        double normalHitRate = 1 - critRate - DHRate + critDHRate;
        return baseDmg * (normalHitRate + dHDmgMod * critDmgMod * critDHRate + critDmgMod * (critRate - critDHRate) + dHDmgMod * DHRate);
    }
}
