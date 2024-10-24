using Lumina.Excel.GeneratedSheets;
using XIVCalc.Calculations;

namespace XIVCalc.Tests;

public class SimpleTests
{
    private ClassJob WHM = new ClassJob()
    {
        ModifierStrength = 55,
        ModifierDexterity = 105,
        ModifierIntelligence = 105,
        ModifierMind = 115,
        ModifierVitality = 100,
        ModifierHitPoints = 105,
        PrimaryStat = (byte) StatType.Mind,
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
            Assert.That(StatEquations.MainStatMultiplier(531, 100, WHM), Is.EqualTo(	1.49));
            Assert.That(StatEquations.MainStatMultiplier(555, 100, WHM), Is.EqualTo(	1.61));
            Assert.That(StatEquations.MainStatMultiplier(629, 100, WHM), Is.EqualTo(	2.01));
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
            Assert.That(StatEquations.DeterminationMultiplier(440, 100), Is.EqualTo(1.0));
            Assert.That(StatEquations.DeterminationMultiplier(916, 100), Is.EqualTo(1.023));
            Assert.That(StatEquations.DeterminationMultiplier(1513, 100), Is.EqualTo(	1.054));
            Assert.That(StatEquations.DeterminationMultiplier(1930 , 100), Is.EqualTo(1.075));
            Assert.That(StatEquations.DeterminationMultiplier(2445, 100), Is.EqualTo(	1.1));
        });
    }
    
    [Test]
    public void TestTenacity()
    {
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.TenacityOffensiveModifier(420 , 100), Is.EqualTo(1.0));
            Assert.That(StatEquations.TenacityOffensiveModifier(685 , 100), Is.EqualTo(	1.01));
            Assert.That(StatEquations.TenacityOffensiveModifier(1165 , 100), Is.EqualTo(1.03));
            Assert.That(StatEquations.TenacityOffensiveModifier(1587, 100), Is.EqualTo(	1.047));
            Assert.That(StatEquations.TenacityOffensiveModifier(2033 , 100), Is.EqualTo(1.064));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(StatEquations.TenacityDefensiveModifier(420 , 100), Is.EqualTo(1.0));
            Assert.That(StatEquations.TenacityDefensiveModifier(685 , 100), Is.EqualTo(0.981));
            Assert.That(StatEquations.TenacityDefensiveModifier(1165 , 100), Is.EqualTo(0.947));
            Assert.That(StatEquations.TenacityDefensiveModifier(1587, 100), Is.EqualTo(0.917));
            Assert.That(StatEquations.TenacityDefensiveModifier(2033 , 100), Is.EqualTo(0.884));
        });
    }
}