using Lumina.Excel.GeneratedSheets;
using XIVCalc.Interfaces;

namespace XIVCalc.Tests;

public class JobStatMock : IJobStatBlock
{
    public ClassJob Job { get; init; } = new ClassJob();
    public int Level { get; init; }
    public int WeaponDamage { get;init; }
    public int WeaponDelay { get; init;}
    public int Vitality { get; init;}
    public int Strength { get; init;}
    public int Dexterity { get; init;}
    public int Intelligence { get; init;}
    public int Mind { get; init;}
    public int PhysicalDefense { get; init;}
    public int MagicalDefense { get; init;}
    public int AttackPower { get; init;}
    public int AttackMagicPotency { get; init;}
    public int HealingMagicPotency { get; init;}
    public int DirectHit { get; init;}
    public int CriticalHit { get; init;}
    public int Determination { get; init;}
    public int SkillSpeed { get; init;}
    public int SpellSpeed { get; init;}
    public int Piety { get; init;}
    public int Tenacity { get; init;}
}