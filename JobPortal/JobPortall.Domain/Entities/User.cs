using JobPortal.JobPortall.Domain.Entities;

namespace JobPortal.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }

        // Navigation Properties
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<UserSavedJob> SavedJobs { get; set; } = new List<UserSavedJob>();
    }
}
