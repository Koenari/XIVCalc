using XIVCalc.Interfaces;

namespace XIVCalc;

public sealed class LuminaGameActionImpl : IGameAction
{
    public LuminaGameActionImpl(uint id, string name, int potency, IGameAction.ActionType type = IGameAction.ActionType.DAMAGE, int combopot = 0, int aoepot = 0,
        IEnumerable<IStatusEffect>? statusEffects = null)
    {
        statusEffects ??= Enumerable.Empty<IStatusEffect>();
        _id = id;
        _name = name;
        _type = type;
        _potency = potency;
        _aoepot = aoepot;
        _combopot = combopot;
        _statusEffects = new(statusEffects);
    }
    private readonly uint _id;
    private readonly string _name;
    private readonly IGameAction.ActionType _type;
    private readonly int _potency;
    private readonly int _combopot;
    private readonly int _aoepot;

    private readonly List<IStatusEffect> _statusEffects;
    public uint ID => _id;
    public string Name => _name;

    public IGameAction.ActionType Type => _type;

    public int Potency => _potency;

    public bool CanCombo => _combopot > 0;

    public bool IsAoE => _aoepot > 0;

    public int AoEPotency => _aoepot;

    public int ComboPotency => _combopot;

    public IEnumerable<IStatusEffect> Effects => _statusEffects;


}
