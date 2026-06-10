namespace Models.AuthDTOModels
{
    public class AuthResponseDTO
    {
        public required string Token { get; set; }
        public required string Email { get; set; }
        public int ExpiresIn { get; set; }
    }
}
