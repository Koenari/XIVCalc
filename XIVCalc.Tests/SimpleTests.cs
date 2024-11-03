using Lumina.Excel.GeneratedSheets;
using XIVCalc.Calculations;
using XIVCalc.Interfaces;
using XIVCalc.Jobs;

namespace XIVCalc.Tests;

public class SimpleTests
{
    private readonly IJobModifiers _whm = StaticJobs.WHM;
    
    private readonly StatBlockEquations _statsMin = new(new JobStatMock
    {
        Level = 100,
        JobModifiers = StaticJobs.WHM,
        Mind = 440,
        WeaponDamage = 0,
        Determination = 440,
        Tenacity = 420,
        Piety = 421,
        SpellSpeed = 420,
        CriticalHit = 420,
        DirectHit = 420,
        Vitality = 440,
    });

    private readonly StatBlockEquations _statsOne = new(new JobStatMock()
    {
        Level = 100,
        JobModifiers = StaticJobs.WHM,
        Mind = 452,
        WeaponDamage = 27,
        Determination = 916,
        Tenacity = 685,
        Piety = 756,
        SpellSpeed = 756,
        CriticalHit = 756,
        DirectHit = 756,
        Vitality = 916,
    });
    
    private readonly StatBlockEquations _statsTwo = new(new JobStatMock()
    {
        Level = 100,
        JobModifiers = StaticJobs.WHM,
        Mind = 531,
        WeaponDamage = 101,
        Determination = 1513,
        Tenacity = 1165,
        Piety = 1182,
        SpellSpeed = 1182,
        CriticalHit = 1182,
        DirectHit = 1182,
        Vitality = 1513,
    });
    private readonly StatBlockEquations _statsThree = new(new JobStatMock()
    {
        Level = 100,
        JobModifiers = StaticJobs.WHM,
        Mind = 555,
        WeaponDamage = 133,
        Determination = 1930,
        Tenacity = 1587,
        Piety = 1627,
        SpellSpeed = 1627,
        CriticalHit = 1627,
        DirectHit = 1627,
        Vitality = 1930,
        
    });
    private readonly StatBlockEquations _statsFour = new(new JobStatMock()
    {
        Level = 100,
        JobModifiers = StaticJobs.WHM,
        Mind = 629,
        WeaponDamage = 200,
        Determination = 2445,
        Tenacity = 2033,
        Piety = 2108,
        SpellSpeed = 2108,
        CriticalHit = 2108,
        DirectHit = 2108,
        Vitality = 2445,
    });

    //https://xivgear.app/?page=sl%7Cf08b1517-d55b-485d-b474-13ae5bcd7b16
    private readonly StatBlockEquations _dmgTest = new(new JobStatMock()
    {
        JobModifiers = StaticJobs.WHM,
        Level    = 100,
        WeaponDamage = 141,
        Mind = 4328,
        CriticalHit = 1510,
        Determination = 2272,
        DirectHit = 420,
        AttackMagicPotency = 4328,
        HealingMagicPotency = 4328,
        SpellSpeed = 665,
        Piety = 1711,
        Tenacity = 420,
    });
    
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void TestDamage()
    {
        Assert.That(_dmgTest.BaseDamage(100), Is.EqualTo(5946));
        Assert.That(_dmgTest.AverageSkillDamage(100), Is.EqualTo(6309.8));
    }
    
    [Test]
    public void TestMainStatMulti()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.MainStatMultiplier(), Is.EqualTo(1d));
            Assert.That(_statsOne.MainStatMultiplier(), Is.EqualTo(1.06));
            Assert.That(_statsTwo.MainStatMultiplier(), Is.EqualTo(1.49));
            Assert.That(_statsThree.MainStatMultiplier(), Is.EqualTo(1.61));
            Assert.That(_statsFour.MainStatMultiplier(), Is.EqualTo(2.01));
        });
    }

    [Test]
    public void TestWeaponDamage()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.WeaponDamageMultiplier(), Is.EqualTo(0.5));
            Assert.That(_statsOne.WeaponDamageMultiplier(), Is.EqualTo(0.77));
            Assert.That(_statsTwo.WeaponDamageMultiplier(), Is.EqualTo(1.51));
            Assert.That(_statsThree.WeaponDamageMultiplier(), Is.EqualTo(1.83));
            Assert.That(_statsFour.WeaponDamageMultiplier(), Is.EqualTo(2.5));
        });
    }
    
    [Test]
    public void TestDetermination()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.DeterminationMultiplier(), Is.EqualTo(1.000));
            Assert.That(_statsOne.DeterminationMultiplier(), Is.EqualTo(1.023));
            Assert.That(_statsTwo.DeterminationMultiplier(), Is.EqualTo(1.054));
            Assert.That(_statsThree.DeterminationMultiplier(), Is.EqualTo(1.075));
            Assert.That(_statsFour.DeterminationMultiplier(), Is.EqualTo(1.100));
        });
    }
    
    [Test]
    public void TestTenacity()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.TenacityOffensiveModifier(), Is.EqualTo(1.000));
            Assert.That(_statsOne.TenacityOffensiveModifier(), Is.EqualTo(1.010));
            Assert.That(_statsTwo.TenacityOffensiveModifier(), Is.EqualTo(1.030));
            Assert.That(_statsThree.TenacityOffensiveModifier(), Is.EqualTo(1.047));
            Assert.That(_statsFour.TenacityOffensiveModifier(), Is.EqualTo(1.064));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.TenacityDefensiveModifier(), Is.EqualTo(1.000));
            Assert.That(_statsOne.TenacityDefensiveModifier(), Is.EqualTo(0.981));
            Assert.That(_statsTwo.TenacityDefensiveModifier(), Is.EqualTo(0.947));
            Assert.That(_statsThree.TenacityDefensiveModifier(), Is.EqualTo(0.917));
            Assert.That(_statsFour.TenacityDefensiveModifier(), Is.EqualTo(0.884));
        });
    }
    
    [Test]
    public void TestPiety()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.MpPerTick(), Is.EqualTo(198));
            Assert.That(_statsOne.MpPerTick(), Is.EqualTo(217));
            Assert.That(_statsTwo.MpPerTick(), Is.EqualTo(240));
            Assert.That(_statsThree.MpPerTick(), Is.EqualTo(264));
            Assert.That(_statsFour.MpPerTick(), Is.EqualTo(290));
        });
    }
    
    [Test]
    public void TestSps()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.Gcd(), Is.EqualTo(2.50));
            Assert.That(_statsOne.Gcd(), Is.EqualTo(2.46));
            Assert.That(_statsTwo.Gcd(), Is.EqualTo(2.41));
            Assert.That(_statsThree.Gcd(), Is.EqualTo(2.36));
            Assert.That(_statsFour.Gcd(), Is.EqualTo(2.30));
        });
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.DotMultiplier(), Is.EqualTo(1.000));
            Assert.That(_statsOne.DotMultiplier(), Is.EqualTo(1.015));
            Assert.That(_statsTwo.DotMultiplier(), Is.EqualTo(1.035));
            Assert.That(_statsThree.DotMultiplier(), Is.EqualTo(1.056));
            Assert.That(_statsFour.DotMultiplier(), Is.EqualTo(1.078));
        });
    }
    
    [Test]
    public void TestCrit()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.CritChance(), Is.EqualTo(0.050));
            Assert.That(_statsOne.CritChance(), Is.EqualTo(0.074));
            Assert.That(_statsTwo.CritChance(), Is.EqualTo(0.104));
            Assert.That(_statsThree.CritChance(), Is.EqualTo(0.136));
            Assert.That(_statsFour.CritChance(), Is.EqualTo(0.171));
        });
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.CritDamage(), Is.EqualTo(1.400));
            Assert.That(_statsOne.CritDamage(), Is.EqualTo(1.424));
            Assert.That(_statsTwo.CritDamage(), Is.EqualTo(1.454));
            Assert.That(_statsThree.CritDamage(), Is.EqualTo(1.486));
            Assert.That(_statsFour.CritDamage(), Is.EqualTo(1.521));
        });
    }

    [Test]
    public void TestDirectHit()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.DirectHitChance(), Is.EqualTo(0.000));
            Assert.That(_statsOne.DirectHitChance(), Is.EqualTo(0.066));
            Assert.That(_statsTwo.DirectHitChance(), Is.EqualTo(0.150));
            Assert.That(_statsThree.DirectHitChance(), Is.EqualTo(0.238));
            Assert.That(_statsFour.DirectHitChance(), Is.EqualTo(0.333));
        });
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.AutoDirectHitMultiplier(), Is.EqualTo(1.000));
            Assert.That(_statsOne.AutoDirectHitMultiplier(), Is.EqualTo(1.016));
            Assert.That(_statsTwo.AutoDirectHitMultiplier(), Is.EqualTo(1.038));
            Assert.That(_statsThree.AutoDirectHitMultiplier(), Is.EqualTo(1.060));
            Assert.That(_statsFour.AutoDirectHitMultiplier(), Is.EqualTo(1.085));
        });
    }
    
    [Test]
    public void TestVitality()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_statsMin.MaxHp(), Is.EqualTo(04200));
            Assert.That(_statsOne.MaxHp(), Is.EqualTo(18527));
            Assert.That(_statsTwo.MaxHp(), Is.EqualTo(36497));
            Assert.That(_statsThree.MaxHp(), Is.EqualTo(49049));
            Assert.That(_statsFour.MaxHp(), Is.EqualTo(64550));
        });
    }
}