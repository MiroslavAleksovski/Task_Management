namespace DataAccess.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string email);
        Task<bool> CreateUser(Guid userId,
            string name,
            string surname,
            string email,
            string passwordHash);
        Task<(string Email, string PasswordHash)?> GetUserByEmail(string email);
    }
}