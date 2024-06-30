using Lumina.Excel.GeneratedSheets;

namespace XIVCalc;

public static class ClassJobExtensions
{
    public static Job AsJob(this ClassJob job)
    {
        return (Job)job.RowId;
    }

    public static bool IsTank(this ClassJob job)
    {
        return job.AsJob().IsTank();
    }

    public static bool IsTank(this Job job)
    {
        return job is Job.DRK or Job.GNB or Job.PLD or Job.WAR or Job.GLA or Job.MRD;
    }

    public static bool IsPhysicalRanged(this ClassJob job)
    {
        return job.AsJob().IsPhysicalRanged();
    }

    public static bool IsPhysicalRanged(this Job job)
    {
        return job is Job.BRD or Job.DNC or Job.MCH or Job.ARC;
    }

    public static bool IsMelee(this ClassJob job)
    {
        return job.AsJob().IsMelee();
    }

    public static bool IsMelee(this Job job)
    {
        return job is Job.DRG or Job.MNK or Job.NIN or Job.RPR or Job.SAM or Job.LNC or Job.PGL or Job.ROG or Job.VPR;
    }

    public static bool IsCaster(this ClassJob job)
    {
        return job.AsJob().IsCaster();
    }

    public static bool IsCaster(this Job job)
    {
        return job is Job.AST or Job.SCH or Job.SGE or Job.WHM or Job.CNJ or Job.BLM or Job.BLU or Job.RDM or Job.SMN
            or Job.THM or Job.ACN or Job.PCT;
    }
}