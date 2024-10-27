using XIVCalc.Interfaces;

namespace XIVCalc.Calculations;

/// <inheritdoc />
public class StatBlockEquations(IJobStatBlock statBlock) : IStatEquations
{
    /// <inheritdoc/>
    public double GetJobModifier(StatType statType) =>
        StatEquations.GetJobModifier(statType, statBlock.JobModifiers);

    /// <inheritdoc/>
    public double GetAttackModifierM() =>
        StatEquations.GetAttackModifierM(statBlock.Level, statBlock.JobModifiers.Job);
    
    /// <inheritdoc/>
    public double GetAutoAttackStatModifier() =>
        StatEquations.GetAutoAttackStatModifier(statBlock.JobModifiers);

    /// <inheritdoc/>
    public double GetTraitModifier()
        => StatEquations.GetTraitModifier(statBlock.Level, statBlock.JobModifiers.Job);
    
    /// <inheritdoc/>
    public double GetHpMultiplier() =>
        StatEquations.GetHpMultiplier(statBlock.Level, statBlock.JobModifiers.Job);

    /// <inheritdoc/>
    public double CritDamage() =>
        StatEquations.CritDamage(statBlock.CriticalHit, statBlock.Level);

    /// <inheritdoc/>
    public double CritChance() =>
        StatEquations.CritChance(statBlock.CriticalHit, statBlock.Level);
    
    /// <inheritdoc/>
    public double DirectHitChance() =>
        StatEquations.DirectHitChance(statBlock.DirectHit, statBlock.Level);

    /// <inheritdoc/>
    public double DirectHitDamage() => 
        StatEquations.DirectHitDamage(statBlock.DirectHit, statBlock.Level);

    /// <inheritdoc />
    public double AutoDirectHitMultiplier() =>
        StatEquations.AutoDirectHitMultiplier(statBlock.DirectHit, statBlock.Level);

    /// <inheritdoc/>
    public double DeterminationMultiplier() =>
        StatEquations.DeterminationMultiplier(statBlock.Determination, statBlock.Level);

    /// <inheritdoc/>
    public double TenacityOffensiveModifier() =>
        StatEquations.TenacityOffensiveModifier(statBlock.Tenacity, statBlock.Level);

    /// <inheritdoc/>
    public double TenacityDefensiveModifier() =>
        StatEquations.TenacityDefensiveModifier(statBlock.Tenacity, statBlock.Level);

    /// <inheritdoc/>
    public double MpPerTick() =>
        StatEquations.MpPerTick(statBlock.Piety, statBlock.Level);

    /// <inheritdoc/>
    public double Gcd() =>
        statBlock.JobModifiers.Job.IsCaster()
            ? StatEquations.SpsToGcd(statBlock.SpellSpeed, statBlock.Level)
            : StatEquations.SksToGcd(statBlock.SkillSpeed, statBlock.Level);

    /// <inheritdoc/>
    public double DotMultiplier() => 
        statBlock.JobModifiers.Job.IsCaster()
            ? StatEquations.DotMultiplier(statBlock.SpellSpeed, statBlock.Level)
            : StatEquations.DotMultiplier(statBlock.SkillSpeed, statBlock.Level);
    
    /// <inheritdoc/>
    public double HotMultiplier() => 
        statBlock.JobModifiers.Job.IsCaster()
            ? StatEquations.HotMultiplier(statBlock.SpellSpeed, statBlock.Level)
            : StatEquations.HotMultiplier(statBlock.SkillSpeed, statBlock.Level);

    /// <inheritdoc/>
    public double PhysicalDefenseMitigation() =>
        StatEquations.DefenseMitigation(statBlock.PhysicalDefense, statBlock.Level);

    /// <inheritdoc/>
    public double MagicalDefenseMitigation() =>
        StatEquations.DefenseMitigation(statBlock.MagicalDefense, statBlock.Level);

    /// <inheritdoc/>
    public double MaxHp() =>
        StatEquations.Hp(statBlock.Vitality, statBlock.Level, statBlock.JobModifiers);

    /// <inheritdoc/>
    public double WeaponDamageMultiplier() =>
        StatEquations.WeaponDamageMultiplier(statBlock.WeaponDamage, statBlock.Level,
                                             statBlock.JobModifiers);

    /// <inheritdoc/>
    public double MainStatMultiplier() =>
        StatEquations.MainStatMultiplier(statBlock.MainStat, statBlock.Level, statBlock.JobModifiers);

    /// <inheritdoc/>
    public double AutoAttackModifier() =>
        StatEquations.AutoAttackModifier(statBlock.WeaponDamage, statBlock.WeaponDelay,
                                         statBlock.Level, statBlock.JobModifiers);

    /// <inheritdoc/>
    public double BaseDamage(int potency, AttackType attackType = AttackType.Unknown, bool isAutoCrit = false, bool isAutoDh = false, bool isDot = false) =>
        StatEquations.BaseDamage(potency, statBlock, attackType, isAutoCrit, isAutoDh, isDot);

    /// <inheritdoc/>
    public double AverageSkillDamage(int potency) =>
        StatEquations.AverageSkillDamage(potency, statBlock);
}