using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStore_API.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Offer_price { get; set; }
		public string Image { get; set; }
		public int Status { get; set; }
		public string Product_type { get; set; }
		public string Slug { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
