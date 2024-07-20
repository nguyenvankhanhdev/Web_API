using AStore_Web.Models;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AStore_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        public ProductController(IProductService product)
        {
            _product = product;

        }

        public async Task<ActionResult> Index()
        {
            List<Product> listProduct = new();
            var response = await _product.GetAllProductsAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                listProduct = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
            }
            ViewBag.Layout = "_FrontendLayout";
            return View(listProduct);
        }
    }
}
