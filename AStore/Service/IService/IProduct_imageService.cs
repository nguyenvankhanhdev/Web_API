using AStore_Web.Models;

namespace AStore_Web.Service.IService
{
	public interface IProduct_imageService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetByIdAsync<T>(int id);
		Task<T> CreateAsync<T>(Product_image product_image);
		Task<T> UpdateAsync<T>(int id, Product_image product_image);
		Task<T> DeleteAsync<T>(int id);
		Task<T> GetImageByIdProduct<T>(int id);
	}
}
