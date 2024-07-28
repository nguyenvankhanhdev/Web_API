using AStore_API.Models;
using AStore_API.Models.DTO;
using AStore_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AStore_API.Controllers
{
	[Route("api/UserAuth")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		protected APIResponse _response;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_response = new APIResponse();
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginResponse = await _userRepository.Login(model);
			if(loginResponse.User ==null || string.IsNullOrEmpty(loginResponse.Token))
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Username or password is incorrect");
				return BadRequest(_response);
			}
			_response.Result = loginResponse;
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
		{
			bool ifUserNameUnique = _userRepository.IsUniqueUser(model.Username);
			if (!ifUserNameUnique)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Username already exists");
				return BadRequest(_response);
			}
			var user = await _userRepository.Register(model);
			if (user == null)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Something went wrong");
				return BadRequest(_response);
			}
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);

		}


	}
}
