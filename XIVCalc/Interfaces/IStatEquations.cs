using System.Diagnostics.CodeAnalysis;
using XIVCalc.Calculations;

namespace XIVCalc.Interfaces;

/// <summary>
/// Set of stat equations
/// </summary>
public interface IStatEquations
{
    /// <inheritdoc cref="StatEquations.GetJobModifier"/>
    public double GetJobModifier(StatType statType);

    /// <inheritdoc cref="StatEquations.GetAttackModifierM"/>
    public double GetAttackModifierM();

    /// <inheritdoc cref="StatEquations.GetAutoAttackStatModifier"/>
    public double GetAutoAttackStatModifier();

    /// <inheritdoc cref="StatEquations.GetTraitModifier(int,Job)"/>
    public double GetTraitModifier();

    /// <inheritdoc cref="StatEquations.GetHpMultiplier(int,Job)"/>
    public double GetHpMultiplier();

    /// <inheritdoc cref="StatEquations.CritDamage"/>
    public double CritDamage();

    /// <inheritdoc cref="StatEquations.CritChance"/>
    public double CritChance();

    /// <inheritdoc cref="StatEquations.DirectHitChance"/>
    public double DirectHitChance();

    /// <inheritdoc cref="StatEquations.DirectHitDamage(int,int)"/>
    public double DirectHitDamage();

    /// <inheritdoc cref="StatEquations.AutoDirectHitMultiplier"/>
    public double AutoDirectHitMultiplier();

    /// <inheritdoc cref="StatEquations.DeterminationMultiplier"/>
    public double DeterminationMultiplier();

    /// <inheritdoc cref="StatEquations.TenacityOffensiveModifier"/>
    public double TenacityOffensiveModifier();
    
    /// <inheritdoc cref="StatEquations.TenacityDefensiveModifier"/>
    public double TenacityDefensiveModifier();
    
    /// <inheritdoc cref="StatEquations.MpPerTick"/>
    public double MpPerTick();

    /// <inheritdoc cref="StatEquations.CalcGcd"/>
    public double Gcd();


    /// <inheritdoc cref="StatEquations.TickMultiplier"/>
    public double DotMultiplier();

    /// <inheritdoc cref="StatEquations.TickMultiplier"/>
    public double HotMultiplier();

    /// <inheritdoc cref="StatEquations.DefenseMitigation"/>
    public double PhysicalDefenseMitigation();

    
    /// <inheritdoc cref="StatEquations.DefenseMitigation"/>
    public double MagicalDefenseMitigation();
    
    /// <inheritdoc cref="StatEquations.MaxHp"/>
    public double MaxHp();

    /// <summary>
    /// Convert a Weapon Damage value to a damage multiplier.
    /// AkhMorning equivalent: F(WD)
    /// </summary>
    /// <returns>Damage Multiplier F(WD)</returns>
    public double WeaponDamageMultiplier();

    /// <summary>
    /// AkhMorning equivalent: F(AP) or F(ATK)
    /// </summary>
    /// <returns>Damage Multiplier F(AP)/F(ATK)</returns>
    public double MainStatMultiplier();

    /// <summary>
    /// Like <see cref="WeaponDamageMultiplier"/>, but for auto-attacks.
    /// AkhMorning equivalent: f(AUTO)
    /// </summary>
    /// <returns>Auto-attack damage multiplier f(AUTO)</returns>
    public double AutoAttackModifier();

    /// <summary>
    /// Calculates the base damage of a skill  with given properties (without crit or direct hit)
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <param name="attackType">Type of attack</param>
    /// <param name="isAutoCrit">If skill auto crits</param>
    /// <param name="isAutoDh">If skill auto direct hits</param>
    /// <param name="isDot">If is a dot</param>
    /// <returns>Damage</returns>
    public double BaseDamage(int potency, AttackType attackType = AttackType.Unknown, bool isAutoCrit = false, bool isAutoDh = false, bool isDot = false);

    /// <summary>
    /// Calculates avarage damage for a skill of given potency 
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <returns>Damage</returns>
    public double AverageSkillDamage(int potency);
}