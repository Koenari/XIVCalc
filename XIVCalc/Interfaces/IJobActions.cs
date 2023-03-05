namespace XIVCalc.Interfaces;

public interface IJobActions
{
    public Job Job { get; }
    IEnumerable<IGameAction> Actions { get; }
}
