namespace SmartShoppingAssistant.BusinessLogic.DTOs.Auth
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
