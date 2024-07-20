using AStore_Web.Models;

namespace AStore_Web.Service.IService
{
	public interface IProductService
	{
		Task<T> GetAllProductsAsync<T>();
		Task<T> GetProductByIdAsync<T>(int id);
		Task<T> CreateProductAsync<T>(Product product);
		Task<T> UpdateProductAsync<T>(int id, Product product);
		Task<T> DeleteProductAsync<T>(int id);
	}
}
