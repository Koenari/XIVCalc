using System.Diagnostics.CodeAnalysis;
using Lumina.Excel.GeneratedSheets;
using XIVCalc.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
using static System.Math;

namespace XIVCalc.Calculations;

public enum AttackType
{
    Unknown,
    Weaponskill,
    AutoAttack
}

public static class StatEquations
{

    public const double BASE_GCD = 2.5;
    public const double DH_DMG = 1.25;
    
    /// <summary>
    /// Calculates a multiplicative modifier based on the Job/class
    /// </summary>
    /// <param name="statType">Which stat is being queried</param>
    /// <param name="job">Current job to evaluate for</param>
    /// <returns>Multiplicative modifier to apply to stat</returns>
    public static double GetJobModifier(byte statType, ClassJob? job)
    {
        return GetJobModifier((StatType)statType, job);
    }

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
            _ => 100,
        } / 100f;
    }

    /// <inheritdoc cref="GetAttackModifierM(int,Job)"/>
    public static double GetAttackModifierM(int level, ClassJob job)
    {
        return GetAttackModifierM(level, job.AsJob());
    }

    /// <summary>
    /// Calculates the Attack modifier. Named 'm' in AllaganStudies formulas
    /// </summary>
    /// <param name="level">current level of the job</param>
    /// <param name="job">current job</param>
    /// <returns>Attack Modifier 'm'</returns>
    public static double GetAttackModifierM(int level, Job job)
    {
        return (job.IsTank(), level) switch
        {
            //See: https://github.com/Kouzukii/ffxiv-characterstatus-refined/blob/master/CharacterPanelRefined/LevelModifiers.cs
            (true, <= 80) => level + 35,
            (true, <= 90) => (level - 80) * 4.1 + 115,
            (true, <= 100) => (level - 90) * 3.4 + 156,
            (_, <= 50)    => 75,
            (_, <= 70)    => (level - 50) * 2.5 + 75,
            (_, <= 80)    => (level - 70) * 4.0 + 125,
            (_, <= 90)    => (level - 80) * 3.0 + 165,
            (_, <= 100)   => (level - 90) * 4.2 + 195,
            _             => double.NaN,
        };
    }

    public static double GetAutoAttackStatModifier(ClassJob job)
    {
        var statType = job.AsJob() switch
        {
            Job.VPR or Job.NIN => StatType.Dexterity,
            Job.BRD or Job.DNC 
                    or Job.MCH => StatType.Dexterity,
            _                  => StatType.Strength,
        };
        return GetJobModifier(statType, job);
    }

    /// <inheritdoc cref="GetTraitModifier(int,Job)"/>
    public static double GetTraitModifier(int level, ClassJob job)
    {
        return GetTraitModifier(level, job.AsJob());
    }

    /// <summary>
    /// Calculates the trait modifier
    /// </summary>
    /// <param name="level">current level of the job</param>
    /// <param name="job">current job</param>
    /// <returns>Trait modifier</returns>
    public static double GetTraitModifier(int level, Job job)
    {
        return job switch
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
            },
        };
    }

    /// <inheritdoc cref="GetHpMultiplier(int,Job)"/>
    public static double GetHpMultiplier(int level, ClassJob job) => GetHpMultiplier(level, job.AsJob());
    

    /// <summary>
    /// Calculates the Hp multiplier (also called ??). Basically SE's way to balance Vitality to actual HP
    /// Currently depends on level and if you are tank or not
    /// </summary>
    /// <param name="level">Level of the job supplied</param>
    /// <param name="job">THe job to be evaluated for</param>
    /// <returns>A multiplier to calculate HP from Vit</returns>
    public static double GetHpMultiplier(int level, Job job)
    {
        return (job.IsTank(), level) switch
        {
            (false, 100) => 30.1,
            (false, 99)  => 29.5,
            (false, 98)  => 28.9,
            (false, 97)  => 28.4,
            (false, 96)  => 27.7,
            (false, 95)  => 27.2,
            (false, 94)  => 26.6,
            (false, 93)  => 26.0,
            (false, 92)  => 25.4,
            (false, 91)  => 24.8,
                
            (true, 100) => 43.0,
            (true, 99)  => 42.2,
            (true, 98)  => 41.3,
            (true, 97)  => 40.5,
            (true, 96)  => 39.6,
            (true, 95)  => 38.8,
            (true, 94)  => 38.0,
            (true, 93)  => 37.1,
            (true, 92)  => 36.3,
            (true, 91)  => 35.4,
            (true,_)    => 6.7 + 0.31 * level,
            _           => 4.5 + 0.22 * level,
        };
    }

    private static double MainStatPowerMod(int level, ClassJob job) => (job.IsTank(), level) switch
    {
        (_,< 70)   => 100,
        (true,< 80)   => 105,
        (false, < 80) => 125,
        (true, < 90) => 115,
        (false,< 90) => 165,
        (true, < 100) => 156,
        (false, < 100) => 195,
        (true, _) => 190,
        (_,_) => 237,

    };
    
    public static double CritDamage(int criticalHit, int level) =>
        Floor(1400 + 200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;

    public static double CritChance(int criticalHit, int level) => 
        Floor(50 + 200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    

    public static double DirectHitChance(int directHit, int level) => 
        Floor(550 * (directHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;

    public static double DirectHitDamage(int directHit, int level) => DirectHitDamage();
    
    private static double DirectHitDamage() => DH_DMG;

    public static double AutoDirectHitMultiplier(int directHit, int level) =>
        Floor(1000 + 140 * ((directHit - LevelTable.SUB(level))/LevelTable.DIV(level))) / 1000d;

    public static double DeterminationMultiplier(int determination, int level) =>
        Floor(1000 + 140 * (determination - LevelTable.MAIN(level)) / LevelTable.DIV(level)) / 1000d;

    public static double TenacityOffensiveModifier(int tenacity, int level) =>
        Floor(1000 + 112 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    
    
    public static double TenacityDefensiveModifier(int tenacity, int level) =>
        Floor(1000 - 200 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    
    public static double MpPerTick(int piety, int level) => 
        200d + Floor(150 * (piety - LevelTable.MAIN(level)) / LevelTable.DIV(level));

    /// <inheritdoc cref="CalcGcd"/>
    public static double SksToGcd(int sks, int level) => CalcGcd(sks, level);
    
    /// <inheritdoc cref="CalcGcd"/>
    public static double SpsToGcd(int sps, int level) => CalcGcd(sps, level);
    
    /// <summary>
    /// Calculates effective GCD
    /// </summary>
    /// <param name="speed">SKS or SPS value</param>
    /// <param name="level">Current level</param>
    /// <param name="haste">Haste on the player (if applicable)</param>
    /// <returns>GCD</returns>
    private static double CalcGcd(int speed, int level, int haste = 0)
    {
        return Floor(
                   Floor(
                       BASE_GCD * 
                       (1000 - Floor(130 * (speed -LevelTable.SUB(level)) / LevelTable.DIV(level)))
                       ) * (100-haste) / 1000d
                   )
              / 100d;
    }

    /// <inheritdoc cref="TickMultiplier"/>
    public static double DotMultiplier(int speed, int level) => TickMultiplier(speed, level);
    
    /// <inheritdoc cref="TickMultiplier"/>
    public static double HotMultiplier(int speed, int level) => TickMultiplier(speed, level);

    /// <inheritdoc cref="TickMultiplier"/>
    public static double SksTickMultiplier(int sks, int level) => TickMultiplier(sks, level);
    
    
    /// <inheritdoc cref="TickMultiplier"/>
    public static double SpsTickMultiplier(int sps, int level) => TickMultiplier(sps, level);
    
    /// <summary>
    /// Multiplier for ticking damage/healing based on SPS/SKS
    /// AkhMorning equivalent: F(SPD)
    /// </summary>
    /// <param name="speed">SPS or SKS</param>
    /// <param name="level">level</param>
    /// <returns>Multiplicative modifier for over time effects</returns>
    public static double TickMultiplier(int speed, int level) =>
        (1000d + Floor(130d * (speed - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000d;

    public static double DefenseMitigation(int defense, int level) =>
        Floor(15 * defense / LevelTable.DIV(level)) / 100d;

    /// <summary>
    /// Calculates max hit points
    /// </summary>
    /// <param name="vitality">Vitality value</param>
    /// <param name="level">Current level</param>
    /// <param name="job">Active job</param>
    /// <returns>Max HP</returns>
    public static double Hp(int vitality, int level, ClassJob job) =>
        Floor(LevelTable.HP(level) * job.ModifierHitPoints / 100d)
               + Floor((vitality - LevelTable.MAIN(level)) * GetHpMultiplier(level, job));

    /// <summary>
    /// Convert a Weapon Damage value to a damage multiplier.
    /// AkhMorning equivalent: F(WD)
    /// </summary>
    /// <param name="weaponDamage">Weapon damage value</param>
    /// <param name="level">level</param>
    /// <param name="job">Active job</param>
    /// <returns>Damage Multiplier F(WD)</returns>
    public static double WeaponDamageMultiplier(int weaponDamage, int level, ClassJob job) =>
        Floor(weaponDamage + LevelTable.MAIN(level) * GetJobModifier((StatType)job.PrimaryStat, job) / 1000d);

    /// <summary>
    /// Convert a main stat value to a damage multiplier.
    /// AkhMorning equivalent: F(AP) or F(ATK)
    /// </summary>
    /// <param name="mainStat">Value of main stat</param>
    /// <param name="level">Current level</param>
    /// <param name="job">Active job</param>
    /// <returns>Damage Multiplier F(AP)/F(ATK)</returns>
    public static double MainStatMultiplier(int mainStat, int level, ClassJob job) =>
        Max(0, (Floor(MainStatPowerMod(level,job) * (mainStat - LevelTable.MAIN(level)) / LevelTable.MAIN(level) + 100) / 100f));

    /// <summary>
    /// Like <see cref="WeaponDamageMultiplier"/>, but for auto-attacks.
    /// AkhMorning equivalent: f(AUTO)
    /// </summary>
    /// <param name="weaponDamage">Value of weapon damage</param>
    /// <param name="weaponDelay">Value of weapon delay</param>
    /// <param name="level">Current level</param>
    /// <param name="job"></param>
    /// <returns>Auto-attack damage multiplier f(AUTO)</returns>
    public static double AutoAttackModifier(int weaponDamage, int weaponDelay, int level, ClassJob job) =>
        Floor(Floor(weaponDamage + LevelTable.MAIN(level)* GetAutoAttackStatModifier(job)/1000d)*(weaponDelay *1000/3d))/1000d;

    private static bool UsesCasterDamageFormula(ClassJob job, AttackType attackType = AttackType.Unknown)
    {
        return job.IsCaster() && attackType != AttackType.AutoAttack;
    }
    
    /// <summary>
    /// Calculates the base damage of a skill  with given properties (without crit or direct hit)
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <param name="stats">Statblock</param>
    /// <param name="attackType">Type of attack</param>
    /// <param name="isAutoCrit">If skill auto crits</param>
    /// <param name="isAutoDh">If skill auto direct hits</param>
    /// <param name="isDot">If is a dot</param>
    /// <returns>Damage</returns>
    public static double BaseDamage(int potency,IJobStatBlock stats, 
                                    AttackType attackType = AttackType.Unknown,
                                    bool isAutoCrit = false, bool isAutoDh = false,
                                    bool isDot = false)
    {
        double spdMulti;
        bool isAA = attackType == AttackType.AutoAttack;
        if (isAA) {
            spdMulti = SksTickMultiplier(stats.SkillSpeed,stats.Level);
        }
        else if (isDot) {
            spdMulti = (attackType == AttackType.Weaponskill)
                ? SksTickMultiplier(stats.SkillSpeed, stats.Level)
                : SpsTickMultiplier(stats.SpellSpeed, stats.Level);
        }
        else {
            spdMulti = 1.0;
        }
        
        var mainStatMulti = MainStatMultiplier(stats.MainStat, stats.Level, stats.Job);
        var wdMulti = WeaponDamageMultiplier(stats.WeaponDamage, stats.Level, stats.Job);
        var critMulti = CritDamage(stats.CriticalHit, stats.Level);
        var critRate = CritChance(stats.CriticalHit, stats.Level);
        var dhRate = DirectHitChance(stats.DirectHit, stats.Level);
        var dhMulti = DirectHitDamage();
        var detMulti = DeterminationMultiplier(stats.Determination, stats.Level);
        var tncMulti = TenacityOffensiveModifier(stats.Tenacity, stats.Level);
        var detAutoDhMulti = detMulti + AutoDirectHitMultiplier(stats.DirectHit, stats.Level);
        var traitMulti = GetTraitModifier(stats.Level, stats.Job);
        var effectiveDetMulti = isAutoDh ? detAutoDhMulti : detMulti;

        double stage1Potency;
        if (UsesCasterDamageFormula(stats.Job, attackType)) {
            // https://github.com/Amarantine-xiv/Amas-FF14-Combat-Sim_source/blob/main/ama_xiv_combat_sim/simulator/calcs/compute_damage_utils.py#L130
            var apDet = Floor(100* mainStatMulti * effectiveDetMulti)/100;
            var basePotency = Floor(apDet * Floor(100 * wdMulti * potency)/100);
            // Factor in Tenacity multiplier
            var afterTnc = Floor(100 * basePotency * tncMulti)/100;
            // noinspection UnnecessaryLocalVariableJS
            var afterSpd = Floor(1000* afterTnc * spdMulti)/1000;
            stage1Potency = afterSpd;
        }
        else {
            var basePotency = Floor(100 * potency * mainStatMulti)/100;
            // Factor in determination and auto DH multiplier
            var afterDet = Floor(100 * basePotency * effectiveDetMulti)/100;
            // Factor in Tenacity multiplier
            var afterTnc = Floor(100 * afterDet * tncMulti)/100;
            var afterSpd = Floor(1000 * afterTnc * spdMulti)/1000;
            // Factor in weapon damage multiplier
            stage1Potency = Floor(afterSpd * wdMulti);
        }

        var afterAutoCrit = isAutoCrit
            ? Floor(stage1Potency * (1 + critRate * (critMulti - 1)))
            : stage1Potency;
        var afterAutoDh =
            isAutoDh ? Floor(afterAutoCrit * (1 + dhRate * (dhMulti - 1))) : afterAutoCrit;
        return Floor(Floor(afterAutoDh * traitMulti) / 100d);
    }

    /// <summary>
    /// Calculates DPS if skill of given potency is repeated every GCD 
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <param name="stats">Statblock</param>
    /// <returns>"DPS"</returns>
    public static double AverageSkillDamagePerSecond(int potency,IJobStatBlock stats)
    {
        var baseDmg = BaseDamage(potency,stats);
        var critRate = CritChance(stats.CriticalHit, stats.Level);
        var dhRate = DirectHitChance(stats.DirectHit, stats.Level);
        var critDmgMod = CritDamage(stats.CriticalHit, stats.Level);
        var dhDmgMod = DirectHitDamage(stats.DirectHit, stats.Level);
        return baseDmg * (1 + (dhDmgMod - 1) * dhRate) * (1 + (1 - critDmgMod) * critRate)
             / (stats.Job.IsCaster()
                   ? SpsToGcd(stats.SpellSpeed, stats.Level)
                   : SksToGcd(stats.SkillSpeed, stats.Level));
    }
}

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IStatEquations
{
    /// <inheritdoc cref="StatEquations.GetJobModifier(StatType,ClassJob?)"/>
    public double GetJobModifier(StatType statType);

    /// <inheritdoc cref="StatEquations.GetAttackModifierM(int,Job)"/>
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

    /// <inheritdoc cref="StatEquations.DirectHitDamage"/>
    public double DirectHitDamage();

    /// <inheritdoc cref="StatEquations.AutoDirectHitMultiplier"/>
    public double AutoDirectHitMultiplier();

    public double DeterminationMultiplier();

    public double TenacityOffensiveModifier();
    
    
    public double TenacityDefensiveModifier();
    
    public double MpPerTick();

    /// <inheritdoc cref="StatEquations.CalcGcd"/>
    public double Gcd();


    /// <inheritdoc cref="StatEquations.TickMultiplier"/>
    public double DotMultiplier();

    /// <inheritdoc cref="StatEquations.TickMultiplier"/>
    public double HotMultiplier();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double PhysicalDefenseMitigation();

    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double MagicalDefenseMitigation();
    
    /// <summary>
    /// Calculates max hit points
    /// </summary>
    /// <returns>Max HP</returns>
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

    public double AutoAttackModifier();

    public double BaseDamage(int potency);

    /// <summary>
    /// Calculates DPS if skill of given potency is repeated every GCD 
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <returns>"DPS"</returns>
    public double AverageSkillDamagePerSecond(int potency);
}

/// <inheritdoc />
public class StatBlockEquations(IJobStatBlock statBlock) : IStatEquations
{
    /// <inheritdoc/>
    public double GetJobModifier(StatType statType) =>
        StatEquations.GetJobModifier(statType, statBlock.Job);

    /// <inheritdoc/>
    public double GetAttackModifierM() =>
        StatEquations.GetAttackModifierM(statBlock.Level, statBlock.Job.AsJob());
    
    /// <inheritdoc/>
    public double GetAutoAttackStatModifier() =>
        StatEquations.GetAutoAttackStatModifier(statBlock.Job);

    /// <inheritdoc/>
    public double GetTraitModifier()
        => StatEquations.GetTraitModifier(statBlock.Level, statBlock.Job.AsJob());
    
    /// <inheritdoc/>
    public double GetHpMultiplier() =>
        StatEquations.GetHpMultiplier(statBlock.Level, statBlock.Job.AsJob());

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
        statBlock.Job.IsCaster()
            ? StatEquations.SpsToGcd(statBlock.SpellSpeed, statBlock.Level)
            : StatEquations.SksToGcd(statBlock.SkillSpeed, statBlock.Level);

    /// <inheritdoc/>
    public double DotMultiplier() => 
        statBlock.Job.IsCaster()
            ? StatEquations.DotMultiplier(statBlock.SpellSpeed, statBlock.Level)
            : StatEquations.DotMultiplier(statBlock.SkillSpeed, statBlock.Level);
    
    /// <inheritdoc/>
    public double HotMultiplier() => 
        statBlock.Job.IsCaster()
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
        StatEquations.Hp(statBlock.Vitality, statBlock.Level, statBlock.Job);

    /// <inheritdoc/>
    public double WeaponDamageMultiplier() =>
        StatEquations.WeaponDamageMultiplier(statBlock.WeaponDamage, statBlock.Level,
                                             statBlock.Job);

    /// <inheritdoc/>
    public double MainStatMultiplier() =>
        StatEquations.MainStatMultiplier(statBlock.MainStat, statBlock.Level, statBlock.Job);

    //// <inheritdoc/>
    public double AutoAttackModifier() =>
        StatEquations.AutoAttackModifier(statBlock.WeaponDamage, statBlock.WeaponDelay,
                                         statBlock.Level, statBlock.Job);

    /// <inheritdoc/>
    public double BaseDamage(int potency) =>
        StatEquations.BaseDamage(potency, statBlock);

    /// <inheritdoc/>
    public double AverageSkillDamagePerSecond(int potency) =>
        StatEquations.AverageSkillDamagePerSecond(potency, statBlock);
}