using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Models.DTO;
using AStore_Web.Service.IService;

namespace AStore_Web.Service
{
	public class AuthService :BaseService, IAuthService
	{
		private readonly IHttpClientFactory _httpClient;
		private string _baseUrl;
		public AuthService(IHttpClientFactory httpClient, IConfiguration configuration):base(httpClient) {

			_httpClient = httpClient;
			_baseUrl = configuration.GetValue<string>("ServiceUrls:AStoreAPI");

		}
		public  Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType=SD.APIType.POST,
				Data=loginRequestDTO,
				Url=_baseUrl+ "/api/UserAuth/login"
			});
		}

		public Task<T> RegisterAsync<T>(RegisterRequestDTO registerRequestDTO)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.POST,
				Data = registerRequestDTO,
				Url = _baseUrl + "/api/UserAuth/register"
			});
		}
	}
}
