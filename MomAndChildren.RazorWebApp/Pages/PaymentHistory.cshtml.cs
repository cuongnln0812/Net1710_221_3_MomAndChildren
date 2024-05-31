using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MomAndChildren.Business;
using MomAndChildren.Common;
using MomAndChildren.Data.Models;

namespace MomAndChildren.RazorWebApp.Pages
{
    public class PaymentHistoryModel : PageModel
    {

        private readonly IPaymentHistoryBusiness _paymentHistoryBusiness;

        public PaymentHistoryModel(IPaymentHistoryBusiness paymentHistoryBusiness)
        {
            _paymentHistoryBusiness = paymentHistoryBusiness;
        }


        [BindProperty]
        public PaymentHistory Payment { get; set; }
        public List<PaymentHistory> PaymentHistories { get; set; }
        public List<SelectListItem> OrderList { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; }
        public string? Message { get; set; }

        public void OnGet()
        {
            PaymentHistories = this.GetPaymentHistories();
            OrderList = this.GetOrders();
        }

        public void OnPost()
        {
            this.CreatePayment();
        }

        private async List<SelectListItem> GetOrders()
        {
            var orderResult = await _orderBusiness.GetOrderListAsync();
            var orders = new List<SelectListItem>();

            if (orderResult.Status == Const.SUCCESS_READ_CODE && orderResult.Data is IEnumerable<Order> orderList)
            {
                foreach (var order in orderList)
                {
                    orders.Add(new SelectListItem
                    {
                        Value = order.OrderId.ToString(),
                        Text = $"Order #{order.OrderId} - {order.OrderDate.ToShortDateString()}"
                    });
                }
            }
            return orders;
        }

        private List<PaymentHistory> GetPaymentHistories()
        {
            var result = _paymentHistoryBusiness.GetPaymentHistoryList();

            if (result.Status > 0 && result.Result.Data != null)
            {
                var paymentHistories = (List<PaymentHistory>)result.Result.Data;
                return paymentHistories;
            }
            return new List<PaymentHistory>();
        }

        private void CreatePayment()
        {
            var result = _paymentHistoryBusiness.CreatePaymentHistory(Payment);

            if (result != null)
            {
                Message = result.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
