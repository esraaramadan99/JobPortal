namespace JobPortal.JobPortall.Domain.Entities
{
    public class Application : BaseEntity
    {
        public DateTime AppliedAt { get; set;}    

        public string Status { get; set; }




        public int JobId { get; set; }
        public Job Job { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }








    }
}
