using XIVCalc.Interfaces;
using static System.Math;

namespace XIVCalc.Calculations;

/// <summary>
/// Collection of FFXIV equations
/// </summary>
public static class StatEquations
{

    private const double BaseGcd = 2.5;
    private const double DhDmg = 1.25;
    /// <summary>
    /// Calculates a multiplicative modifier based on the Job/class
    /// </summary>
    /// <param name="statType">Which stat is being queried</param>
    /// <param name="job">Current job to evaluate for</param>
    /// <returns>Multiplicative modifier to apply to stat</returns>
    public static double GetJobModifier(StatType statType, IJobModifiers? job)
    {
        if (job is null)
            return 1.0;
        return statType switch
        {
            StatType.Strength => job.Strength,
            StatType.Dexterity => job.Dexterity,
            StatType.Intelligence => job.Intelligence,
            StatType.Mind => job.Mind,
            StatType.Vitality => job.Vitality,
            StatType.HP => job.HitPoints,
            StatType.MP => job.ManaPoints,
            StatType.Piety => job.Piety,
            _ => 100,
        } / 100f;
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

    /// <summary>
    /// Modifier for auto attacks 
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    public static double GetAutoAttackStatModifier(IJobModifiers job)
    {
        var statType = job.Job switch
        {
            Job.VPR or Job.NIN => StatType.Dexterity,
            Job.BRD or Job.DNC 
                    or Job.MCH => StatType.Dexterity,
            _                  => StatType.Strength,
        };
        return GetJobModifier(statType, job);
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

    private static double MainStatPowerMod(int level, IJobModifiers job) => (job.Job.IsTank(), level) switch
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
    
    /// <summary>
    /// Damage modifier for a critical hit
    /// </summary>
    /// <param name="criticalHit">Value of critical hit stat</param>
    /// <param name="level">Current level</param>
    /// <returns>Damage modifier</returns>
    public static double CritDamage(int criticalHit, int level) =>
        Floor(1400 + 200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;

    /// <summary>
    /// Chance to land a critical hit
    /// </summary>
    /// <param name="criticalHit">Value of critical hit stat</param>
    /// <param name="level">Current level</param>
    /// <returns>Crit chance</returns>
    public static double CritChance(int criticalHit, int level) => 
        Floor(50 + 200 * (criticalHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    

    /// <summary>
    /// Chance to land a direct hit
    /// </summary>
    /// <param name="directHit">Value of direct hit stat</param>
    /// <param name="level">Current level</param>
    /// <returns>Dh chance</returns>
    public static double DirectHitChance(int directHit, int level) => 
        Floor(550 * (directHit - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;

    /// <summary>
    /// Calculates direct hit damage multiplier
    /// </summary>
    /// <param name="directHit">Current Direct Hit value</param>
    /// <param name="level">Current level</param>
    /// <returns>Damage multiplier</returns>
    public static double DirectHitDamage(int directHit, int level) => DirectHitDamage();
    
    private static double DirectHitDamage() => DhDmg;

    /// <summary>
    /// Damage multiplier for skill that automatically direct hit
    /// </summary>
    /// <param name="directHit">Current Direct Hit value</param>
    /// <param name="level">Current level</param>
    /// <returns>Dmg multiplier</returns>
    public static double AutoDirectHitMultiplier(int directHit, int level) =>
        Floor(1000 + 140 * ((directHit - LevelTable.SUB(level))/LevelTable.DIV(level))) / 1000d;

    /// <summary>
    /// Damage multiplier affected by determination
    /// </summary>
    /// <param name="determination">Current det value</param>
    /// <param name="level">Current level</param>
    /// <returns>Dmg multiplier</returns>
    public static double DeterminationMultiplier(int determination, int level) =>
        Floor(1000 + 140 * (determination - LevelTable.MAIN(level)) / LevelTable.DIV(level)) / 1000d;

    /// <summary>
    /// Damage multiplier affected by tenacity
    /// </summary>
    /// <param name="tenacity">Current tenacity value</param>
    /// <param name="level">Current level</param>
    /// <returns>Dmg multiplier</returns>
    public static double TenacityOffensiveModifier(int tenacity, int level) =>
        Floor(1000 + 112 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level)) / 1000d;
    
    /// <summary>
    /// Incoming damage multiplier affected by tenacity
    /// </summary>
    /// <param name="tenacity">Current tenacity value</param>
    /// <param name="level">Current level</param>
    /// <returns>Incoming damage multiplier</returns>
    public static double TenacityDefensiveModifier(int tenacity, int level) =>
        (1000 - Floor(200 * (tenacity - LevelTable.SUB(level)) / LevelTable.DIV(level))) / 1000d;
    
    /// <summary>
    /// MP gained per tick
    /// </summary>
    /// <param name="piety">Current piety value</param>
    /// <param name="level">Current level</param>
    /// <returns>MP/t</returns>
    public static double MpPerTick(int piety, int level) => 
        200d + Floor(150 * (piety - LevelTable.MAIN(level)) / LevelTable.DIV(level));

    /// <inheritdoc cref="CalcGcd"/>
    public static double SksToGcd(int sks, int level, int haste = 0) => CalcGcd(sks, level, haste);
    
    /// <inheritdoc cref="CalcGcd"/>
    public static double SpsToGcd(int sps, int level, int haste = 0) => CalcGcd(sps, level, haste);
    
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
                       BaseGcd * 
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

    /// <summary>
    /// Calculates how much incoming damage ist mitigated
    /// </summary>
    /// <param name="defense">Value of defense corresponding to damage type</param>
    /// <param name="level">Current level</param>
    /// <returns>Fraction of damage mitigated</returns>s
    public static double DefenseMitigation(int defense, int level) =>
        Floor(15 * defense / LevelTable.DIV(level)) / 100d;

    /// <summary>
    /// Calculates max hit points
    /// </summary>
    /// <param name="vitality">Vitality value</param>
    /// <param name="level">Current level</param>
    /// <param name="jobMod">Active job</param>
    /// <returns>Max HP</returns>
    public static double MaxHp(int vitality, int level, IJobModifiers jobMod) =>
        Floor(LevelTable.HP(level) * jobMod.HitPoints / 100d)
               + Floor((vitality - LevelTable.MAIN(level)) * GetHpMultiplier(level, jobMod.Job));

    /// <summary>
    /// Convert a Weapon Damage value to a damage multiplier.
    /// AkhMorning equivalent: F(WD)
    /// </summary>
    /// <param name="weaponDamage">Weapon damage value</param>
    /// <param name="level">level</param>
    /// <param name="job">Active job</param>
    /// <returns>Damage Multiplier F(WD)</returns>
    public static double WeaponDamageMultiplier(int weaponDamage, int level, IJobModifiers job) =>
        Floor(weaponDamage + LevelTable.MAIN(level) * GetJobModifier(job.PrimaryStat, job) / 10d) / 100d;

    /// <summary>
    /// Convert a main stat value to a damage multiplier.
    /// AkhMorning equivalent: F(AP) or F(ATK)
    /// </summary>
    /// <param name="mainStat">Value of main stat</param>
    /// <param name="level">Current level</param>
    /// <param name="job">Active job</param>
    /// <returns>Damage Multiplier F(AP)/F(ATK)</returns>
    public static double MainStatMultiplier(int mainStat, int level, IJobModifiers job) =>
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
    public static double AutoAttackModifier(int weaponDamage, int weaponDelay, int level, IJobModifiers job) =>
        Floor(Floor(weaponDamage + LevelTable.MAIN(level)* GetAutoAttackStatModifier(job)/1000d)*(weaponDelay *1000/3d))/1000d;

    private static bool UsesCasterDamageFormula(IJobModifiers job, AttackType attackType = AttackType.Unknown)
    {
        return job.Job.IsCaster() && attackType != AttackType.AutoAttack;
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
        var isAa = attackType == AttackType.AutoAttack;
        if (isAa) {
            spdMulti = SksTickMultiplier(stats.SkillSpeed,stats.Level);
        }
        else if (isDot) {
            spdMulti = (attackType == AttackType.WeaponSkill)
                ? SksTickMultiplier(stats.SkillSpeed, stats.Level)
                : SpsTickMultiplier(stats.SpellSpeed, stats.Level);
        }
        else {
            spdMulti = 1.0;
        }
        
        var mainStatMulti = MainStatMultiplier(stats.MainStat, stats.Level, stats.JobModifiers);
        var wdMulti = WeaponDamageMultiplier(stats.WeaponDamage, stats.Level, stats.JobModifiers);
        var critMulti = CritDamage(stats.CriticalHit, stats.Level);
        var critRate = CritChance(stats.CriticalHit, stats.Level);
        var dhRate = DirectHitChance(stats.DirectHit, stats.Level);
        var dhMulti = DirectHitDamage();
        var detMulti = DeterminationMultiplier(stats.Determination, stats.Level);
        var tncMulti = TenacityOffensiveModifier(stats.Tenacity, stats.Level);
        var detAutoDhMulti = detMulti + AutoDirectHitMultiplier(stats.DirectHit, stats.Level);
        var traitMulti = GetTraitModifier(stats.Level, stats.JobModifiers.Job);
        var effectiveDetMulti = isAutoDh ? detAutoDhMulti : detMulti;

        double stage1Potency;
        if (UsesCasterDamageFormula(stats.JobModifiers, attackType)) {
            // https://github.com/Amarantine-xiv/Amas-FF14-Combat-Sim_source/blob/main/ama_xiv_combat_sim/simulator/calcs/compute_damage_utils.py#L130
            var apDet = Floor(100* mainStatMulti * effectiveDetMulti)/100;
            var basePotency = Floor(apDet * Floor( wdMulti * potency));
            // Factor in Tenacity multiplier
            var afterTnc = Floor(basePotency * tncMulti);
            var afterSpd = Floor(afterTnc * spdMulti);
            stage1Potency = afterSpd;
        }
        else {
            var basePotency = Floor(potency * mainStatMulti);
            // Factor in determination and auto DH multiplier
            var afterDet = Floor(basePotency * effectiveDetMulti);
            // Factor in Tenacity multiplier
            var afterTnc = Floor(afterDet * tncMulti);
            var afterSpd = Floor(afterTnc * spdMulti);
            // Factor in weapon damage multiplier
            stage1Potency = Floor(afterSpd * wdMulti);
        }

        var afterAutoCrit = isAutoCrit
            ? Floor(stage1Potency * (1 + critRate * (critMulti - 1)))
            : stage1Potency;
        var afterAutoDh =
            isAutoDh ? Floor(afterAutoCrit * (1 + dhRate * (dhMulti - 1))) : afterAutoCrit;
        return Floor(afterAutoDh * traitMulti)+(potency <100 ? 1:0);
    }

    /// <summary>
    /// Calculates DPS if skill of given potency is repeated every GCD 
    /// </summary>
    /// <param name="potency">Potency of the skill</param>
    /// <param name="stats">Statblock</param>
    /// <returns>"DPS"</returns>
    public static double AverageSkillDamage(int potency,IJobStatBlock stats)
    {
        var baseDmg = BaseDamage(potency,stats);
        var critRate = CritChance(stats.CriticalHit, stats.Level);
        var dhRate = DirectHitChance(stats.DirectHit, stats.Level);
        var critDmgMod = CritDamage(stats.CriticalHit, stats.Level);
        var dhDmgMod = DirectHitDamage(stats.DirectHit, stats.Level);
        return Floor(10 * baseDmg * (1 + (dhDmgMod - 1) * dhRate + (critDmgMod-1) * critRate))/10;
    }

    /// <summary>
    /// Applies the party bonus to main stat
    /// </summary>
    /// <param name="partyBonus"></param>
    /// <param name="mainStat"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double MainStatWithPartyBonus(PartyBonus partyBonus, double mainStat) =>
        partyBonus switch
        {
            PartyBonus.ThreePercent => mainStat * 1.03,
            PartyBonus.FourPercent  => mainStat * 1.04,
            PartyBonus.FivePercent  => mainStat * 1.05,
            PartyBonus.None or _    => mainStat,
        };
}



