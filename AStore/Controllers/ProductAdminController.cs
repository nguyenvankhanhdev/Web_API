using AStore_Web.Models;
using AStore_Web.Models.VM.Product;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
namespace AStore_Web.Controllers
{
	public class ProductAdminController : Controller
	{
		private readonly IProductService _product;
		private readonly ICategoryService _category;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IProduct_imageService _product_ImageService;
		public ProductAdminController(IProductService product, ICategoryService category, IWebHostEnvironment webHost, IProduct_imageService product_ImageService)
		{
			_category = category;
			_product = product;
			_webHostEnvironment = webHost;
			_product_ImageService = product_ImageService;
		}
		public async Task<IActionResult> Index()
		{
			List<Product> listProduct = new();
			var response = await _product.GetAllProductsAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				listProduct = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
			}
			ViewBag.Layout = "_AdminLayout";
			return View("Index", listProduct);
		}


		public async Task<IActionResult> Create()
		{
			ProductCreateVM productCreateVM = new();
			var response = await _category.GetAllCategoriesAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				productCreateVM.CategoryList = JsonConvert.DeserializeObject<List<Category>>
					(Convert.ToString(response.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}).ToList();
			}
			ViewBag.Layout = "_AdminLayout";
			return View("Create", productCreateVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
		{
			if (productCreateVM == null)
			{
				Console.WriteLine("ProductCreateVM is null");
				return View("Create", productCreateVM);
			}

			if (productCreateVM.ImageVM != null && productCreateVM.ImageVM.Length > 0)
			{
				string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "asset/frontend/img/");
				string fileName = Guid.NewGuid().ToString() + "-" + productCreateVM.ImageVM.FileName;
				string filePath = Path.Combine(uploadDir, fileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await productCreateVM.ImageVM.CopyToAsync(fileStream);
				}
				productCreateVM.pro_id.Image = "/asset/frontend/img/" + fileName;
			}

			if (ModelState.IsValid)
			{
				var response = await _product.CreateProductAsync<APIResponse>(productCreateVM.pro_id);
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
			else
			{
				foreach (var modelState in ModelState.Values)
				{
					foreach (var error in modelState.Errors)
					{
						Console.WriteLine(error.ErrorMessage);
					}
				}
			}

			var resp = await _category.GetAllCategoriesAsync<APIResponse>();
			if (resp != null && resp.IsSuccess)
			{
				productCreateVM.CategoryList = JsonConvert.DeserializeObject<List<Category>>
					(Convert.ToString(resp.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}).ToList();
			}
			ViewBag.Layout = "_AdminLayout";
			return View("Create", productCreateVM);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var response = await _product.DeleteProductAsync<APIResponse>(id);
			if (response != null && response.IsSuccess)
			{
				return Json(new { status = "success", message = "Delete Successful!" });
			}
			ViewBag.Layout = "_AdminLayout";

			return Json(new { status = "success", message = "Delete Failed!" });
		}
		[HttpPut]
		public async Task<IActionResult> ChangeStatus(int id)
		{
			var response = await _product.GetProductByIdAsync<APIResponse>(id);
			if (response != null && response.IsSuccess)
			{
				Product product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
				product.Status = product.Status == 1 ? 0 : 1;
				var responseChange = await _product.ChangeStatus<APIResponse>(id, product);
				if (responseChange != null && responseChange.IsSuccess)
				{
					return Json(new { status = "success", message = "Change Status Successful!" });
				}
			}

			ViewBag.Layout = "_AdminLayout";
			return Json(new { status = "success", message = "Change Status Failed!" });
		}


		public async Task<IActionResult> Edit(int id)
		{
			ProductUpdateVM productUpdateVM = new();
			var response = await _product.GetProductByIdAsync<APIResponse>(id);
			if (response != null && response.IsSuccess)
			{
				Product product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
				productUpdateVM.pro_id = product;
			}
			response = await _category.GetAllCategoriesAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				productUpdateVM.CategoryList = JsonConvert.DeserializeObject<List<Category>>
					(Convert.ToString(response.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}).ToList();
			}
			return View(productUpdateVM);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductUpdateVM product)
		{

			var existingProductResponse = await _product.GetProductByIdAsync<APIResponse>(product.pro_id.Id);
			if (existingProductResponse != null && existingProductResponse.IsSuccess)
			{
				var existingProduct = JsonConvert.DeserializeObject<Product>(Convert.ToString(existingProductResponse.Result));
				if (existingProduct != null)
				{
					// If no new image is uploaded, keep the existing image path
					if (product.ImageVM == null || product.ImageVM.Length == 0)
					{
						product.pro_id.Image = existingProduct.Image;
					}
					else
					{
						// Handle new image upload
						string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "asset/frontend/img/");
						string fileName = Guid.NewGuid().ToString() + "-" + product.ImageVM.FileName;
						string filePath = Path.Combine(uploadDir, fileName);

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await product.ImageVM.CopyToAsync(fileStream);
						}
						product.pro_id.Image = "/asset/frontend/img/" + fileName;
					}
				}
			}
			if (ModelState.IsValid)
			{

				var response = await _product.UpdateProductAsync<APIResponse>(product.pro_id.Id, product.pro_id);
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
			var resp = await _category.GetAllCategoriesAsync<APIResponse>();
			if (resp != null && resp.IsSuccess)
			{
				product.CategoryList = JsonConvert.DeserializeObject<List<Category>>
					(Convert.ToString(resp.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}).ToList();
			}
			ViewBag.Layout = "_AdminLayout";
			return View(product);
		}
		public async Task<IActionResult> GetImageById(int id)
		{
			List<Product_image> listProduct_image = new();
			var response = await _product_ImageService.GetImageByIdProduct<APIResponse>(id);
			if (response != null && response.IsSuccess)
			{
				var productImage = JsonConvert.DeserializeObject<Product_image>(Convert.ToString(response.Result));
				if (productImage != null)
				{
					listProduct_image.Add(productImage);
				}
			}
			ViewBag.Layout = "_AdminLayout";
			return View(listProduct_image);
		}


	}

}
