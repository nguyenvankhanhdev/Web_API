using AStore_API.Models;
using AStore_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AStore_API.Controllers
{
	[Route("api/images")]
	[ApiController]
	public class Product_imageController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IProduct_imageRepository _product_imageRepository;

		private readonly IProductRepository _productRepository;
		public Product_imageController(IProduct_imageRepository product_imageRepository, IProductRepository productRepository)
		{
			_product_imageRepository = product_imageRepository;
			this._response = new();
			_productRepository = productRepository;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAll()
		{
			try
			{
				IEnumerable<Product_image> product_images = await _product_imageRepository.GetAllAsync(includeProperties: "Product");
				_response.Result = product_images;
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
		[HttpGet("{id:int}", Name = "GetImage")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> GetImageById(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				var response = await _product_imageRepository.GetAsync(v => v.Id == id);
				if (response == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					return NotFound(_response);
				}
				_response.Result = response;
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateImages([FromBody] Product_image _Image)
		{
			try
			{
				if (_Image == null)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string>() { "Null Image" };
					return BadRequest(_response);
				}
				if (_Image.Product_id == 0)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string>() { "Invalid Product" };
					return BadRequest(_response);
				}
				var product = await _productRepository.GetAsync(v => v.Id == _Image.Product_id);
				if (product == null)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string>() { "Invalid Product" };
					return BadRequest(_response);
				}
				await _product_imageRepository.CreateAsync(_Image);
				_response.Result = _Image;
				_response.IsSuccess = true;
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetImage", new { id = _Image.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}


	}
}
