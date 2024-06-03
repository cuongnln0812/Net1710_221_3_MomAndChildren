using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndChildren.Business;
using MomAndChildren.Data.Models;

namespace MomAndChildren.RazorWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductBusiness _productBusiness = new ProductBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Product Product { get; set; } = default;
        public List<Product> Products { get; set; } = new List<Product>();
        public void OnGet()
        {
            Products = this.GetProducts();
        }
        public void OnPost()
        {
            this.ProductRepository();
        }

        public void OnDelete()
        {
        }

        private List<Product> GetProducts()
        {
            var productResult = _productBusiness.GetProductsAsync();

            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                var products = (List<Product>)productResult.Result.Data;
                return products;
            }
            return new List<Product>();
        }

        private void ProductRepository()
        {
            var productResult = _productBusiness.CreateProduct(this.Product);

            if (productResult != null)
            {
                this.Message = productResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
