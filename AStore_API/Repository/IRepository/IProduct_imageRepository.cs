using AStore_API.Models;

namespace AStore_API.Repository.IRepository
{
	public interface IProduct_imageRepository:IRepository<Product_image>
	{
		Task<Product_image> UpdateAsync(Product_image product_image);
		//void Detach(Product_image product_image);
	}
}
