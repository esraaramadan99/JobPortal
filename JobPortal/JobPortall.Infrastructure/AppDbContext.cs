namespace JobPortal.JobPortall.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } 
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserJobs> UserJobs { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the many-to-many relationship between User and Job through UserJobs
            modelBuilder.Entity<UserJobs>()
                .HasKey(uj => new { uj.UserId, uj.JobId }); // Composite primary key
            modelBuilder.Entity<UserJobs>()
                .HasOne(uj => uj.User)
                .WithMany(u => u.UserJobs)
                .HasForeignKey(uj => uj.UserId);
            modelBuilder.Entity<UserJobs>()
                .HasOne(uj => uj.Job)
                .WithMany(j => j.UserJobs)
                .HasForeignKey(uj => uj.JobId);
        }
    }
}
