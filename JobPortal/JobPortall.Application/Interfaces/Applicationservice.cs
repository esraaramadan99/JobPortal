using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IJobRepository jobRepository,
            IUserRepository userRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ApplicationDto>> GetAllAsync()
        {
            var apps = await _applicationRepository.GetAllAsync();
            return apps.Select(MapToDto);
        }

        public async Task<ApplicationDto?> GetByIdAsync(int id)
        {
            var app = await _applicationRepository.GetApplicationWithDetailsAsync(id);
            return app is null ? null : MapToDto(app);
        }

        public async Task<IEnumerable<ApplicationDto>> GetByUserAsync(int userId)
        {
            var apps = await _applicationRepository.GetApplicationsByUserAsync(userId);
            return apps.Select(MapToDto);
        }

        public async Task<IEnumerable<ApplicationDto>> GetByJobAsync(int jobId)
        {
            var apps = await _applicationRepository.GetApplicationsByJobAsync(jobId);
            return apps.Select(MapToDto);
        }

        public async Task<ApplicationDto> CreateAsync(CreateApplicationDto dto)
        {
            var jobExists = await _jobRepository.ExistsAsync(dto.JobId);
            if (!jobExists) throw new KeyNotFoundException($"Job with ID {dto.JobId} not found.");

            var userExists = await _userRepository.ExistsAsync(dto.UserId);
            if (!userExists) throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            var alreadyApplied = await _applicationRepository.HasUserAppliedAsync(dto.UserId, dto.JobId);
            if (alreadyApplied) throw new InvalidOperationException("User has already applied to this job.");

            var application = new Application
            {
                JobId = dto.JobId,
                UserId = dto.UserId,
                CoverLetter = dto.CoverLetter
            };

            var created = await _applicationRepository.AddAsync(application);
            var detailed = await _applicationRepository.GetApplicationWithDetailsAsync(created.Id);
            return MapToDto(detailed!);
        }

        public async Task<ApplicationDto?> UpdateStatusAsync(int id, UpdateApplicationStatusDto dto)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application is null) return null;

            application.Status = dto.Status;
            application.UpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);

            var detailed = await _applicationRepository.GetApplicationWithDetailsAsync(id);
            return MapToDto(detailed!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _applicationRepository.ExistsAsync(id)) return false;
            await _applicationRepository.DeleteAsync(id);
            return true;
        }

        private static ApplicationDto MapToDto(Application a) => new()
        {
            Id = a.Id,
            AppliedAt = a.AppliedAt,
            Status = a.Status,
            CoverLetter = a.CoverLetter,
            JobId = a.JobId,
            JobTitle = a.Job?.Title ?? string.Empty,
            CompanyName = a.Job?.Company?.Name ?? string.Empty,
            UserId = a.UserId,
            UserName = a.User?.Name ?? string.Empty,
            UserEmail = a.User?.Email ?? string.Empty
        };
    }
}