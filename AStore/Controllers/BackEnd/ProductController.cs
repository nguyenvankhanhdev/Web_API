using Microsoft.AspNetCore.Mvc;

namespace AStore_Web.Controllers.BackEnd
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Layout = "_AdminLayout";
			return View();
		}
	}
}
