using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Repository.IRepository;

namespace AStore_API.Repository
{
	public class ProductRepository:Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public async Task<Product> UpdateAsync(Product entity)
		{
			entity.UpdateDate = DateTime.Now;
			_db.Products.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
