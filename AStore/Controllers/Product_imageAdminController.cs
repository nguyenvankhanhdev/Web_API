using AStore_Web.Models;
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
		public Product_imageAdminController(IWebHostEnvironment webHostEnvironment,IProduct_imageService _ImageService,IProductService productService)
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

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Product_image product_image)
		{
			if (ModelState.IsValid)
			{
				var response = await _product_image.CreateAsync<APIResponse>(product_image);
				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					if (response?.ErrorMessages != null && response.ErrorMessages.Count > 0)
					{
						ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
					}
				}

			}
			return View(product_image);
		}
	

	}
}
