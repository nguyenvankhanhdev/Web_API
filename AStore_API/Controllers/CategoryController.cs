using AStore_API.Models;
using AStore_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AStore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		protected APIResponse _response;
		private readonly ICategoryRepository _cate;
		public CategoryController(ICategoryRepository cate)
		{
			_cate = cate;
			this._response = new APIResponse();
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> GetAllCate()
		{
			try
			{
				IEnumerable<Category> cates = await _cate.GetAllAsync();
				_response.Result = cates;
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		[HttpGet("{id:int}", Name = "GetCategory")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetCate(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string>() { "Invalid Category ID" };
					return BadRequest(_response);
				}
				var Cate = await _cate.GetAsync(v => v.Id == id);
				if (Cate == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Result = Cate;
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;

		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateCate([FromBody] Category cate)
		{
			try
			{
				if (await _cate.GetAsync(v => v.Name.ToLower() == cate.Name.ToLower()) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Category name already exists");
					return BadRequest(ModelState);
				}
				if (cate == null)
				{
					return BadRequest(cate);
				}
				await _cate.CreateAsync(cate);
				_response.Result = cate;
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetCategory", new { id = cate.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		[HttpDelete("{id:int}", Name = "DeleteCate")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<APIResponse>> DeleteCate(int id)
		{

			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var cate = await _cate.GetAsync(v => v.Id == id);
				if (cate == null)
				{
					return NotFound();
				}

				await _cate.RemoveAsync(cate);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;

		}
		[HttpPut("{id:int}", Name = "UpdateCate")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<APIResponse>> UpdateCate(int id, [FromBody] Category cate)
		{
			try
			{
				if (id == 0 || cate.Id != id)
				{
					return BadRequest();
				}
				var category = await _cate.GetAsync(v => v.Id == id);
				if (cate == null)
				{
					return NotFound();
				}
				category.Name = cate.Name;
				category.Slug = cate.Slug;
				category.Status = cate.Status;
				await _cate.UpdateAsync(category);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;

		}

	}
}
