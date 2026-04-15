using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;
using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Infrastructure.Repositories
{
    // ─── Job Repository ───────────────────────────────────────────────────────────

    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Job>> GetJobsWithCompanyAsync()
            => await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Applications)
                .ToListAsync();

        public async Task<Job?> GetJobWithDetailsAsync(int id)
            => await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Applications)
                    .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(j => j.Id == id);

        public async Task<IEnumerable<Job>> SearchJobsAsync(string? title, string? location, string? jobType)
        {
            var query = _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Applications)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(j => j.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(j => j.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(jobType))
                query = query.Where(j => j.JobType == jobType);

            return await query.ToListAsync();
        }
    }

    // ─── Company Repository ───────────────────────────────────────────────────────

    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context) { }

        public async Task<Company?> GetCompanyWithJobsAsync(int id)
            => await _context.Companies
                .Include(c => c.Jobs)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Company>> GetAllWithJobsAsync()
            => await _context.Companies
                .Include(c => c.Jobs)
                .ToListAsync();
    }

    // ─── Application Repository ───────────────────────────────────────────────────

    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Application>> GetApplicationsByUserAsync(int userId)
            => await _context.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Company)
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();

        public async Task<IEnumerable<Application>> GetApplicationsByJobAsync(int jobId)
            => await _context.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Company)
                .Include(a => a.User)
                .Where(a => a.JobId == jobId)
                .ToListAsync();

        public async Task<Application?> GetApplicationWithDetailsAsync(int id)
            => await _context.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Company)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<bool> HasUserAppliedAsync(int userId, int jobId)
            => await _context.Applications
                .AnyAsync(a => a.UserId == userId && a.JobId == jobId);
    }

    // ─── User Repository ──────────────────────────────────────────────────────────

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetUserWithApplicationsAsync(int id)
            => await _context.Users
                .Include(u => u.Applications)
                    .ThenInclude(a => a.Job)
                .FirstOrDefaultAsync(u => u.Id == id);
    }
}
