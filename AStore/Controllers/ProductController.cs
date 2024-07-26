using AStore_Web.Models;
using AStore_Web.Models.VM.Product;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AStore_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        public ProductController(IProductService product, ICategoryService category)
        {
            _product = product;
            _category = category;

        }
        public async Task<ActionResult> Index()
        {
            List<Category> categories = new();
            var response = await _category.GetAllCategoriesAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
            }
            ViewBag.Layout = "_FrontendLayout";
            return View(categories);
        }
        public async Task<ActionResult> Details(int id)
        {
            Product product = new();
            var response = await _product.GetProductByIdAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));

            }
            var cateName = await _category.GetCategoryByIdAsync<APIResponse>(product.CategoryId);
            var viewModel = new ProductViewModel
            {
                Product = product,
                CategoryName = cateName.IsSuccess ? JsonConvert.DeserializeObject<Category>(Convert.ToString(cateName.Result)).Name : ""
            };
            ViewBag.Layout = "_FrontendLayout";
            return View(viewModel);
        }

        public async Task<ActionResult> showProductByCate(int id)
        {
            List<Product> products = new();
            var response = await _product.GetProductByCategoryAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
            }
            ViewBag.Layout = "_FrontendLayout";
            return View(products);
        }
    }
}
