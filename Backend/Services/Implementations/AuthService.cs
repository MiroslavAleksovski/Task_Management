using DataAccess.Interfaces;
using Infrastructure;
using Infrastructure.ExceptionExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.AuthDTOModels;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<Guid> Register(RegisterRequestDTO request)
        {
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                throw new CustomInvalidOperationException("Passwords do not match.");
            }
            if (await _authRepository.UserExists(request.Email))
            {
                throw new CustomInvalidOperationException("User with this email already exists.");
            }

            var passwordHash = Cryptography.HashPassword(request.Password);
            Guid userId = Guid.NewGuid();
            var created = await _authRepository.CreateUser(
                userId,
                request.Name,
                request.Surname,
                request.Email,
                passwordHash);

            if (!created)
            {
                throw new CustomInvalidOperationException("Failed to register user.");
            }

            return userId;
        }
        public async Task<AuthResponseDTO> Login(LoginRequestDTO request)
        {
            var user = await _authRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                throw new CustomInvalidOperationException("Invalid email or password.");
            }
            if (!Cryptography.VerifyPassword(request.Password, user.Value.PasswordHash))
            {
                throw new CustomInvalidOperationException("Invalid email or password.");
            }

            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "30");
            var token = GenerateJwtToken(request.Email, expirationMinutes);

            return new AuthResponseDTO
            {
                Token = token,
                Email = request.Email,
                ExpiresIn = expirationMinutes * 60,
                Message = "Login successful."
            };
        }

        private string GenerateJwtToken(string email, int expirationMinutes)
        {
            var jwtSecret = _configuration["Jwt:Secret"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: [new Claim(ClaimTypes.Email, email)],
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
