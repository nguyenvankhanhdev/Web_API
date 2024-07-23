using AStore_API.Models;
using AStore_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AStore_API.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IProductRepository _product;
		private readonly ICategoryRepository _category;
		public ProductController(IProductRepository product, ICategoryRepository category)
		{
			_product = product;
			this._response = new APIResponse();
			_category = category;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAllProduct()
		{
			try
			{
				IEnumerable<Product> products = await _product.GetAllAsync(includeProperties:"Category");
				_response.Result = products;
				_response.IsSuccess = true;
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
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string>() { "Invalid Product ID" };
					return BadRequest(_response);
				}
				var product = await _product.GetAsync(v => v.Id == id);
				if (product == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Result = product;
				_response.IsSuccess = true;
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
				_response.Result = product;
				_response.IsSuccess = true;
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetProduct", new { id = product.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;

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
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				
				}
				await _product.RemoveAsync(product);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
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
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var product = await _product.GetAsync(v => v.Id == id);
				if (product == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _product.UpdateAsync(pro);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return NoContent();
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		[HttpPut("{id:int}/status", Name = "UpdateStatus")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> ChangeStatus(int id, [FromBody] Product product)
        {
            try
            {
                if (id == 0 || product.Id != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }
                var pro = await _product.GetAsync(v => v.Id == id);
                if (pro == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Product not found." };
                    return NotFound(_response);
                }

                pro.Status = product.Status;
                await _product.UpdateAsync(pro);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return NoContent();
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
