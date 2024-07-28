using AStore_API.Models;
using AStore_API.Models.DTO;

namespace AStore_API.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO);
		Task<User> Register (RegisterRequestDTO registerRequestDTO);
	}
}
