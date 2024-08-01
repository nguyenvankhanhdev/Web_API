using AStore_Web.Models;
using AStore_Web.Models.VM.Product;
using AStore_Web.Models.VM.ProductImage;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AStore_Web.Controllers
{
	public class Product_imageAdminController : Controller
	{
		private readonly IProduct_imageService _product_image;
		private readonly IProductService _product;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public Product_imageAdminController(IWebHostEnvironment webHostEnvironment, IProduct_imageService _ImageService, IProductService productService)
		{
			_webHostEnvironment = webHostEnvironment;
			_product_image = _ImageService;
			_product = productService;
		}

		public async Task<IActionResult> Index()
		{
			IEnumerable<Product_image> listProduct_image = new List<Product_image>();
			var response = await _product_image.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				listProduct_image = JsonConvert.DeserializeObject<IEnumerable<Product_image>>(Convert.ToString(response.Result));
			}
			ViewBag.Layout = "_AdminLayout";
			return View(listProduct_image);
		}

		public async Task<IActionResult> Create(int productId)
		{
			var model = new ProImgCreateVM { Pro = new Product_image { Product_id = productId } };
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProImgCreateVM product_image)
		{
			if (product_image.ImageFile != null && product_image.ImageFile.Count > 0)
			{
				string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "asset/frontend/img/");
				foreach (var imageFile in product_image.ImageFile)
				{
					if (imageFile.Length > 0)
					{
						string fileName = Guid.NewGuid().ToString() + "-" + imageFile.FileName;
						string filePath = Path.Combine(uploadDir, fileName);

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await imageFile.CopyToAsync(fileStream);
						}

						// Tạo đối tượng Product_image cho từng tệp hình ảnh và lưu vào database
						var newImage = new Product_image
						{
							Product_id = product_image.Pro.Product_id,
							Image = "/asset/frontend/img/" + fileName,
							CreateDate = DateTime.Now,
							UpdateDate = DateTime.Now
						};

						var response = await _product_image.CreateAsync<APIResponse>(newImage);
						if (response == null || !response.IsSuccess)
						{
							if (response?.ErrorMessages != null && response.ErrorMessages.Count > 0)
							{
								ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
							}
							return View(product_image);
						}
					}
				}

				return RedirectToAction("Index","ProductAdmin");
			}

			ModelState.AddModelError("ImageFiles", "Please upload at least one image.");
			return View(product_image);
		}


	}
}
