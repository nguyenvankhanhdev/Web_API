using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStore_Web.Models
{
	public class Order_detail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Order")]
		public int Order_id { get; set; }
		[ForeignKey("Product")]
		public int Product_id { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }
		public int Total { get; set; }
		public Order Order { get; set; }
		public Product Product { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
