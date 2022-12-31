using Lumina.Excel.GeneratedSheets;

namespace XIVCalc;

public static class ClassJobExtensions
{

    public static bool IsTank(this ClassJob job) => (Job)job.RowId switch
    {
        Job.DRK or Job.GNB or Job.PLD or Job.WAR or Job.GLA or Job.MRD => true,
        _ => false
    };
    public static bool IsPhysicalRanged(this ClassJob job) => (Job)job.RowId switch
    {
        Job.BRD or Job.DNC or Job.MCH or Job.ARC => true,
        _ => false
    };
    public static bool IsMelee(this ClassJob job) => (Job)job.RowId switch
    {
        Job.DRG or Job.MNK or Job.NIN or Job.RPR or Job.SAM or Job.LNC or Job.PGL or Job.ROG => true,
        _ => false
    };

    public static bool IsCaster(this ClassJob job) => (Job)job.RowId switch
    {
        Job.AST or Job.SCH or Job.SGE or Job.WHM or Job.CNJ => true,
        Job.BLM or Job.BLU or Job.RDM or Job.SMN or Job.THM or Job.ACN => true,
        _ => false
    };

}
