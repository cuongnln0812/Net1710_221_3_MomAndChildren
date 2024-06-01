using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndChildren.Business;
using MomAndChildren.Data.Models.DTO;
using MomAndChildren.Data.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MomAndChildren.Data.Repositories;
using MomAndChildren.Data;

namespace MomAndChildren.RazorWebApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartBusiness _cartService;
        private readonly IOrderDetailBusiness _orderDetail = new OrderDetailBusiness();
        private readonly IProductBusiness _product = new ProductBusiness();
        public CartModel(ICartBusiness cartService)
        {
            _cartService = cartService;
        }
        public string Message { get; set; } = default;

        public List<CartItem> CartItems { get; set; }
        public List<Product> Products { get; set; }
        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public void OnGet()
        {
            CartItems = _cartService.GetCartItems();
            Products = this.GetProducts();
        }

        private List<Product> GetProducts()
        {
            var categoryResult = _product.GetProductsAsync();

            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                var categories = (List<Product>)categoryResult.Result.Data;
                return categories;
            }
            return new List<Product>();
        }

        public IActionResult OnPostAddToCart()
        {
            _cartService.AddProductToCart(ProductId, Quantity);
            return RedirectToPage();
        }

        public IActionResult OnPostCreateOrder()
        {
            var cartItems = _cartService.GetCartItems();
            if (cartItems.Count > 0)
            {
                var orderDetailResult = _orderDetail.CreateOrderDetail(1, cartItems);
                if (orderDetailResult != null)
                {
                    this.Message = orderDetailResult.Result.Message;
                }
                else
                {
                    this.Message = "Error system";
                }
                _cartService.ClearCart();
                return RedirectToPage("OrderDetail");
            }
            return Page();
        }

    }
}
