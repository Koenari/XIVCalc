using XIVCalc.Interfaces;

namespace XIVCalc.DPS;

internal static class JobDefinitions
{
    internal static IJobActions AST { get; set; } = new DummyJobImpl(Job.AST);
    internal static IJobActions BLM { get; set; } = new DummyJobImpl(Job.BLM);
    internal static IJobActions BRD { get; set; } = new DummyJobImpl(Job.BRD);
    internal static IJobActions DNC { get; set; } = new DummyJobImpl(Job.DNC);
    internal static IJobActions DRG { get; set; } = new DummyJobImpl(Job.DRG);
    internal static IJobActions DRK { get; set; } = new DummyJobImpl(Job.DRK);
    internal static IJobActions GNB { get; set; } = new DummyJobImpl(Job.GNB);
    internal static IJobActions MCH { get; set; } = new DummyJobImpl(Job.MCH);
    internal static IJobActions MNK { get; set; } = new DummyJobImpl(Job.MNK);
    internal static IJobActions NIN { get; set; } = new DummyJobImpl(Job.NIN);
    internal static IJobActions PLD { get; set; } = new DummyJobImpl(Job.PLD);
    internal static IJobActions RDM { get; set; } = new DummyJobImpl(Job.RDM);
    internal static IJobActions RPR { get; set; } = new DummyJobImpl(Job.RPR);
    internal static IJobActions SAM { get; set; } = new DummyJobImpl(Job.SAM);
    internal static IJobActions SCH { get; set; } = new DummyJobImpl(Job.SCH);
    internal static IJobActions SGE { get; set; } = new DummyJobImpl(Job.SGE);
    internal static IJobActions SMN { get; set; } = new DummyJobImpl(Job.SMN);
    internal static IJobActions WAR { get; set; } = new DummyJobImpl(Job.WAR);
    internal static IJobActions WHM { get; set; } = new DummyJobImpl(Job.WHM);

}
public class DummyJobImpl : IJobActions
{
    public DummyJobImpl(Job j) => _job = j;
    private readonly Job _job;
    public Job Job => _job;

    public IEnumerable<IGameAction> Actions => Enumerable.Empty<IGameAction>();
}

public class SimpleJobImpl : IJobActions
{
    public SimpleJobImpl(Job j, IEnumerable<LuminaGameActionImpl> gameActions)
    {
        _job = j;
        _actions = new(gameActions);
    }
    private readonly Job _job;
    private readonly List<LuminaGameActionImpl> _actions;
    public Job Job => throw new NotImplementedException();

    public IEnumerable<IGameAction> Actions => throw new NotImplementedException();
}