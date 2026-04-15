using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;

        public JobService(IJobRepository jobRepository, ICompanyRepository companyRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _jobRepository.GetJobsWithCompanyAsync();
            return jobs.Select(MapToDto);
        }

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var job = await _jobRepository.GetJobWithDetailsAsync(id);
            return job is null ? null : MapToDto(job);
        }

        public async Task<IEnumerable<JobDto>> SearchAsync(string? title, string? location, string? jobType)
        {
            var jobs = await _jobRepository.SearchJobsAsync(title, location, jobType);
            return jobs.Select(MapToDto);
        }

        public async Task<JobDto> CreateAsync(CreateJobDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(dto.CompanyId)
                ?? throw new KeyNotFoundException($"Company with ID {dto.CompanyId} not found.");

            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Salary = dto.Salary,
                JobType = dto.JobType,
                ExperienceLevel = dto.ExperienceLevel,
                Deadline = dto.Deadline,
                CompanyId = dto.CompanyId
            };

            var created = await _jobRepository.AddAsync(job);
            created.Company = company;
            return MapToDto(created);
        }

        public async Task<JobDto?> UpdateAsync(int id, UpdateJobDto dto)
        {
            var job = await _jobRepository.GetJobWithDetailsAsync(id);
            if (job is null) return null;

            job.Title = dto.Title;
            job.Description = dto.Description;
            job.Location = dto.Location;
            job.Salary = dto.Salary;
            job.JobType = dto.JobType;
            job.ExperienceLevel = dto.ExperienceLevel;
            job.Deadline = dto.Deadline;
            job.UpdatedAt = DateTime.UtcNow;

            await _jobRepository.UpdateAsync(job);
            return MapToDto(job);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _jobRepository.ExistsAsync(id)) return false;
            await _jobRepository.DeleteAsync(id);
            return true;
        }

        private static JobDto MapToDto(Job j) => new()
        {
            Id = j.Id,
            Title = j.Title,
            Description = j.Description,
            Location = j.Location,
            Salary = j.Salary,
            JobType = j.JobType,
            ExperienceLevel = j.ExperienceLevel,
            Deadline = j.Deadline,
            CreatedAt = j.CreatedAt,
            CompanyId = j.CompanyId,
            CompanyName = j.Company?.Name ?? string.Empty,
            CompanyLocation = j.Company?.Location ?? string.Empty,
            TotalApplications = j.Applications?.Count(a => !a.IsDeleted) ?? 0
        };
    }
}