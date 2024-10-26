namespace XIVCalc.Interfaces;

public interface IJobModifiers
{
        public Job Job { get; }
        public int ModifierHitPoints { get; }
        public int ModifierManaPoints { get; }
        public int ModifierStrength { get; }
        public int ModifierVitality { get; }
        public int ModifierDexterity { get; }
        public int ModifierIntelligence { get; }
        public int ModifierMind { get; }
        public int ModifierPiety { get; }
        public bool IsTank { get; }
        public StatType PrimaryStat { get; }
        public bool IsCaster { get; }
}