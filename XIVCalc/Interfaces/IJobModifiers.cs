namespace XIVCalc.Interfaces;

/// <summary>
/// Describes the properties specific to different
/// Modifiers are generally meant to be used as in Value * Mod / 100 
/// </summary>
public interface IJobModifiers
{
        /// <summary>
        /// Job described
        /// </summary>
        public Job Job { get; }
        
        /// <summary>
        /// Hit point modifier
        /// </summary>
        public int HitPoints { get; }
        
        /// <summary>
        /// Mana point modifier
        /// </summary>
        public int ManaPoints { get; }
        
        /// <summary>
        /// Strength multiplier
        /// </summary>
        public int Strength { get; }
        
        /// <summary>
        /// Vitality modifier
        /// </summary>
        public int Vitality { get; }
        
        /// <summary>
        /// Dexterity modifier
        /// </summary>
        public int Dexterity { get; }
        
        /// <summary>
        /// Intelligence modifier
        /// </summary>
        public int Intelligence { get; }
        
        /// <summary>
        /// Mind modifier
        /// </summary>
        public int Mind { get; }
        
        /// <summary>
        /// Piety modifier
        /// </summary>
        public int Piety { get; }
        /// <summary>
        /// Type of stat that is primarily used by this job
        /// </summary>
        public StatType PrimaryStat { get; }
}