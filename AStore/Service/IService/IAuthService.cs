using AStore_Web.Models.DTO;

namespace AStore_Web.Service.IService
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
		Task<T> RegisterAsync<T>(RegisterRequestDTO registerRequestDTO);
	}
}
