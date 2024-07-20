using AStore_API.Models;

namespace AStore_API.Repository.IRepository
{
	public interface ICartRepository:IRepository<Cart>
	{
		Task<Cart> UpdateAsync(Cart cart);
	}
}
