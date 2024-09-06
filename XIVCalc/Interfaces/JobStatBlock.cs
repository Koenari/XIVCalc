using Lumina.Excel.GeneratedSheets;

namespace XIVCalc.Interfaces;

public interface IJobStatBlock
{
    public ClassJob Job { get; }
    public int Level { get; }

    //Weapon stats
    public int WeaponDamage { get; }
    public int WeaponDelay { get; }

    public int MainStat => (StatType)Job.PrimaryStat switch
    {
        StatType.Strength => Strength,
        StatType.Dexterity => Dexterity,
        StatType.Intelligence => Intelligence,
        StatType.Mind => Mind,
        _ => 0,
    };
    
    //Main Stats
    public int Vitality { get; }
    public int Strength { get; }
    public int Dexterity { get; }
    public int Intelligence { get; }
    public int Mind { get; }

    public int PhysicalDefense { get; }
    public int MagicalDefense { get; }
    public int AttackPower { get; }
    public int AttackMagicPotency { get; }
    public int HealingMagicPotency { get; }
    //Secondary Stats
    public int DirectHit { get; }
    public int CriticalHit { get; }
    public int Determination { get; }
    public int SkillSpeed { get; }
    public int SpellSpeed { get; }
    //Role specifics
    public int Piety { get; }
    public int Tenacity { get; }
}
