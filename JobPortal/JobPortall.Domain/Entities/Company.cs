namespace JobPortal.JobPortall.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Describtion { get; set; }


        public ICollection<Job> Jobs { get; set; } = new List<Job>();
       

    }
}
