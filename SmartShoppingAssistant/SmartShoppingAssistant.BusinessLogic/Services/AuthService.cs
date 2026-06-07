using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartShoppingAssistant.BusinessLogic.DTOs.Auth;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Entities.Enums;
using SmartShoppingAssistant.DataAccess.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public async Task RegisterAsync(RegisterDTO dto)
        {
            var existing = await userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new InvalidOperationException("Email already in use.");

            var user = new User { Email = dto.Email, Role = UserRole.User };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await userRepository.AddAsync(user);
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO dto)
        {
            const string invalid = "Invalid credentials.";

            var user = await userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException(invalid);

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException(invalid);

            return GenerateToken(user);
        }

        private TokenDTO GenerateToken(User user)
        {
            var secret = configuration["Jwt:Secret"]
                ?? throw new InvalidOperationException("JWT secret is not configured.");
            var issuer   = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var expiry   = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? "60");

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddMinutes(expiry);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer:             issuer,
                audience:           audience,
                claims:             claims,
                expires:            expiresAt,
                signingCredentials: creds);

            return new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt   = expiresAt,
                Email       = user.Email,
                Role        = user.Role.ToString()
            };
        }
    }
}
