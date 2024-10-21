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
    public void MainStatMulti()
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
    public void TestBasicStats()
    {
        Assert.Pass();
    }
}