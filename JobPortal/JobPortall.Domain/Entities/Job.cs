namespace JobPortal.JobPortall.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Location { get; set; }
        public decimal Salary { get; set; }


        // Foreign Key -> Company
        public int CompanyId { get; set; }


        // Navigation Property -> each job belongs to one company

        public Company Company { get; set; }


        // ba3ml icollection of applications  3ashan a3ml el relationship de ,, el job w e l application one to many
        public ICollection<Application> Applications { get; set; } = new List<Application>();

        public ICollection<UserJobs> UserJobs { get; set; } = new List<UserJobs>();

    }
}
