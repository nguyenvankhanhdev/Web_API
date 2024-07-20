
using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Service.IService;

namespace AStore_Web.Service
{
	public class ProductService : BaseService, IProductService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string ApiBaseUrl;

		public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			ApiBaseUrl =  configuration.GetValue<string>("ServiceUrls:AStoreAPI");

		}
		public Task<T> GetAllProductsAsync<T>()
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.GET,
				Url = ApiBaseUrl + "/api/product"

			});
		}

		public Task<T> CreateProductAsync<T>(Product product)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.POST,
				Url = ApiBaseUrl + "/api/product",
				Data = product
			});
		}

		public Task<T> DeleteProductAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest
			{
				apiType = SD.APIType.DELETE,
				Url = ApiBaseUrl + "/api/product/" + id,
			});
		}

		
		public Task<T> GetProductByIdAsync<T>(int id)
		{
			throw new NotImplementedException();
		}

		public Task<T> UpdateProductAsync<T>(int id, Product product)
		{
			throw new NotImplementedException();
		}
	}
}
