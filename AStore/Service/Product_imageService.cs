using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Service.IService;
using System.Configuration;

namespace AStore_Web.Service
{
	public class Product_imageService : BaseService, IProduct_imageService
	{
		private readonly IHttpClientFactory _client;
		private string ApiBaseUrl;

		public Product_imageService(IHttpClientFactory client, IConfiguration configuration) : base(client)
		{
			client = _client;
			ApiBaseUrl = configuration.GetValue<string>("ServiceUrls:AStoreAPI");
		}

		public Task<T> CreateAsync<T>(Product_image product_image)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType= SD.APIType.POST,
				Url = ApiBaseUrl + "/api/images",
				Data = product_image,

			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.DELETE,
				Url = ApiBaseUrl + "/api/images/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/images"
			});
		}

		public Task<T> GetByIdAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/images/" + id
			});
		}

		public Task<T> UpdateAsync<T>(int id, Product_image product_image)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.PUT,
				Url = ApiBaseUrl + "/api/images/" + id,
				Data = product_image
			});
		}
		public Task<T> GetImageByIdProduct<T>(int id)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/images/" + id + "/idproduct",
			});
		}
	}
}
