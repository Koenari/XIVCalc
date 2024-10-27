namespace XIVCalc.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IJobStatBlock
{
    /// <inheritdoc cref="IJobModifiers"/>
    public IJobModifiers JobModifiers { get; }
    
    /// <summary>
    /// Current level
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// Weapon Damage
    /// </summary>
    public int WeaponDamage { get; }
    
    /// <summary>
    /// Weapon Delay
    /// </summary>
    public int WeaponDelay { get; }

    /// <summary>
    /// Main or Primary stat for that job
    /// </summary>
    public int MainStat => JobModifiers.PrimaryStat switch
    {
        StatType.Strength => Strength,
        StatType.Dexterity => Dexterity,
        StatType.Intelligence => Intelligence,
        StatType.Mind => Mind,
        _ => 0,
    };

    #region Main Stats
    
    /// <summary>
    /// Vitality
    /// </summary>
    public int Vitality { get; }
    
    /// <summary>
    /// Strength
    /// </summary>
    public int Strength { get; }
    
    /// <summary>
    /// Dexterity
    /// </summary>
    public int Dexterity { get; }
    
    /// <summary>
    /// Intelligence
    /// </summary>
    public int Intelligence { get; }
    
    /// <summary>
    /// Mind
    /// </summary>
    public int Mind { get; }

    /// <summary>
    /// Physical defense
    /// </summary>
    public int PhysicalDefense { get; }
    
    /// <summary>
    /// Magical defense
    /// </summary>
    public int MagicalDefense { get; }
    
    /// <summary>
    /// Attack power
    /// </summary>
    public int AttackPower { get; }
    
    /// <summary>
    /// Attack magic potency
    /// </summary>
    public int AttackMagicPotency { get; }
    
    /// <summary>
    /// Healing magic potency
    /// </summary>
    public int HealingMagicPotency { get; }

    #endregion

    #region Secondary Stats 

    /// <summary>
    /// Direct hit
    /// </summary>
    public int DirectHit { get; }
    
    /// <summary>
    /// Critical hit
    /// </summary>
    public int CriticalHit { get; }
    
    /// <summary>
    /// Determination
    /// </summary>
    public int Determination { get; }
    
    /// <summary>
    /// Skill speed
    /// </summary>
    public int SkillSpeed { get; }
    
    /// <summary>
    /// Spell speed
    /// </summary>
    public int SpellSpeed { get; }

    #endregion

    #region Role specific stats

    /// <summary>
    /// Piety
    /// </summary>
    public int Piety { get; }
    
    /// <summary>
    /// Tenacity
    /// </summary>
    public int Tenacity { get; }

    #endregion
}
