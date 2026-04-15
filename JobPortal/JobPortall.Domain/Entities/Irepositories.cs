using JobPortal.Domain.Entities;

namespace JobPortal.Domain.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> GetJobsWithCompanyAsync();
        Task<Job?> GetJobWithDetailsAsync(int id);
        Task<IEnumerable<Job>> SearchJobsAsync(string? title, string? location, string? jobType);
    }

    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetCompanyWithJobsAsync(int id);
        Task<IEnumerable<Company>> GetAllWithJobsAsync();
    }

    public interface IApplicationRepository : IGenericRepository<Application>
    {
        Task<IEnumerable<Application>> GetApplicationsByUserAsync(int userId);
        Task<IEnumerable<Application>> GetApplicationsByJobAsync(int jobId);
        Task<Application?> GetApplicationWithDetailsAsync(int id);
        Task<bool> HasUserAppliedAsync(int userId, int jobId);
    }

    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserWithApplicationsAsync(int id);
    }
}
