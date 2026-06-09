namespace Models.AuthDTOModels
{
    /// <summary>
    /// Authentication response model with JWT token.
    /// </summary>
    public class AuthResponseDTO
    {
        /// <summary>
        /// JWT access token.
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Token expiration time in seconds.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Success message.
        /// </summary>
        public string Message { get; set; } = "Authentication successful.";
    }
}
