namespace AStore_Web.Models.VM.ProductImage
{
	public class ProImgCreateVM
	{
		ProImgCreateVM()
		{
			ProductImages = new List<Product_image>();
		}
		public Models.Product Product { get; set; }
		public List<Product_image> ProductImages { get; set; }



	}
}
