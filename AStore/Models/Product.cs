using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStore_Web.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required(ErrorMessage = "Price is required")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public decimal Price { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Offer Price must be greater than 0")]
		public decimal Offer_price { get; set; }
		[ValidateNever]
		public string Image { get; set; }
		public int Status { get; set; }
		public string Product_type { get; set; }
		public string Slug { get; set; }
		public int CategoryId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
