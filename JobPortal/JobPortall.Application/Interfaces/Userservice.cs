using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : MapToDto(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new InvalidOperationException($"A user with email '{dto.Email}' already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                ResumeUrl = dto.ResumeUrl,
                Bio = dto.Bio
            };

            var created = await _userRepository.AddAsync(user);
            return MapToDto(created);
        }

        public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) return null;

            user.Name = dto.Name;
            user.Phone = dto.Phone;
            user.ResumeUrl = dto.ResumeUrl;
            user.Bio = dto.Bio;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return MapToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _userRepository.ExistsAsync(id)) return false;
            await _userRepository.DeleteAsync(id);
            return true;
        }

        private static UserDto MapToDto(User u) => new()
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Phone = u.Phone,
            ResumeUrl = u.ResumeUrl,
            Bio = u.Bio,
            CreatedAt = u.CreatedAt
        };
    }
}