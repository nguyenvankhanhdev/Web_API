using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Repository.IRepository;

namespace AStore_API.Repository
{
	public class Product_imageRepository:Repository<Product_image>,IProduct_imageRepository
	{
		private readonly ApplicationDbContext _db;
		public Product_imageRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public async Task<Product_image> UpdateAsync(Product_image entity)
		{
			entity.UpdateDate = DateTime.Now;
			_db.Product_Images.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
