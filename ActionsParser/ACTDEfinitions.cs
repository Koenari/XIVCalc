namespace ActionsParser
{
    internal class ACTDefinitions
    {
        public ACTDefinitions()
        {

        }
    }
    public class DamageItem
    {
        public int potency { get; set; }
    }

    public class ActionsItem
    {
        public KeyValuePair<string, string> info { get; set; }
        public List<DamageItem> damage { get; set; }
        public List<DamageItem> heal { get; set; }
    }

    public class Timeproc
    {
        public string type { get; set; }
        public string potency { get; set; }
        public string maxticks { get; set; }
    }

    public class StatuseffectsItem
    {
        public KeyValuePair<string, string> action { get; set; }
        public Timeproc timeproc { get; set; }
    }

    public class ActJobDefnition
    {
        public string job { get; set; }
        public List<ActionsItem> actions { get; set; }
        public List<StatuseffectsItem> statuseffects { get; set; }
    }

}
