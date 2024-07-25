using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
			var existingEntity = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == entity.Id);
			if (existingEntity != null)
			{
				_db.Entry(existingEntity).State = EntityState.Detached;
			}

			entity.UpdateDate = DateTime.Now;
			_db.Products.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
		public void Detach(Product entity)
		{
			var entry = _db.Entry(entity);
			if (entry != null)
			{
				entry.State = EntityState.Detached;
			}
		}
	}
}
