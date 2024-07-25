using AStore_API.Models;

namespace AStore_API.Repository.IRepository
{
	public interface IProductRepository:IRepository<Product>
	{
		Task<Product> UpdateAsync(Product product);
		void Detach(Product product);
	}
}
