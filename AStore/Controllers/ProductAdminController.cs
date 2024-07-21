
using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Models.VM.Product;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AStore_Web.Controllers
{
	public class ProductAdminController : Controller
	{
		private readonly IProductService _product;
		private readonly ICategoryService _category;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductAdminController(IProductService product, ICategoryService category, IWebHostEnvironment webHost)
		{
			_category = category;
			_product = product;
			_webHostEnvironment = webHost;
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
			return View(listProduct);
		}

        public async Task<IActionResult> CreateProduct()
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
					});
			}

			return View(productCreateVM);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductCreateVM product)
        {
            if (product.ImageVM != null && product.ImageVM.Length > 0)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "asset");
                string fileName = Guid.NewGuid().ToString() + "-" + product.ImageVM.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageVM.CopyToAsync(fileStream);
                }

                product.pro_id.Image = "/asset/frontend/img/" + fileName;
            }
            Console.WriteLine(product.pro_id.Image);

            if (product !=null)
            {
                var response = await _product.CreateProductAsync<APIResponse>(product.pro_id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
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
                product.CategoryList = JsonConvert.DeserializeObject<List<Category>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(product);
        }



    }
}
