using JobPortal.Application.DTOs;

namespace JobPortal.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto?> GetByIdAsync(int id);
        Task<CompanyDto> CreateAsync(CreateCompanyDto dto);
        Task<CompanyDto?> UpdateAsync(int id, UpdateCompanyDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public interface IJobService
    {
        Task<IEnumerable<JobDto>> GetAllAsync();
        Task<JobDto?> GetByIdAsync(int id);
        Task<IEnumerable<JobDto>> SearchAsync(string? title, string? location, string? jobType);
        Task<JobDto> CreateAsync(CreateJobDto dto);
        Task<JobDto?> UpdateAsync(int id, UpdateJobDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> GetAllAsync();
        Task<ApplicationDto?> GetByIdAsync(int id);
        Task<IEnumerable<ApplicationDto>> GetByUserAsync(int userId);
        Task<IEnumerable<ApplicationDto>> GetByJobAsync(int jobId);
        Task<ApplicationDto> CreateAsync(CreateApplicationDto dto);
        Task<ApplicationDto?> UpdateStatusAsync(int id, UpdateApplicationStatusDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
