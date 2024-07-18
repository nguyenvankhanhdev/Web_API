using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStore_API.Models
{
	public class Cart
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Product")]
		public int Product_id { get; set; }
		public int Quantity { get; set; }
		[ForeignKey("User")]
		public int User_id { get; set; }
		public Product Product { get; set; }
		public User User { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
