namespace SmartShoppingAssistant.BusinessLogic.DTOs.Auth
{
    public class TokenDTO
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
