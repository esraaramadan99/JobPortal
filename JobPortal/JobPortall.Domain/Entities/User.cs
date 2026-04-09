namespace JobPortal.JobPortall.Domain.Entities
{
    public class User : BaseEntity
    {


        public string Name { get; set; }
        public string Email { get; set; } 

        public ICollection <Application> Applications { get; set; } = new List<Application>();






        public ICollection<UserJobs> UserJobs { get; set; } = new List<UserJobs>();
    }
}
