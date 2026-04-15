using JobPortal.Domain.Enums;
using JobPortal.JobPortall.Domain.Entities;

namespace JobPortal.Domain.Entities
{
    public class Application : BaseEntity
    {
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
        public string? CoverLetter { get; set; }

        // Foreign Keys
        public int JobId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public Job Job { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
