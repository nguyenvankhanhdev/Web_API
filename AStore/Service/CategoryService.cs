using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Service.IService;
using Newtonsoft.Json.Linq;

namespace AStore_Web.Service
{
	public class CategoryService : BaseService, ICategoryService
	{
		private readonly IHttpClientFactory _httpClient;
		private string ApiBaseUrl;
		public CategoryService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
		{
			_httpClient = httpClient;
			ApiBaseUrl = configuration.GetValue<string>("ServiceUrls:AStoreAPI");
		}

		public Task<T> CreateCategoryAsync<T>(Category categoryDTO)
		{
			return SendAsync<T>(new APIRequest()
			{
				apiType = SD.APIType.POST,
				Data = categoryDTO,
				Url = ApiBaseUrl + "/api/category"
			});
		}

		public Task<T> DeleteCategoryAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				apiType = SD.APIType.POST,
				Url = ApiBaseUrl + "/api/category/"+ id
			});
		}

		public Task<T> GetAllCategoriesAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/category"
			});
		}

		public Task<T> GetCategoryByIdAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/category/" + id
			});
		}

		public Task<T> UpdateCategoryAsync<T>(Category categoryDTO, int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				apiType = SD.APIType.PUT,
				Data = categoryDTO,
				Url = ApiBaseUrl + "/api/category/"+id
			});
		}
	}
}
