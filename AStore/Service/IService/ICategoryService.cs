using AStore_Web.Models;

namespace AStore_Web.Service.IService
{
	public interface ICategoryService
	{
		Task<T> CreateCategoryAsync<T>(Category categoryDTO);
		Task<T> UpdateCategoryAsync<T>(Category categoryDTO, int id);
		Task<T> GetCategoryByIdAsync<T>(int id);
		Task<T> GetAllCategoriesAsync<T>();
		Task<T> DeleteCategoryAsync<T>(int id);

	}
}
