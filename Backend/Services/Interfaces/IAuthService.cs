using Models.AuthDTOModels;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> Register(RegisterRequestDTO request);
        Task<AuthResponseDTO> Login(LoginRequestDTO request);
    }
}
