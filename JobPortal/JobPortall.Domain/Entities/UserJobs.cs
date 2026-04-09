namespace JobPortal.JobPortall.Domain.Entities
{
    public class UserJobs
    {

        // Many-to-Many relationship between User and Job
        /*This entity represents the association between a User and a Job, indicating that a user has applied
         for or is interested in a job. */
        // bakhod el IDs w ba3ml Navigation property for both of them,
        // w ba3ml ICollection in both el User w el Job 3ashan a3ml el relationship de
        public int UserId { get; set; }
        public int JobId { get; set; } = 0;


        public User User { get; set; }
        public Job Job { get; set; }




    }
}
