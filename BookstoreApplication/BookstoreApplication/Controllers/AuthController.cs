using BookstoreApplication.DTO;
using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize(Policy = "UnregisteredGet")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.RegisterAsync(data);
            return NoContent();
        }
        [Authorize(Policy = "UnregisteredGet")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _authService.Login(data);
            return Ok(token);
        }
        [Authorize(Policy ="RegisteredGet")]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok(await _authService.GetProfile(User));
        }

    }
}
