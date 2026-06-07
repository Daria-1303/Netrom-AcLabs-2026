using SmartShoppingAssistant.BusinessLogic.DTOs.Auth;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities.Enums;
using SmartShoppingAssistant.DataAccess.Repositories;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<List<UserGetDTO>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users.Select(u => new UserGetDTO
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();
        }

        public async Task UpdateRoleAsync(int id, string role)
        {
            if (!Enum.TryParse<UserRole>(role, ignoreCase: true, out var parsedRole))
                throw new ArgumentException($"Invalid role: {role}");

            var user = await userRepository.GetByIdAsync(id);
            user.Role = parsedRole;
            await userRepository.UpdateAsync(user);
        }
    }
}
