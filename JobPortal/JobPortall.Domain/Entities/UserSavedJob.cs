namespace JobPortal.Domain.Entities
{
    public class UserSavedJob
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Job Job { get; set; } = null!;
    }
}
