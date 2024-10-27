using XIVCalc.Interfaces;

namespace XIVCalc.Jobs;

/// <inheritdoc/>
internal class FixedJobModifiers : IJobModifiers
{
    /// <inheritdoc/>
    public Job Job { get; init; }
    
    /// <inheritdoc/>
    public int HitPoints { get; init;}
    
    /// <inheritdoc/>
    public int ManaPoints  => 100;
    
    /// <inheritdoc/>
    public int Strength { get; init;}
    
    /// <inheritdoc/>
    public int Vitality { get; init; }
    
    /// <inheritdoc/>
    public int Dexterity { get; init; }
    
    /// <inheritdoc/>
    public int Intelligence { get; init; }
    
    /// <inheritdoc/>
    public int Mind { get; init; }

    /// <inheritdoc/>
    public int Piety => 100;
    
    /// <inheritdoc/>
    public StatType PrimaryStat { get; init; }
}