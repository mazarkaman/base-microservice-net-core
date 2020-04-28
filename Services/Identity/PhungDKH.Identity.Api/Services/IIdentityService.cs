namespace PhungDKH.Identity.Api.Services
{
    using PhungDKH.Identity.Api.Models;
    using System.Threading.Tasks;

    public interface IIdentityService<T>
    {
        Task<object> LoginAsync(LoginDto model);

        Task<object> RegisterAsync(RegisterDto model);
    }
}
