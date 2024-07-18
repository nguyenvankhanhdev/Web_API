using AStore_API.Models;

namespace AStore_API.Repository.IRepository
{
	public interface ICategoryRepository:IRepository<Category>
	{
		Task<Category> UpdateAsync(Category category);
	}
}
