using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Application> Applications => Set<Application>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserSavedJob> UserSavedJobs => Set<UserSavedJob>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ─── Global Soft Delete Filter ────────────────────────────────────────
            modelBuilder.Entity<Job>().HasQueryFilter(j => !j.IsDeleted);
            modelBuilder.Entity<Company>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Application>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

            // ─── UserSavedJob: Composite Primary Key ──────────────────────────────
            modelBuilder.Entity<UserSavedJob>()
                .HasKey(us => new { us.UserId, us.JobId });

            modelBuilder.Entity<UserSavedJob>()
                .HasOne(us => us.User)
                .WithMany(u => u.SavedJobs)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSavedJob>()
                .HasOne(us => us.Job)
                .WithMany(j => j.SavedByUsers)
                .HasForeignKey(us => us.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            // ─── Application: Prevent duplicate applications ───────────────────────
            modelBuilder.Entity<Application>()
                .HasIndex(a => new { a.UserId, a.JobId })
                .IsUnique();

            // ─── Salary precision ─────────────────────────────────────────────────
            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18,2)");

            // ─── Seed Data ────────────────────────────────────────────────────────
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "TechCorp Egypt", Location = "Cairo", Description = "Leading software company in Egypt.", Website = "https://techcorp.eg", CreatedAt = new DateTime(2024, 1, 1) },
                new Company { Id = 2, Name = "Vodafone Egypt", Location = "Giza", Description = "Telecommunications leader.", Website = "https://vodafone.com.eg", CreatedAt = new DateTime(2024, 1, 1) }
            );

            modelBuilder.Entity<Job>().HasData(
                new Job { Id = 1, Title = "Junior .NET Developer", Description = "Work on backend APIs using ASP.NET Core.", Location = "Cairo", Salary = 12000, JobType = "Full-time", ExperienceLevel = "Junior", Deadline = new DateTime(2025, 12, 31), CompanyId = 1, CreatedAt = new DateTime(2024, 1, 1) },
                new Job { Id = 2, Title = "Senior Backend Engineer", Description = "Lead microservices architecture.", Location = "Remote", Salary = 35000, JobType = "Remote", ExperienceLevel = "Senior", Deadline = new DateTime(2025, 12, 31), CompanyId = 1, CreatedAt = new DateTime(2024, 1, 1) },
                new Job { Id = 3, Title = "Software Engineer", Description = "Build internal tools and systems.", Location = "Giza", Salary = 18000, JobType = "Full-time", ExperienceLevel = "Mid", Deadline = new DateTime(2025, 12, 31), CompanyId = 2, CreatedAt = new DateTime(2024, 1, 1) }
            );
        }
    }
}