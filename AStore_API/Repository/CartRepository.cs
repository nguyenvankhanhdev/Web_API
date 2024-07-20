using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Repository.IRepository;

namespace AStore_API.Repository
{
	public class CartRepository:Repository<Cart>, ICartRepository
	{
		private readonly IRepository<Cart> _cartRepository;
		private readonly ApplicationDbContext _db;

		public CartRepository(ApplicationDbContext db) : base(db)
		{
			_db= db;
		}

		public async Task<Cart> UpdateAsync(Cart entity)
		{
			entity.UpdateDate = DateTime.Now;
			_db.Carts.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}

}
