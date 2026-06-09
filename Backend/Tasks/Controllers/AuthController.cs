using Infrastructure.ExceptionExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.AuthDTOModels;
using Services.Interfaces;

namespace Tasks.Controllers
{
    [AllowAnonymous]
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
        [HttpPost("register", Name = "Register")]
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
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _authService.Login(request);
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
    }
}
