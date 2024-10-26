using Lumina.Excel.GeneratedSheets;
using XIVCalc.Interfaces;

namespace XIVCalc.Lumina;

public class LuminaJobModifiers(ClassJob source) : IJobModifiers
{
        public Job Job => (Job)source.RowId;
        public int ModifierHitPoints => source.ModifierHitPoints;
        public int ModifierManaPoints => source.ModifierManaPoints;
        public int ModifierStrength => source.ModifierStrength;
        public int ModifierVitality => source.ModifierVitality;
        public int ModifierDexterity => source.ModifierDexterity;
        public int ModifierIntelligence => source.ModifierIntelligence;
        public int ModifierMind => source.ModifierMind;
        public int ModifierPiety => source.ModifierMind;
        public bool IsTank => Job.IsTank();
        public StatType PrimaryStat => (StatType)source.PrimaryStat;
        public bool IsCaster => Job.IsCaster();

        public static implicit operator LuminaJobModifiers(ClassJob job) => new(job);
}