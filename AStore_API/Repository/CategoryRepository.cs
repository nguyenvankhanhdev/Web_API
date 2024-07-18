using AStore_API.Data;
using AStore_API.Models;
using AStore_API.Repository.IRepository;

namespace AStore_API.Repository
{
	public class CategoryRepository:Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public async Task<Category> UpdateAsync(Category entity)
		{
			entity.UpdateDate = DateTime.Now;
			_db.Categories.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}


	}
}
