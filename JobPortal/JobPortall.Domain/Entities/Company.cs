using JobPortal.JobPortall.Domain.Entities;

namespace JobPortal.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }

        // Navigation Properties
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
