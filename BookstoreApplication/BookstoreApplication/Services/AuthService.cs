using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegistrationDto data)
        {
            var user = _mapper.Map<ApplicationUser>(data);
            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                // više grešaka se može desiti pri registraciji
                // preuzima se tekstualni opis svake i spaja u jedan string
                string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(null, errorMessage);
            }
        }
        public async Task<string> Login(LoginDto data)
        {
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                string msg = $"User with username '{data.Username}' not found.";
                throw new BadRequestException(null, msg);
            }

            var passwordMatch = await _userManager.CheckPasswordAsync(user, data.Password);
            if (!passwordMatch)
            {
                string msg = "Password is incorrect.";
                throw new BadRequestException(null, msg);
            }
            // Novina
            var token = await GenerateJwt(user);
            return token;
        }
        public async Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal)
        {
            // Preuzimanje korisničkog imena iz tokena
            var username = userPrincipal.FindFirstValue("username");

            if (username == null)
            {
                string msg = "Username claim is missing in the token.";
                throw new BadRequestException(null, msg);
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                string msg = $"User with username '{username}' not found.";
                throw new NotFoundException(null,msg);
            }

            return _mapper.Map<ProfileDto>(user);
        }


        private async Task<string> GenerateJwt(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id),  // 'sub' atribut
              new Claim("username", user.UserName),  // 'username' atribut
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // jedinstveni identifikator tokena
            };
            // Roles
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            // Konfiguracija za generisanje tokena
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddDays(1), // važi 1 dan ('exp' atribut), nakon čega mora nova prijava
              signingCredentials: creds
            );

            // Generisanje tokena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
