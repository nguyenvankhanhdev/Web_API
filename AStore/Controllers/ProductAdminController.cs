
using Microsoft.AspNetCore.Mvc;

namespace AStore_Web.Controllers
{
    public class ProductAdminController : Controller
    {


        public ProductAdminController()
        {

        }

        public async Task<IActionResult> IndexProduct()
        {
            ViewBag.Layout = "_AdminLayout";
            return View();
        }
    }
}
