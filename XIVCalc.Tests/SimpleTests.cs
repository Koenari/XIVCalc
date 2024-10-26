using Lumina.Excel.GeneratedSheets;
using XIVCalc.Calculations;

namespace XIVCalc.Tests;

public class SimpleTests
{
    private JobModMock WHM = new()
    {
        ModifierStrength = 55,
        ModifierDexterity = 105,
        ModifierIntelligence = 105,
        ModifierMind = 115,
        ModifierVitality = 100,
        ModifierHitPoints = 105,
        PrimaryStat = StatType.Mind,
    };
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMainStatMulti()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.MainStatMultiplier(440, 100, WHM), Is.EqualTo(1d));
            Assert.That(StatEquations.MainStatMultiplier(452, 100, WHM), Is.EqualTo(1.06));
            Assert.That(StatEquations.MainStatMultiplier(531, 100, WHM), Is.EqualTo(1.49));
            Assert.That(StatEquations.MainStatMultiplier(555, 100, WHM), Is.EqualTo(1.61));
            Assert.That(StatEquations.MainStatMultiplier(629, 100, WHM), Is.EqualTo(2.01));
        });
    }

    [Test]
    public void TestWeaponDamage()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.WeaponDamageMultiplier(000, 100, WHM), Is.EqualTo(0.5));
            Assert.That(StatEquations.WeaponDamageMultiplier(027, 100, WHM), Is.EqualTo(0.77));
            Assert.That(StatEquations.WeaponDamageMultiplier(101, 100, WHM), Is.EqualTo(1.51));
            Assert.That(StatEquations.WeaponDamageMultiplier(133, 100, WHM), Is.EqualTo(1.83));
            Assert.That(StatEquations.WeaponDamageMultiplier(200, 100, WHM), Is.EqualTo(2.5));
        });
    }
    
    [Test]
    public void TestDetermination()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.DeterminationMultiplier(0440, 100), Is.EqualTo(1.000));
            Assert.That(StatEquations.DeterminationMultiplier(0916, 100), Is.EqualTo(1.023));
            Assert.That(StatEquations.DeterminationMultiplier(1513, 100), Is.EqualTo(1.054));
            Assert.That(StatEquations.DeterminationMultiplier(1930, 100), Is.EqualTo(1.075));
            Assert.That(StatEquations.DeterminationMultiplier(2445, 100), Is.EqualTo(1.100));
        });
    }
    
    [Test]
    public void TestTenacity()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.TenacityOffensiveModifier(0420, 100), Is.EqualTo(1.000));
            Assert.That(StatEquations.TenacityOffensiveModifier(0685, 100), Is.EqualTo(1.010));
            Assert.That(StatEquations.TenacityOffensiveModifier(1165, 100), Is.EqualTo(1.030));
            Assert.That(StatEquations.TenacityOffensiveModifier(1587, 100), Is.EqualTo(1.047));
            Assert.That(StatEquations.TenacityOffensiveModifier(2033, 100), Is.EqualTo(1.064));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.TenacityDefensiveModifier(0420, 100), Is.EqualTo(1.000));
            Assert.That(StatEquations.TenacityDefensiveModifier(0685, 100), Is.EqualTo(0.981));
            Assert.That(StatEquations.TenacityDefensiveModifier(1165, 100), Is.EqualTo(0.947));
            Assert.That(StatEquations.TenacityDefensiveModifier(1587, 100), Is.EqualTo(0.917));
            Assert.That(StatEquations.TenacityDefensiveModifier(2033, 100), Is.EqualTo(0.884));
        });
    }
    
    [Test]
    public void TestPiety()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.MpPerTick(0421, 100), Is.EqualTo(198));
            Assert.That(StatEquations.MpPerTick(0756, 100), Is.EqualTo(217));
            Assert.That(StatEquations.MpPerTick(1182, 100), Is.EqualTo(240));
            Assert.That(StatEquations.MpPerTick(1627, 100), Is.EqualTo(264));
            Assert.That(StatEquations.MpPerTick(2108, 100), Is.EqualTo(290));
        });
    }
    
    [Test]
    public void TestSps()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.SpsToGcd(0420, 100), Is.EqualTo(2.50));
            Assert.That(StatEquations.SpsToGcd(0756, 100), Is.EqualTo(2.46));
            Assert.That(StatEquations.SpsToGcd(1182, 100), Is.EqualTo(2.41));
            Assert.That(StatEquations.SpsToGcd(1627, 100), Is.EqualTo(2.36));
            Assert.That(StatEquations.SpsToGcd(2108, 100), Is.EqualTo(2.30));
        });
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.DotMultiplier(0420, 100), Is.EqualTo(1.000));
            Assert.That(StatEquations.DotMultiplier(0756, 100), Is.EqualTo(1.015));
            Assert.That(StatEquations.DotMultiplier(1182, 100), Is.EqualTo(1.035));
            Assert.That(StatEquations.DotMultiplier(1627, 100), Is.EqualTo(1.056));
            Assert.That(StatEquations.DotMultiplier(2108, 100), Is.EqualTo(1.078));
        });
    }
    
    [Test]
    public void TestSks()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.SksToGcd(0420, 100), Is.EqualTo(2.50));
            Assert.That(StatEquations.SksToGcd(0756, 100), Is.EqualTo(2.46));
            Assert.That(StatEquations.SksToGcd(1182, 100), Is.EqualTo(2.41));
            Assert.That(StatEquations.SksToGcd(1627, 100), Is.EqualTo(2.36));
            Assert.That(StatEquations.SksToGcd(2108, 100), Is.EqualTo(2.30));
        });
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.DotMultiplier(0420, 100), Is.EqualTo(1.000));
            Assert.That(StatEquations.DotMultiplier(0756, 100), Is.EqualTo(1.015));
            Assert.That(StatEquations.DotMultiplier(1182, 100), Is.EqualTo(1.035));
            Assert.That(StatEquations.DotMultiplier(1627, 100), Is.EqualTo(1.056));
            Assert.That(StatEquations.DotMultiplier(2108, 100), Is.EqualTo(1.078));
        });
    }
    
    [Test]
    public void TestCrit()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.CritChance(0420, 100), Is.EqualTo(0.050));
            Assert.That(StatEquations.CritChance(0756, 100), Is.EqualTo(0.074));
            Assert.That(StatEquations.CritChance(1182, 100), Is.EqualTo(0.104));
            Assert.That(StatEquations.CritChance(1627, 100), Is.EqualTo(0.136));
            Assert.That(StatEquations.CritChance(2108, 100), Is.EqualTo(0.171));
        });
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.CritDamage(0420, 100), Is.EqualTo(1.400));
            Assert.That(StatEquations.CritDamage(0756, 100), Is.EqualTo(1.424));
            Assert.That(StatEquations.CritDamage(1182, 100), Is.EqualTo(1.454));
            Assert.That(StatEquations.CritDamage(1627, 100), Is.EqualTo(1.486));
            Assert.That(StatEquations.CritDamage(2108, 100), Is.EqualTo(1.521));
        });
    }
}