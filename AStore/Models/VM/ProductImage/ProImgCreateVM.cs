using System.ComponentModel.DataAnnotations.Schema;

namespace AStore_Web.Models.VM.ProductImage
{
	public class ProImgCreateVM
	{
		public ProImgCreateVM()
		{
			Pro = new Product_image();
			ImageFile = new List<IFormFile>();
		}
		public Product_image Pro { get; set; }
		[NotMapped]
		public List<IFormFile> ImageFile { get; set; }
	}
}
