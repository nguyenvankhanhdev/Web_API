
using AStore_Library;
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

        public ProductAdminController(IProductService product, ICategoryService category)
        {
            _category = category;
            _product = product;
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
       
        public async Task<IActionResult> Create()
        {
            ProductCreateVM productCreateVM = new ();
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
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            if(ModelState.IsValid)
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
            var resp =await _product.GetAllProductsAsync<APIResponse>();

            if(resp != null && resp.IsSuccess)
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
