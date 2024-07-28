using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Models.DTO;
using AStore_API.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AStore_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;
		private string secretkey;
		public UserRepository(ApplicationDbContext db, IConfiguration configuration)
		{
			_db = db;
			secretkey = configuration.GetValue<string>("ApiSettings:Secret");
		}

		public bool IsUniqueUser(string username)
		{
			var user = _db.Users.FirstOrDefault(u => u.Username.ToLower().Trim().Equals(username.ToLower().Trim()));
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{

			if (loginRequestDTO == null || string.IsNullOrEmpty(loginRequestDTO.Username) || string.IsNullOrEmpty(loginRequestDTO.Password))
			{
				throw new ArgumentNullException(nameof(loginRequestDTO), "LoginRequestDTO hoặc thuộc tính của nó không được phép null.");
			}

			var user = _db.Users.FirstOrDefault(u => u.Username.ToLower() == loginRequestDTO.Username.ToLower()
			&& u.Password.ToLower() == loginRequestDTO.Password.ToLower());

			if (user == null)
			{
				return new LoginResponseDTO()
				{
					User = null,
					Token = ""
				};
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretkey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
			new Claim(ClaimTypes.Name, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			LoginResponseDTO loginResponse = new LoginResponseDTO()
			{
				User = user,
				Token = tokenHandler.WriteToken(token)
			};

			return loginResponse;
		}


		public async Task<User> Register(RegisterRequestDTO registerRequestDTO)
		{
			User user = new User()
			{
				Name = registerRequestDTO.Name,
				Username = registerRequestDTO.Username,
				Role = registerRequestDTO.Role,
				Image = registerRequestDTO.Image,
				Password = registerRequestDTO.Password
			};
			_db.Users.Add(user);
			await _db.SaveChangesAsync();
			user.Password = "";
			return user;
		}
	}
}
