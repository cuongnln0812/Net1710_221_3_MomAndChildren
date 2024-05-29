using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndChildren.Business;
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
        public Order Order { get; set; }
        public List<PaymentHistory> PaymentHistories {get; set;}
        public List<Order> SelectiveOrderList { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            PaymentHistories = this.GetPaymentHistories();
        }

        public void OnPost()
        {
            this.CreatePayment();
        }

        private List<Order> GetOrders()
        {

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
            var result = _paymentHistoryBusiness.CreatePaymentHistory(Order, PaymentMethod);

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
