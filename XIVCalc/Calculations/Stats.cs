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
    public static float GetJobModifier(byte statType, ClassJob? job) => GetJobModifier((StatType)statType, job);
    /// <summary>
    /// Calculates a multiplicative modifier based on the Job/class
    /// </summary>
    /// <param name="statType">Which stat is being queried</param>
    /// <param name="job">Current job to evaluate for</param>
    /// <returns>Multiplicative modifier to apply to stat</returns>
    public static float GetJobModifier(StatType statType, ClassJob? job)
    {
        if (job is null)
            return 1f;
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
    public static float GetAttackModifierM(int level, ClassJob job) => (job.IsTank(), level) switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/LevelModifiers.cs
        (true, <= 80) => level + 35,
        (true, <= 90) => (level - 80) * 4.1f + 115,
        (_, <= 50) => 75,
        (_, <= 70) => (level - 50) * 2.5f + 75,
        (_, <= 80) => (level - 70) * 4 + 125,
        (_, <= 90) => (level - 80) * 3 + 165,
        _ => float.NaN,
    };
    /// <summary>
    /// Calculates the trait modifier
    /// </summary>
    /// <param name="level">current level of the job</param>
    /// <param name="job">curretn job</param>
    /// <returns>Trait modifier</returns>
    public static float GetTraitModifier(int level, ClassJob job) => (Job)job.RowId switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/JobInfo.cs
        Job.BLU => level switch
        {
            >= 50 => 1.5f,
            >= 40 => 1.4f,
            >= 30 => 1.3f,
            >= 20 => 1.2f,
            >= 10 => 1.1f,
            _ => 1,
        },
        Job.DNC => level switch
        {
            >= 60 => 1.2f,
            >= 50 => 1.1f,
            _ => 1f,
        },
        _ => (job.IsCaster(), job.IsPhysicalRanged(), level) switch
        {
            //Maim and mend
            (true, _, >= 40) => 1.3f,
            (true, _, >= 20) => 1.1f,
            (true, _, _) => 1.0f,
            // increased action damage trait
            (_, true, >= 40) => 1.2f,
            (_, true, >= 20) => 1.1f,
            _ => 1f,
        }
    };
    /// <summary>
    /// Calculates the Hp multiplier (aslo called ??). Bassically SE's way to balance Vitality to actual HP
    /// Currently depends on level and if you are tank or not
    /// </summary>
    /// <param name="level">Level of the job supplied</param>
    /// <param name="job">THe job to be evaluated for</param>
    /// <returns>A multiplier to calcualte HP from Vit</returns>
    public static float GetHPMultiplier(int level, ClassJob job) => job.IsTank() switch
    {
        //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/LevelModifiers.cs
        true => 6.7f + 0.31f * level,
        _ => 4.5f + 0.22f * level
    };
    public static float CalcCritDamage(int criticalHit, int level)
        => MathF.Floor(200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level) + 1400) / 1000f;
    public static float CalcCritRate(int criticalHit, int level)
        => MathF.Floor(200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level) + 50) / 1000f;
    public static float CalcDirectHitRate(int directHit, int level)
        => MathF.Floor(550 * (directHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000f;
    public static float CalcDeterminationMultiplier(int determination, int level)
        => MathF.Floor(1000 + 140 * (determination - LevelTable.MAIN(level)) / LevelTable.DIV(level)) / 1000f;
    public static float CalcTenacityModifier(int tenacity, int level)
        => (MathF.Floor(100 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000f;
    public static float CalcMPPerSecond(int piety, int level)
        => 200f + MathF.Floor(150 * (piety - LevelTable.MAIN(level)) / LevelTable.DIV(level));
    public static float CalcGCD(int speed, int level)
        => MathF.Floor(2500f * (1000 + MathF.Ceiling(130 * (LevelTable.SUB(level) - speed) / LevelTable.DIV(level))) / 10000f) / 100f;
    public static float CalcAADotMultiplier(int speed, int level)
        => (1000f + MathF.Ceiling(130f * (speed - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000f;
    public static float CalcDefenseMitigation(int defense, int level)
        => MathF.Floor(15 * defense / LevelTable.DIV(level)) / 100f;
    public static float CalcHP(int vitality, int level, ClassJob job)
        => MathF.Floor(LevelTable.HP(level) * job.ModifierHitPoints / 100f)
        + MathF.Floor((vitality - LevelTable.MAIN(level)) * GetHPMultiplier(level, job));
    public static float CalcBaseDamage(int weaponDamage, int mainStat, int determination, int tenacity, int level, ClassJob job)
    {
        float m = GetAttackModifierM(level, job);
        float trait = GetTraitModifier(level, job);
        float baseDmg =
            MathF.Floor((weaponDamage + MathF.Floor(LevelTable.MAIN(level) * GetJobModifier(job.PrimaryStat, job) / 10f))
            * (100 + (mainStat - LevelTable.MAIN(level)) * m / LevelTable.MAIN(level))) / 100f;
        float determinationMultiplier = CalcDeterminationMultiplier(determination, level);
        float tenacityMultiplier = 1f + (job.IsTank() ? CalcTenacityModifier(tenacity, level) : 0f);
        return baseDmg * determinationMultiplier * tenacityMultiplier * trait / 100f;
    }
    public static float CalcAvarageDamage(int weaponDamage, int mainStat, int criticalHit, int directHit, int determination, int tenacity, int level, ClassJob job)
    {
        float baseDmg = CalcBaseDamage(weaponDamage, mainStat, determination, tenacity, level, job);
        float critRate = CalcCritRate(criticalHit, level);
        float DHRate = CalcDirectHitRate(directHit, level);
        float critDmgMod = CalcCritDamage(criticalHit, level);
        float dHDmgMod = 1.25f;
        float critDHRate = critRate * DHRate;
        float normalHitRate = 1 - critRate - DHRate + critDHRate;
        return baseDmg * (normalHitRate + dHDmgMod * critDmgMod * critDHRate + critDmgMod * (critRate - critDHRate) + dHDmgMod * DHRate);
    }
}
