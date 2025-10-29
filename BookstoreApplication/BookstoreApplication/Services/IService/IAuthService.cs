using BookstoreApplication.DTO;

namespace BookstoreApplication.Services.IService
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task Login(LoginDto data);
    }
}