namespace XIVCalc.Interfaces;


public interface IGameAction
{
    uint ID { get; }
    string Name { get; }
    ActionType Type { get; }
    int Potency { get; }
    bool CanCombo { get; }
    bool IsAoE { get; }
    int AoEPotency { get; }
    int ComboPotency { get; }

    IEnumerable<IStatusEffect> Effects { get; }

    public enum ActionType
    {
        None = 0,
        DAMAGE,
        HEAL,
    }
}
