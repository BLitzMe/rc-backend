using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairConsole.Data.Models
{
    public enum DurationType
    {
        ErrorDiagnosis = 1,
        Repair = 2,
        PartSearch = 3,
    }

    public class WorkDuration
    {
        public int Id { get; set; }

        public DurationType Type { get; set; }

        [NotMapped]
        public TimeSpan? TimeTaken
        {
            get => !TimeTakenTicks.HasValue ? (TimeSpan?)null : TimeSpan.FromTicks(TimeTakenTicks.Value);
            set => TimeTakenTicks = value?.Ticks;
        }

        public long? TimeTakenTicks { get; set; }

        public UserDevice Device { get; set; }

        [NotMapped]
        public int UserDeviceId { get; set; }

        public bool ShouldSerializeTimeTakenTicks() => false;
    }
}