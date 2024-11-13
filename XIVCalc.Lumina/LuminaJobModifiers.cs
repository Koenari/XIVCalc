using Lumina.Excel.Sheets;
using XIVCalc.Interfaces;

namespace XIVCalc.Lumina;

public class LuminaJobModifiers(ClassJob source) : IJobModifiers
{
        public Job Job => (Job)source.RowId;
        public int HitPoints => source.ModifierHitPoints;
        public int ManaPoints => source.ModifierManaPoints;
        public int Strength => source.ModifierStrength;
        public int Vitality => source.ModifierVitality;
        public int Dexterity => source.ModifierDexterity;
        public int Intelligence => source.ModifierIntelligence;
        public int Mind => source.ModifierMind;
        public int Piety => source.ModifierMind;
        public bool IsTank => Job.IsTank();
        public StatType PrimaryStat => (StatType)source.PrimaryStat;
        public bool IsCaster => Job.IsCaster();

        public static implicit operator LuminaJobModifiers(ClassJob job) => new(job);
}