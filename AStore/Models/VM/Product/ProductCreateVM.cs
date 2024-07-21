using AStore_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AStore_Web.Models.VM.Product
{
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            pro_id = new Models.Product();
        }

        [ValidateNever]
        public Models.Product pro_id { get; set; }
        [ValidateNever]
        public IFormFile ImageVM { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
