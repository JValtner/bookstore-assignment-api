using BookstoreApplication.DTO;

namespace BookstoreApplication.Services.IService
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> Login(LoginDto data);
        Task<ProfileDto> GetProfile(System.Security.Claims.ClaimsPrincipal userPrincipal);
    }
}
