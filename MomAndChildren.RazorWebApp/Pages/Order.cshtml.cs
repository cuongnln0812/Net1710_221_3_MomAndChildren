using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndChildren.Business;
using MomAndChildren.Data.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MomAndChildren.RazorWebApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness = new OrderBusiness();

        [BindProperty]
        //public Customer Customer { get; set; } = default;

        public string Message { get; set; } = default;
        [BindProperty]
        public Order Order { get; set; } = default;
        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet()
        {
            Orders = this.GetOrders();
        }

        public void OnPost()
        {
            this.SaveOrder();
        }

        public void OnDelete()
        {
        }


        private List<Order> GetOrders()
        {
            var orderResult = _orderBusiness.GetOrdersAsync();

            if (orderResult.Status > 0 && Order.Result.Data != null)
            {
                var orders = (List<Order>)orderResult.Result.Data;
                return orders;
            }
            return new List<Order>();
        }

        private void SaveOrder()
        {
            var orderResult = _orderBusiness.CreateOrder(this.Order);

            if (orderResult != null)
            {
                this.Message = orderResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
