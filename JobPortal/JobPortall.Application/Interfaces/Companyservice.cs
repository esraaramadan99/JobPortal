using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var companies = await _companyRepository.GetAllWithJobsAsync();
            return companies.Select(MapToDto);
        }

        public async Task<CompanyDto?> GetByIdAsync(int id)
        {
            var company = await _companyRepository.GetCompanyWithJobsAsync(id);
            return company is null ? null : MapToDto(company);
        }

        public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
        {
            var company = new Company
            {
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                Website = dto.Website,
                LogoUrl = dto.LogoUrl
            };

            var created = await _companyRepository.AddAsync(company);
            return MapToDto(created);
        }

        public async Task<CompanyDto?> UpdateAsync(int id, UpdateCompanyDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company is null) return null;

            company.Name = dto.Name;
            company.Location = dto.Location;
            company.Description = dto.Description;
            company.Website = dto.Website;
            company.LogoUrl = dto.LogoUrl;
            company.UpdatedAt = DateTime.UtcNow;

            await _companyRepository.UpdateAsync(company);
            return MapToDto(company);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _companyRepository.ExistsAsync(id)) return false;
            await _companyRepository.DeleteAsync(id);
            return true;
        }

        private static CompanyDto MapToDto(Company c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location,
            Description = c.Description,
            Website = c.Website,
            LogoUrl = c.LogoUrl,
            TotalJobs = c.Jobs?.Count(j => !j.IsDeleted) ?? 0
        };
    }
}