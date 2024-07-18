using Microsoft.EntityFrameworkCore;
using AStore_API.Models;
namespace AStore_API.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Product_image> Product_Images { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Order_detail> Order_details { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<User_address> User_Addresses { get; set; }
		public DbSet<Cart> Carts { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category
				{
					Id = 1,
					Name ="IPhone",
					Slug = "iphone",
					Status = 1,
					CreateDate= System.DateTime.Now,
				},
				new Category
				{
					Id = 2,
					Name = "iMac",
					Slug = "imac",
					Status = 1,
					CreateDate = System.DateTime.Now,
				},
				new Category
				{
					Id = 3,
					Name = "Macbook",
					Slug = "macbook",
					Status = 1,
					CreateDate = System.DateTime.Now,
				},
				new Category
				{
					Id=4,
					Name = "Apple Watch",
					Slug = "apple-watch",
					Status = 1,
					CreateDate = System.DateTime.Now,
				},
				new Category
				{
					Id = 5,
					Name = "AirPods",
					Slug = "airpods",
					Status = 1,
					CreateDate = System.DateTime.Now,
				}
				);
		}


	}
}
