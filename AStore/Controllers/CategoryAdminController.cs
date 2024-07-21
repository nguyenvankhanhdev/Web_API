using AStore_Web.Models;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AStore_Web.Controllers
{
	public class CategoryAdminController : Controller
	{
		private readonly ICategoryService _category;
		public CategoryAdminController(ICategoryService category)
		{
			_category = category;
		}
		public async Task< IActionResult> Index()
		{
			List<Category> listCate = new();
			var response = await _category.GetAllCategoriesAsync<APIResponse>();
			if(response !=null && response.IsSuccess)
			{
				listCate = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
			}
			ViewBag.Layout = "_AdminLayout";
			return View(listCate);

		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				var response = await _category.CreateCategoryAsync<APIResponse>(category);
				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(category);
		}



	}
}
