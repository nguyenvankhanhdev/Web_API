using AStore_API.Models;
using AStore_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AStore_API.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		protected APIResponse _reponse;
		private readonly IProductRepository _product;
		private readonly ICategoryRepository _category;
		public ProductController(IProductRepository product, ICategoryRepository category)
		{
			_product = product;
			this._reponse = new APIResponse();
			_category = category;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAllProduct()
		{
			try
			{
				IEnumerable<Product> products = await _product.GetAllAsync(includeProperties:"Category");
				_reponse.Result = products;
				_reponse.IsSuccess = true;
				_reponse.StatusCode = HttpStatusCode.OK;
				return Ok(_reponse);
			}
			catch (Exception ex)
			{
				_reponse.IsSuccess = false;
				_reponse.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _reponse;
		}
		[HttpGet("{id:int}", Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]	
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetProduct(int id)
		{
			try
			{
				if (id == 0)
				{
					_reponse.IsSuccess = false;
					_reponse.ErrorMessages = new List<string>() { "Invalid Product ID" };
					return BadRequest(_reponse);
				}
				var product = await _product.GetAsync(v => v.Id == id);
				if (product == null)
				{
					_reponse.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_reponse);
				}
				_reponse.Result = product;
				_reponse.IsSuccess = true;
				_reponse.StatusCode = HttpStatusCode.OK;
				return Ok(_reponse);
			}
			catch (Exception ex)
			{
				_reponse.IsSuccess = false;
				_reponse.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _reponse;
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] Product product)
		{
			try
			{
				if (await _product.GetAsync(v => v.Name.ToLower() == product.Name.ToLower()) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Product name already exists");
					return BadRequest(ModelState);
				}
				if (product == null)
				{
					ModelState.AddModelError("ErrorMessages", "Product object is null");
				}
				if (await _product.GetAsync(v => v.Id == product.Id) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Product ID already exists");
					return BadRequest(ModelState);
				}
				await _product.CreateAsync(product);
				_reponse.Result = product;
				_reponse.IsSuccess = true;
				_reponse.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetProduct", new { id = product.Id }, _reponse);
			}
			catch (Exception ex)
			{
				_reponse.IsSuccess = false;
				_reponse.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _reponse;

		}

		[HttpDelete("{id:int}", Name ="DeleteProduct")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
		{
			try
			{
				if(id == 0)
				{
					return BadRequest();
				}
				
				var product = await _product.GetAsync(v => v.Id == id);
				if (product == null)
				{
					_reponse.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_reponse);
				
				}
				await _product.RemoveAsync(product);
				_reponse.StatusCode = HttpStatusCode.NoContent;
				_reponse.IsSuccess = true;
				return Ok(_reponse);

			}
			catch (Exception ex)
			{
				_reponse.IsSuccess = false;
				_reponse.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _reponse;
		}
		[HttpPut("{id:int}", Name = "UpdateProduct")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] Product pro)
		{
			try
			{
				if (id == 0 || pro.Id != id || pro == null)
				{
					_reponse.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_reponse);
				}

			}
			catch (Exception ex)
			{
				_reponse.IsSuccess = false;
				_reponse.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _reponse;
		}
		// viết 1 hàm để upload image 
		[HttpPost("upload")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/frontend/img", fileName + extension);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _reponse.Result = fileName + extension;
                    _reponse.IsSuccess = true;
                    _reponse.StatusCode = HttpStatusCode.OK;
                    return Ok(_reponse);
                }
                else
                {
                    _reponse.IsSuccess = false;
                    _reponse.ErrorMessages = new List<string>() { "File is empty" };
                    return BadRequest(_reponse);
                }
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _reponse;
        }



	}
}
