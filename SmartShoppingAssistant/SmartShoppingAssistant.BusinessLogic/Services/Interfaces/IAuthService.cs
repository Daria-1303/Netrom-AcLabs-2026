using SmartShoppingAssistant.BusinessLogic.DTOs.Auth;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO dto);
        Task<TokenDTO> LoginAsync(LoginDTO dto);
    }
}
