using JobPortal.Domain.Enums;
using JobPortal.JobPortall.Domain.Enums;

namespace JobPortal.Application.DTOs
{
    // ─── Company DTOs ────────────────────────────────────────────────────────────

    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
        public int TotalJobs { get; set; }
    }

    public class CreateCompanyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class UpdateCompanyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
    }

    // ─── Job DTOs ────────────────────────────────────────────────────────────────

    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string JobType { get; set; } = string.Empty;
        public string ExperienceLevel { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyLocation { get; set; } = string.Empty;
        public int TotalApplications { get; set; }
    }

    public class CreateJobDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string JobType { get; set; } = string.Empty;
        public string ExperienceLevel { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public int CompanyId { get; set; }
    }

    public class UpdateJobDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string JobType { get; set; } = string.Empty;
        public string ExperienceLevel { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
    }

    // ─── User DTOs ───────────────────────────────────────────────────────────────

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
    }

    public class UpdateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
    }

    // ─── Application DTOs ────────────────────────────────────────────────────────

    public class ApplicationDto
    {
        public int Id { get; set; }
        public DateTime AppliedAt { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? CoverLetter { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }

    public class CreateApplicationDto
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string? CoverLetter { get; set; }
    }

    public class UpdateApplicationStatusDto
    {
        public ApplicationStatus Status { get; set; }
    }
}