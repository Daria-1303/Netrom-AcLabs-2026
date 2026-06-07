using SmartShoppingAssistant.BusinessLogic.DTOs.Auth;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserGetDTO>> GetAllAsync();
        Task UpdateRoleAsync(int id, string role);
    }
}
