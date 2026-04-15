using JobPortal.JobPortall.Domain.Entities;

namespace JobPortal.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string JobType { get; set; } = string.Empty; // Full-time, Part-time, Remote
        public string ExperienceLevel { get; set; } = string.Empty; // Junior, Mid, Senior
        public DateTime Deadline { get; set; }

        // Foreign Key
        public int CompanyId { get; set; }

        // Navigation Properties
        public Company Company { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<UserSavedJob> SavedByUsers { get; set; } = new List<UserSavedJob>();
    }
}