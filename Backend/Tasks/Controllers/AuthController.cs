using Infrastructure.ExceptionExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.AuthDTOModels;
using Services.Interfaces;
using System.Security.Claims;

namespace Tasks.Controllers
{
    [Tags("Auth")]
    public class AuthController(IAuthService authService) : BaseController
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration request with email and password.</param>
        /// <response code="200">Returns JWT token for newly registered user.</response>
        /// <response code="400">Bad request when user already exists or validation fails.</response>
        /// <response code="500">Server error while processing the request.</response>
        [AllowAnonymous]
        [HttpPost(Name = "Register")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] RegisterRequestDTO request)
        {
            try
            {
                var userId = await _authService.Register(request);
                return Ok(userId);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Authenticates a user and returns JWT token.
        /// </summary>
        /// <param name="request">Login request with email and password.</param>
        /// <response code="200">Returns JWT token on successful authentication.</response>
        /// <response code="401">Unauthorized when credentials are invalid.</response>
        /// <response code="400">Bad request when validation fails.</response>
        /// <response code="500">Server error while processing the request.</response>
        [AllowAnonymous]
        [HttpPost(Name = "Login")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _authService.Login(request);

                Response.Cookies.Append("token", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Unspecified,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn)
                });

                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Validates the current JWT token and returns the authenticated user's email.
        /// </summary>
        /// <response code="200">Token is valid. Returns the user's email.</response>
        /// <response code="401">Token is missing, expired, or invalid.</response>
        [Authorize]
        [HttpGet(Name = "ValidateToken")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ValidateToken()
        {
            return Ok(true);
        }
    }
}
