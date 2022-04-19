using LoginPageMVC.Models;
using Refit;

namespace LoginPageMVC.Services
{
    public interface IUserApiService
    {
        [Post("/Login")]
        Task<IEnumerable<LoginResponse>> LoginUserAsync(LoginModel user);
    }
}
